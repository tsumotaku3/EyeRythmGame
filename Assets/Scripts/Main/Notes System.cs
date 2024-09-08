using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesSystem : MonoBehaviour
{
    //�A�j���[�V����
    public Animator myAnim;
    //����
    public float p_Range,g_Range;
    //����̈�
    float JudgeZone;
    //���蕶���̃X�v���C�g
    /// <summary>
    /// 0:Perfect
    /// 1:Good
    /// 2:Bad
    /// </summary>
    public Sprite[] judgeSprite;
    //���蕶���̃v���n�u
    public GameObject judgeText;
    //�n�C�X�s
    public float HighSpeed;

    //AudioSorce
    AudioSource[] NotesAudioSorces;
    //�m�[�c������
    public AudioClip SuccessSE;

    // Start is called before the first frame update
    void Start()
    {
        //��Փx�ɂ���Ĕ����ς���
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
        //AudioSorce���擾
        NotesAudioSorces = GameObject.FindGameObjectWithTag("NotesSEGenerator").GetComponents<AudioSource>();
    }

    //����̎���
    float delta = 0;
    //���肵����
    bool isJudge;
    void Update()
    {
        if (delta > 0)
        {
            //�}�E�X�̍��W���擾
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //�}�E�X�ɐG�ꂽ���擾
            if (true)//(Vector2.Distance(transform.position, mousePos) <= JudgeZone)
            {
                SpriteRenderer j_text = Instantiate(judgeText, transform.position, Quaternion.identity).GetComponent<SpriteRenderer>();
                //�������Perfect,�x�����Good
                if (true)//(delta > g_Range - p_Range)
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
                //�R���{�𑝂₷
                NotesGenerator.Combo++;
                Destroy(gameObject);
            }
            delta -= Time.deltaTime * 1000f;
        }else if (isJudge)
        {
            //�X�J���Bad
            SpriteRenderer j_text = Instantiate(judgeText, transform.position,Quaternion.identity).GetComponent<SpriteRenderer>();
            j_text.sprite = judgeSprite[2];
            //�R���{��������
            NotesGenerator.Combo = 0;
            NotesGenerator.Combo_type = 2;
            NotesGenerator.result[2]++;
            Destroy(gameObject);
        }
    }

    //delta���Z�b�g
    public void setDelta()
    {
        delta = g_Range;
        //���łɉ��炷
        NotesAudioSorces[0].PlayOneShot(SuccessSE);
        //��x���肵����A��܂���
        isJudge = true;
    }
}
