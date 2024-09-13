using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;
using CI.QuickSave.Core.Storage;

public class SettingManager : MonoBehaviour
{
    //�ݒ荀��
    public static float 
        NoteSpeed,JudgeOffset,MusicOffset,NoteSize,JudgeSpeed,//�Q�[���ݒ�
        sys_BGM,sys_SE,game_BGM,game_TSE,game_SSE;//���ʐݒ�

    //�Z�[�u�ݒ�
    QuickSaveSettings m_saveSettings;

    //�ݒ��ʂ̍��ڐ؂�ւ��A�j���[�^�[
    public Animator c_animator;
    // Start is called before the first frame update
    void Awake()
    {
        // QuickSaveSettings�̃C���X�^���X���쐬
        m_saveSettings = new QuickSaveSettings();
        // �Í����̕��@ 
        m_saveSettings.SecurityMode = SecurityMode.Aes;
        // Aes�̈Í����L�[
        m_saveSettings.Password = "Password";
        // ���k�̕��@
        m_saveSettings.CompressionMode = CompressionMode.Gzip;

        LoadData();
    }
    //�f�[�^�����[�h����
    void LoadData()
    {
        //�t�@�C����������Ώ����l�ɐݒ�
        if (!QuickSaveBase.RootExists("SaveData"))
        {
            NoteSpeed = 1;
            JudgeOffset = 0;
            MusicOffset = 0;
            NoteSize = 1;
            JudgeSpeed = 1;

            sys_BGM = 0;
            sys_SE = 0;
            game_BGM = 0;
            game_TSE = 0;
            game_SSE = 0;

            WriteData();
        }
        else
        {
            // QuickSaveReader�̃C���X�^���X���쐬
            QuickSaveReader reader = QuickSaveReader.Create("SaveData", m_saveSettings);

            //���[�h����
            NoteSpeed = reader.Read<float>("NoteSpeed");
            JudgeOffset = reader.Read<float>("JudgeOffset");
            MusicOffset = reader.Read<float>("MusicOffset");
            NoteSize = reader.Read<float>("NoteSize");
            JudgeSpeed = reader.Read<float>("JudgeSpeed");

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
        writer.Write("JudgeOffset", JudgeOffset);
        writer.Write("MusicOffset", MusicOffset);
        writer.Write("NoteSize", NoteSize);
        writer.Write("JudgeSpeed", JudgeSpeed);


        writer.Write("sys_BGM", sys_BGM);
        writer.Write("sys_SE", sys_SE);
        writer.Write("game_BGM", game_BGM);
        writer.Write("game_TSE", game_TSE);
        writer.Write("game_SSE", game_SSE);

        // �ύX�𔽉f
        writer.Commit();
    }

    //�ݒ��ʂ̐��ڃA�j���[�V����
    public void OnClick(bool isGame)
    {
        c_animator.SetBool("isSelectGame",isGame);
    }
}
