using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

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

    //�m�[�c�̃v���n�u
    public GameObject notesPre;
    //���ʂ̃t�@�C���p�X
    public string scorePass;
    //���ʂ�csv���i�[����
    List<string[]> notes = new List<string[]>();
    //�m�[�c�̐�����
    List<float[]> NoteTimings = new List<float[]>();

    //AudioSorce
    AudioSource myAudioSorce;

    //bpm
    float BPM;
    // Start is called before the first frame update
    void Start()
    {
        //AudioSorce���擾
        myAudioSorce = GetComponent<AudioSource>();
        Setting.NoteSpeed = 3f;
        //CSV�����[�h
        LoadCSV();
        BPM = float.Parse(notes[0][3]) * myAudioSorce.pitch;
        //�������ɕ��ѕς�
        ArrageTiming();
        //delta��������
        delta = NoteTimings[NoteIndex][1];
        myAudioSorce.time = (delta) * myAudioSorce.pitch;
        //�R���{�A�X�R�A�A���U���g��������
        Combo = 0;
        Combo_type = 0;
        Score = 0;
        result[0] = 0;
        result[1] = 0;
        result[2] = 0;
    }

    float delta = 0;
    public int NoteIndex = 0;
    bool isMusicPlay = false;
    // Update is called once per frame
    void Update()
    {
        delta += Time.deltaTime;
        //���������ɐ���
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
        if(delta > 0 && !isMusicPlay)
        {
            myAudioSorce.Play();
            isMusicPlay = true;
        }
        //�ő�R���{�����X�V
        if (Combo >= BestCombo)
        {
            BestCombo = Combo;
        }
        //�Ō�ɂȂ�����f�o�b�O
        if(delta > NoteTimings[NoteTimings.Count - 1][1] + Setting.GoodRange)
        {
            
        }
    }

    //���ʂ�CSV�����[�h����
    /// <summary>
    /// CSV�̒��g�̉���ꉞ
    /// ��s��:�ȃ^�C�g��,��Ȏ҂Ȃ�("���:�`�`"�̂悤�ɏ���),��Փx,BPM,�R���{��
    /// ����ȍ~:BPM�ω��p�̃m�[�c���ۂ�(0or1��),�m�[�c��x���W,y���W,�O�̃m�[�c�Ƃ̊Ԋu���q,����(ex 16���Ȃ�1,16 �����Ȃ� 0,1),�n�C�X�s(��ڂ�1�Ȃ�ω����BPM����)
    /// </summary>
    void LoadCSV()
    {
        //CSV�̒��g���i�[�AStringReader�ɕϊ�
        TextAsset scoreCSV = Resources.Load(scorePass) as TextAsset;
        StringReader reader = new StringReader(scoreCSV.text);
        //�ǂݎ��
        while(reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            notes.Add(line.Split(','));
        }
    }
    //�m�[�c�𐶐����ɂ���
    void ArrageTiming()
    {
        //�������Ԃ��v�Z���A�z��ɂԂ�����
        float totalTime = 0;

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
                temp[1] = totalTime - 1 / (Setting.NoteSpeed * float.Parse(notes[i][5]));
            }
            NoteTimings.Add(temp);
        }
        //���ѕς���
        float[][] TimingsArray = NoteTimings.ToArray();
        Array.Sort(TimingsArray, ( a, b) => a[1] > b[1] ? 1 : -1);
        NoteTimings = TimingsArray.ToList();
    }
}
