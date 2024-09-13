using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SelectButtonGenerator : MonoBehaviour
{
    //�y�Ȃ̏��Ƃ������郊�X�g
    public static List<string[]> MusicList = new List<string[]>();
    //�Ȃ̃W���P�̃��X�g
    public static List<Sprite> Jackets = new List<Sprite>();

    //�I�ȃ{�^���̃v���n�u
    [SerializeField] SeleceButtonController buttonPre;
    //�t�B�[���h�̃I�u�W�F�N�g
    [SerializeField] GameObject contentsParent;
    // Start is called before the first frame update
    void Start()
    {
        //CSV�����[�h
        LoadData();
        
    }

    //CSV��ǂݎ��
    async void LoadData()
    {
        //CSV�̒��g���i�[�AStringReader�ɕϊ�
        if (MusicList.Count == 0)
        {
            var csvHandle = Addressables.LoadAssetAsync<TextAsset>("musicList");
            TextAsset musicCSV = await csvHandle.Task;
            Addressables.Release(csvHandle);
            StringReader reader = new StringReader(musicCSV.text);
            //�ǂݎ��
            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                MusicList.Add(line.Split(','));
            }
        }
        //���l�ɃW���P�G���擾
        if (Jackets.Count == 0)
        {
            for (int i=1;i<=MusicList.Count;i++)
            {
                var jacketHandle = Addressables.LoadAssetAsync<Sprite>("j_" + i.ToString("d3"));
                var spr = await jacketHandle.Task;
                Jackets.Add(spr);
            }
        }
        //CSV�����ƂɃ{�^���𐶐�
        for (int i = 0; i < MusicList.Count; i++)
        {
            SeleceButtonController obj = Instantiate(buttonPre,contentsParent.transform);
            obj.MusicNumber = i + 1;
        }
    }
}
