using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;
using CI.QuickSave.Core.Storage;

public class SettingManager : MonoBehaviour
{
    //�ݒ荀��
    public static float 
        NoteSpeed,judgeOffset,MusicOffset,NoteSize,JudgeSpeed,//�Q�[���ݒ�
        sys_BGM,sys_SE,game_BGM,game_TSE,game_SSE;//���ʐݒ�

    //�Z�[�u�ݒ�
    QuickSaveSettings m_saveSettings;

    //SceneChange
    [SerializeField]
    SceneChange sceneChange;
    // Start is called before the first frame update
    void Start()
    {
        // QuickSaveSettings�̃C���X�^���X���쐬
        m_saveSettings = new QuickSaveSettings();
        // �Í����̕��@ 
        m_saveSettings.SecurityMode = SecurityMode.Aes;
        // Aes�̈Í����L�[
        m_saveSettings.Password = "EyeRythm";
        // ���k�̕��@
        m_saveSettings.CompressionMode = CompressionMode.Gzip;

        LoadData();
    }
    //�f�[�^�����[�h����
    void LoadData()
    {

        //�t�@�C����������Ώ����l�ɐݒ�
        if (FileAccess.Exists("SaveData") == false)
        {
            NoteSpeed = 1;
            judgeOffset = 0;
            MusicOffset = 0;
            NoteSize = 1;

            sys_BGM = 100;
            sys_SE = 100;
            game_BGM = 100;
            game_TSE = 100;
            game_SSE = 100;

            WriteData();
        }
        else
        {

            // QuickSaveReader�̃C���X�^���X���쐬
            QuickSaveReader reader = QuickSaveReader.Create("SaveData", m_saveSettings);

            //���[�h����
            NoteSpeed = reader.Read<float>("NoteSpeed");
            judgeOffset = reader.Read<float>("judgeOffset");
            MusicOffset = reader.Read<float>("MusicOffset");
            NoteSize = reader.Read<float>("NoteSize");

            sys_BGM = reader.Read<float>("sys_BGM");
            sys_SE = reader.Read<float>("sys_SE");
            game_BGM = reader.Read<float>("game_BGM");
            game_TSE = reader.Read<float>("game_TSE");
            game_SSE = reader.Read<float>("game_SSE");
        }
    }

    //�f�[�^��ۑ�����
    public void WriteData()
    {
        // QuickSaveWriter�̃C���X�^���X���쐬
        QuickSaveWriter writer = QuickSaveWriter.Create("SaveData", m_saveSettings);

        // �f�[�^����������
        writer.Write("NoteSpeed", NoteSpeed);
        writer.Write("judgeOffset", judgeOffset);
        writer.Write("MusicOffset", MusicOffset);
        writer.Write("NoteSize", NoteSize);

        writer.Write("sys_BGM", sys_BGM);
        writer.Write("sys_SE", sys_SE);
        writer.Write("game_BGM", game_BGM);
        writer.Write("game_TSE", game_TSE);
        writer.Write("game_SSE", game_SSE);

        // �ύX�𔽉f
        writer.Commit();
    }
}
