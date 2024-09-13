using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SelectButtonGenerator : MonoBehaviour
{
    //楽曲の情報とかを入れるリスト
    public static List<string[]> MusicList = new List<string[]>();
    //曲のジャケのリスト
    public static List<Sprite> Jackets = new List<Sprite>();

    //選曲ボタンのプレハブ
    [SerializeField] SeleceButtonController buttonPre;
    //フィールドのオブジェクト
    [SerializeField] GameObject contentsParent;
    // Start is called before the first frame update
    void Start()
    {
        //CSVをロード
        LoadData();
        
    }

    //CSVを読み取る
    async void LoadData()
    {
        //CSVの中身を格納、StringReaderに変換
        if (MusicList.Count == 0)
        {
            var csvHandle = Addressables.LoadAssetAsync<TextAsset>("musicList");
            TextAsset musicCSV = await csvHandle.Task;
            Addressables.Release(csvHandle);
            StringReader reader = new StringReader(musicCSV.text);
            //読み取る
            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                MusicList.Add(line.Split(','));
            }
        }
        //同様にジャケ絵を取得
        if (Jackets.Count == 0)
        {
            for (int i=1;i<=MusicList.Count;i++)
            {
                var jacketHandle = Addressables.LoadAssetAsync<Sprite>("j_" + i.ToString("d3"));
                var spr = await jacketHandle.Task;
                Jackets.Add(spr);
            }
        }
        //CSVをもとにボタンを生成
        for (int i = 0; i < MusicList.Count; i++)
        {
            SeleceButtonController obj = Instantiate(buttonPre,contentsParent.transform);
            obj.MusicNumber = i + 1;
        }
    }
}
