using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class OptionsGenerator : MonoBehaviour
{
    //�I��UI�̃v���n�u�̃X�N���v�g
    public SeleceButtonController buttons;
    //�Ȃ̃��X�g
    List<string[]> musicList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //csv�����[�h����
    void LoadCSV()
    {
        //CSV�̒��g���i�[�AStringReader�ɕϊ�
        TextAsset scoreCSV = Resources.Load("csv/musicList") as TextAsset;
        StringReader reader = new StringReader(scoreCSV.text);
        //�ǂݎ��
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            musicList.Add(line.Split(','));
        }
    }
}
