using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;
using CI.QuickSave.Core.Storage;

public class SettingManager : MonoBehaviour
{
    //設定項目
    public static float 
        NoteSpeed,judgeOffset,MusicOffset,NoteSize,JudgeSpeed,//ゲーム設定
        sys_BGM,sys_SE,game_BGM,game_TSE,game_SSE;//音量設定

    //セーブ設定
    QuickSaveSettings m_saveSettings;

    //SceneChange
    [SerializeField]
    SceneChange sceneChange;
    // Start is called before the first frame update
    void Start()
    {
        // QuickSaveSettingsのインスタンスを作成
        m_saveSettings = new QuickSaveSettings();
        // 暗号化の方法 
        m_saveSettings.SecurityMode = SecurityMode.Aes;
        // Aesの暗号化キー
        m_saveSettings.Password = "EyeRythm";
        // 圧縮の方法
        m_saveSettings.CompressionMode = CompressionMode.Gzip;

        LoadData();
    }
    //データをロードする
    void LoadData()
    {

        //ファイルが無ければ初期値に設定
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

            // QuickSaveReaderのインスタンスを作成
            QuickSaveReader reader = QuickSaveReader.Create("SaveData", m_saveSettings);

            //ロードする
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

    //データを保存する
    public void WriteData()
    {
        // QuickSaveWriterのインスタンスを作成
        QuickSaveWriter writer = QuickSaveWriter.Create("SaveData", m_saveSettings);

        // データを書き込む
        writer.Write("NoteSpeed", NoteSpeed);
        writer.Write("judgeOffset", judgeOffset);
        writer.Write("MusicOffset", MusicOffset);
        writer.Write("NoteSize", NoteSize);

        writer.Write("sys_BGM", sys_BGM);
        writer.Write("sys_SE", sys_SE);
        writer.Write("game_BGM", game_BGM);
        writer.Write("game_TSE", game_TSE);
        writer.Write("game_SSE", game_SSE);

        // 変更を反映
        writer.Commit();
    }
}
