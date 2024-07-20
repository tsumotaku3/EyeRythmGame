using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class OptionsGenerator : MonoBehaviour
{
    //選択UIのプレハブのスクリプト
    public SeleceButtonController buttons;
    //曲のリスト
    List<string[]> musicList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //csvをロードする
    void LoadCSV()
    {
        //CSVの中身を格納、StringReaderに変換
        TextAsset scoreCSV = Resources.Load("csv/musicList") as TextAsset;
        StringReader reader = new StringReader(scoreCSV.text);
        //読み取る
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            musicList.Add(line.Split(','));
        }
    }
}
