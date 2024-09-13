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
    //�R���{��
    public static int Combo;
    //�ő�R���{��
    public static int BestCombo;
    //�R���{�\��
    public static int Combo_type;
    //�X�R�A
    public static int Score;
    //���U���g
    public static int[] result = new int[3];

    //�y�Ȃ̔ԍ�(3�P�^)
    public static string musicNum;
    //��Փx
    public static int difficulty;

    //�m�[�c�̃v���n�u
    public GameObject notesPre;
    //���ʂ�csv���i�[����
    List<string[]> notes = new List<string[]>();
    //�m�[�c�̐�����
    List<float[]> NoteTimings = new List<float[]>();

    //AudioSorce
    AudioSource myAudioSource;
    //��
    public static AudioClip music;

    //FC,AP�̉��o�I�u�W�F�N�g
    [SerializeField] GameObject FCtext, APtext;
    //SEController
    [SerializeField]
    SEController SEController;
    //AP,FC ��SE
    [SerializeField]
    AudioClip[] eventClip = new AudioClip[2];
    //SceneChange
    [SerializeField] SceneChange sceneChange;

    //bpm
    float BPM;
    // Start is called before the first frame update
    async void Start()
    {
        //AudioSorce���擾
        myAudioSource = GetComponent<AudioSource>();
        //�Ȃ����[�h
        var musicHandle = Addressables.LoadAssetAsync<AudioClip>("m_" + musicNum);
        music = await musicHandle.Task;
        myAudioSource.clip = music;
        //CSV�����[�h
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
        //���������ɐ���
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
            //delta��0�ɂȂ�����Ȃ��Đ�
            if (delta > 0 && !isMusicPlay)
            {
                myAudioSource.Play();
                isMusicPlay = true;
            }
            //�ő�R���{�����X�V
            if (Combo >= BestCombo)
            {
                BestCombo = Combo;
            }
        }
        else
        {
            //�Ō�ɂȂ����牉�o��\����V�[���]��
            if (!myAudioSource.isPlaying && !isfinish)
            {
                Addressables.Release("m_" + musicNum);
                //�~�X��0
                if (result[2] == 0)
                {
                    //�O���ɂ���ĉ��o��ς���
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

    //���ʂ�CSV�����[�h����
    /// <summary>
    /// CSV�̒��g�̉���ꉞ
    /// ��s��:�ȃ^�C�g��,��Ȏ҂Ȃ�("���:�`�`"�̂悤�ɏ���),��Փx,BPM,�R���{��
    /// ����ȍ~:BPM�ω��p�̃m�[�c���ۂ�(0or1��),�m�[�c��x���W,y���W,�O�̃m�[�c�Ƃ̊Ԋu���q,����(ex 16���Ȃ�1,16 �����Ȃ� 0,1),�n�C�X�s(��ڂ�1�Ȃ�ω����BPM����)
    /// </summary>
    async void LoadCSV()
    {
        //CSV�̒��g���i�[�AStringReader�ɕϊ�
        var csvHandle = Addressables.LoadAssetAsync<TextAsset>("s_" + musicNum + "_" + difficulty.ToString());
        TextAsset scoreCSV = await csvHandle.Task;
        Addressables.Release(csvHandle);
        StringReader reader = new StringReader(scoreCSV.text);
        //�ǂݎ��
        while(reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            notes.Add(line.Split(','));
        }
        //�������ɕ��ѕς�
        ArrageTiming();
    }
    //�m�[�c�𐶐����ɂ���
    void ArrageTiming()
    {
        //�������Ԃ��v�Z���A�z��ɂԂ�����
        float totalTime = -SettingManager.MusicOffset / 10;

        BPM = float.Parse(notes[0][3]) * myAudioSource.pitch;

        //BPM�����u��
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
        //���ѕς���
        float[][] TimingsArray = NoteTimings.ToArray();
        Array.Sort(TimingsArray, ( a, b) => a[1] > b[1] ? 1 : -1);
        NoteTimings = TimingsArray.ToList();


        //delta��������
        delta = NoteTimings[0][1] - 7;
        //�R���{�A�X�R�A�A���U���g��������
        Combo = 0;
        Combo_type = 0;
        Score = 0;
        result[0] = 0;
        result[1] = 0;
        result[2] = 0;
    }
}
