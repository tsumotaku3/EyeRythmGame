using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Threading;
using System.Threading.Tasks;

public class NotesGenerator : MonoBehaviour
{
    //コンボ数
    public static int Combo;
    //最大コンボ数
    public static int BestCombo;
    //コンボ表示
    public static int Combo_type;
    //スコア
    public static int Score;
    //リザルト
    public static int[] result = new int[3];

    //楽曲の番号(3ケタ)
    public static string musicNum;
    //難易度
    public static int difficulty;

    //ノーツのプレハブ
    public GameObject notesPre;
    //譜面のcsvを格納する
    List<string[]> notes = new List<string[]>();
    //ノーツの生成順
    List<float[]> NoteTimings = new List<float[]>();

    //AudioSorce
    AudioSource myAudioSource;
    //曲
    public static AudioClip music;

    //FC,APの演出オブジェクト
    [SerializeField] GameObject FCtext, APtext;
    //SEController
    [SerializeField]
    SEController SEController;
    //AP,FC のSE
    [SerializeField]
    AudioClip[] eventClip = new AudioClip[2];
    //SceneChange
    [SerializeField] SceneChange sceneChange;

    //bpm
    float BPM;
    // Start is called before the first frame update
    async void Start()
    {
        //AudioSorceを取得
        myAudioSource = GetComponent<AudioSource>();
        //曲をロード
        var musicHandle = Addressables.LoadAssetAsync<AudioClip>("m_" + musicNum);
        music = await musicHandle.Task;
        myAudioSource.clip = music;
        //CSVをロード
         LoadCSV();
    }

    float delta;
    public int NoteIndex = 0;
    bool isMusicPlay = false;
    bool isfinish = false;
    // Update is called once per frame
    void Update()
    {
        delta += Time.deltaTime;
        //いい感じに生成
        if (NoteIndex < notes.Count - 1 || !isMusicPlay)
        {
            if (delta > NoteTimings[NoteIndex][1])
            {
                if (notes[(int)NoteTimings[NoteIndex][0]][0] == "0")
                {
                    NotesSystem newNotes = Instantiate(notesPre.GetComponent<NotesSystem>(), new Vector2(float.Parse(notes[(int)NoteTimings[NoteIndex][0]][1]), float.Parse(notes[(int)NoteTimings[NoteIndex][0]][2])), Quaternion.identity, transform);
                    newNotes.HighSpeed = float.Parse(notes[(int)NoteTimings[NoteIndex][0]][5]);
                }
                NoteIndex++;
            }
            //deltaが0になったら曲を再生
            if (delta > 0 && !isMusicPlay)
            {
                myAudioSource.Play();
                isMusicPlay = true;
            }
            //最大コンボ数を更新
            if (Combo >= BestCombo)
            {
                BestCombo = Combo;
            }
        }
        else
        {
            //最後になったら演出を表示後シーン転移
            if (!myAudioSource.isPlaying && !isfinish)
            {
                Addressables.Release("m_" + musicNum);
                //ミスが0
                if (result[2] == 0)
                {
                    //グレによって演出を変える
                    if (result[1] == 0)
                    {
                        SEController.OnClick(eventClip[0]);
                        APtext.SetActive(true);
                    }
                    else
                    {
                        SEController.OnClick(eventClip[1]);
                        FCtext.SetActive(true);
                    }
                }
                isfinish = true;
                sceneChange.StartCoroutine(sceneChange.LoadScene(3, "Result"));
            }
        }
    }

    //譜面のCSVをロードする
    /// <summary>
    /// CSVの中身の解説一応
    /// 一行目:曲タイトル,作曲者など("作曲:～～"のように書く),難易度,BPM,コンボ数
    /// それ以降:BPM変化用のノーツか否か(0or1で),ノーツのx座標,y座標,前のノーツとの間隔分子,分母(ex 16分なら1,16 同時なら 0,1),ハイスピ(一つ目が1なら変化先のBPMをば)
    /// </summary>
    async void LoadCSV()
    {
        //CSVの中身を格納、StringReaderに変換
        var csvHandle = Addressables.LoadAssetAsync<TextAsset>("s_" + musicNum + "_" + difficulty.ToString());
        TextAsset scoreCSV = await csvHandle.Task;
        Addressables.Release(csvHandle);
        StringReader reader = new StringReader(scoreCSV.text);
        //読み取る
        while(reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            notes.Add(line.Split(','));
        }
        //生成順に並び変え
        ArrageTiming();
    }
    //ノーツを生成順にする
    void ArrageTiming()
    {
        //生成時間を計算し、配列にぶち込む
        float totalTime = -SettingManager.MusicOffset / 10;

        BPM = float.Parse(notes[0][3]) * myAudioSource.pitch;

        //BPMを仮置き
        float tempBPM = BPM;

        for (int i = 1;i < notes.Count;i++)
        {
            totalTime += 1 / (tempBPM / 60 * (float.Parse(notes[i][4]) / 4) / float.Parse(notes[i][3]));
            float[] temp = new float[2];
            temp[0] = i;
            if (notes[i][0] == "1")
            {
                temp[1] = totalTime;
                tempBPM = float.Parse(notes[i][5]);
            }
            else
            {
                temp[1] = totalTime - 1 / (SettingManager.NoteSpeed * float.Parse(notes[i][5]));
            }
            NoteTimings.Add(temp);
        }
        //並び変える
        float[][] TimingsArray = NoteTimings.ToArray();
        Array.Sort(TimingsArray, ( a, b) => a[1] > b[1] ? 1 : -1);
        NoteTimings = TimingsArray.ToList();


        //deltaを初期化
        delta = NoteTimings[0][1] - 7;
        //コンボ、スコア、リザルトを初期化
        Combo = 0;
        Combo_type = 0;
        Score = 0;
        result[0] = 0;
        result[1] = 0;
        result[2] = 0;
    }
}
