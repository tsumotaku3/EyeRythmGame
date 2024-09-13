using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CI.QuickSave;

public class HighScoreController : MonoBehaviour
{
    //セーブ設定
    QuickSaveSettings m_saveSettings;
    //TMP
    [SerializeField]
    TextMeshProUGUI myTMP;
    // Start is called before the first frame update
    void Start()
    {
         // QuickSaveSettingsのインスタンスを作成
        m_saveSettings = new QuickSaveSettings();
        // 暗号化の方法 
        m_saveSettings.SecurityMode = SecurityMode.Aes;
        // Aesの暗号化キー
        m_saveSettings.Password = "Password";
        // 圧縮の方法
        m_saveSettings.CompressionMode = CompressionMode.Gzip;
        LoadData();
    }

    //ハイスコアを表示・設定
    void LoadData()
    {
        // QuickSaveReaderのインスタンスを作成
        QuickSaveReader reader = QuickSaveReader.Create("HighScore", m_saveSettings);
        //ファイルが無ければ無視
        if (!reader.TryRead(NotesGenerator.musicNum + "_" + NotesGenerator.difficulty,out int highScore))
        {
            myTMP.text = NotesGenerator.Score.ToString("N0");
            // QuickSaveWriterのインスタンスを作成
            QuickSaveWriter writer = QuickSaveWriter.Create("HighScore", m_saveSettings);

            // データを書き込む
            writer.Write(NotesGenerator.musicNum + "_" + NotesGenerator.difficulty, NotesGenerator.Score);

            writer.Commit();
            return;
        }
        else
        { 
            //ハイスコア更新の有無
            if (NotesGenerator.Score > highScore)
            {
                myTMP.text = NotesGenerator.Score.ToString("N0");
                //更新したらハイスコアを更新する()
                // QuickSaveWriterのインスタンスを作成
                QuickSaveWriter writer = QuickSaveWriter.Create("HighScore", m_saveSettings);

                // データを書き込む
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
