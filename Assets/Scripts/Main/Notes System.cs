using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesSystem : MonoBehaviour
{
    //アニメーション
    public Animator myAnim;
    //判定
    public float p_Range,g_Range;
    //判定領域
    float JudgeZone;
    //判定文字のスプライト
    /// <summary>
    /// 0:Perfect
    /// 1:Good
    /// 2:Bad
    /// </summary>
    public Sprite[] judgeSprite;
    //判定文字のプレハブ
    public GameObject judgeText;
    //ハイスピ
    public float HighSpeed;

    //AudioSorce
    AudioSource[] NotesAudioSorces;
    //ノーツ成功音
    public AudioClip SuccessSE;
    // Start is called before the first frame update
    void Start()
    {
        //難易度によって判定を変える
        switch (NotesGenerator.difficulty)
        {
            case 0://easy
                JudgeZone = 5;
                g_Range = 2000;
                p_Range = 1000;
                break;
            case 1://normal
                JudgeZone = 4;
                g_Range = 1500;
                p_Range = 750;
                break;
            case 2://hard
                JudgeZone = 3;
                g_Range = 1000;
                p_Range = 500;
                break;
            case 3://impossible
                JudgeZone = 2;
                g_Range = 500;
                p_Range = 200;
                break;
        }
        myAnim.SetFloat("Speed",SettingManager.NoteSpeed * HighSpeed);
        myAnim.SetFloat("JudgeRange", 1000 / g_Range);
        //ノーツサイズを設定
        gameObject.transform.localScale = 1.5f * new Vector2(SettingManager.NoteSize, SettingManager.NoteSize);
        //AudioSorceを取得
        NotesAudioSorces = GameObject.FindGameObjectWithTag("NotesSEGenerator").GetComponents<AudioSource>();

        //判定開始時間を調整
        JSdelta = -(1 / (SettingManager.NoteSpeed * HighSpeed)) + SettingManager.JudgeOffset / 10;
    }

    //判定の時間
    [SerializeField]
    float delta = 0;
    //判定開始時間
    [SerializeField]
    float JSdelta = 0;
    //判定したか
    bool isJudge = false;
    void Update()
    {
        //判定開始を感知
        if (JSdelta > 0 && !isJudge)
        {
            //deltaを設定
            delta = g_Range;
            //ついでに音鳴らす
            NotesAudioSorces[0].PlayOneShot(SuccessSE);
            //一度判定したら帰れません
            isJudge = true;
        }
        else
        {
            JSdelta += Time.deltaTime;
        }
        if (delta > 0)
        {
            //マウスの座標を取得
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //マウスに触れたか取得
            if (Vector2.Distance(transform.position, mousePos) <= JudgeZone)
            {
                SpriteRenderer j_text = Instantiate(judgeText, transform.position, Quaternion.identity).GetComponent<SpriteRenderer>();
                //速ければPerfect,遅ければGood
                if (delta > g_Range - p_Range)
                {
                    j_text.sprite = judgeSprite[0];
                    j_text.gameObject.GetComponent<JudgeTextSystem>().PlaySound(0);
                    NotesGenerator.result[0]++;
                }
                else
                {
                    j_text.sprite = judgeSprite[1];
                    j_text.gameObject.GetComponent<JudgeTextSystem>().PlaySound(1);
                    if(NotesGenerator.Combo_type == 0) NotesGenerator.Combo_type = 1;
                    NotesGenerator.result[1]++;

                }
                //コンボを増やす
                NotesGenerator.Combo++;
                Destroy(gameObject);
            }
        }else if (isJudge)
        {
            //スカるとBad
            SpriteRenderer j_text = Instantiate(judgeText, transform.position,Quaternion.identity).GetComponent<SpriteRenderer>();
            j_text.sprite = judgeSprite[2];
            //コンボを初期化
            if(NotesGenerator.Combo > NotesGenerator.BestCombo)NotesGenerator.BestCombo = NotesGenerator.Combo;
            NotesGenerator.Combo = 0;
            NotesGenerator.Combo_type = 2;
            NotesGenerator.result[2]++;
            Destroy(gameObject);
        }
        delta -= Time.deltaTime * 1000f;
    }
}
