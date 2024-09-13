using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CI.QuickSave;

public class HighScoreController : MonoBehaviour
{
    //�Z�[�u�ݒ�
    QuickSaveSettings m_saveSettings;
    //TMP
    [SerializeField]
    TextMeshProUGUI myTMP;
    // Start is called before the first frame update
    void Start()
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

    //�n�C�X�R�A��\���E�ݒ�
    void LoadData()
    {
        // QuickSaveReader�̃C���X�^���X���쐬
        QuickSaveReader reader = QuickSaveReader.Create("HighScore", m_saveSettings);
        //�t�@�C����������Ζ���
        if (!reader.TryRead(NotesGenerator.musicNum + "_" + NotesGenerator.difficulty,out int highScore))
        {
            myTMP.text = NotesGenerator.Score.ToString("N0");
            // QuickSaveWriter�̃C���X�^���X���쐬
            QuickSaveWriter writer = QuickSaveWriter.Create("HighScore", m_saveSettings);

            // �f�[�^����������
            writer.Write(NotesGenerator.musicNum + "_" + NotesGenerator.difficulty, NotesGenerator.Score);

            writer.Commit();
            return;
        }
        else
        { 
            //�n�C�X�R�A�X�V�̗L��
            if (NotesGenerator.Score > highScore)
            {
                myTMP.text = NotesGenerator.Score.ToString("N0");
                //�X�V������n�C�X�R�A���X�V����()
                // QuickSaveWriter�̃C���X�^���X���쐬
                QuickSaveWriter writer = QuickSaveWriter.Create("HighScore", m_saveSettings);

                // �f�[�^����������
                writer.Write(NotesGenerator.musicNum + "_" +NotesGenerator.difficulty, NotesGenerator.Score);

                writer.Commit();
            }
            else
            {
                myTMP.text = highScore.ToString("N0");
            }

        }
    }
}
