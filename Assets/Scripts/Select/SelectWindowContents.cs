using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AddressableAssets;

public class SelectWindowContents : MonoBehaviour
{
    //������Image
    [SerializeField]
    Image myImage;
    //�^�C�g���ƃT�u�^�C�g��
    [SerializeField]
    TextMeshProUGUI titleTMP, subTMP;
    //��Փx�\��
    [SerializeField]
    TextMeshProUGUI[] difficultyTMPs = new TextMeshProUGUI[4];

    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        //���낢��ݒ肷��
        myImage.sprite = SelectButtonGenerator.Jackets[int.Parse(NotesGenerator.musicNum) - 1];
        titleTMP.text = SelectButtonGenerator.MusicList[int.Parse(NotesGenerator.musicNum) - 1][0];
        subTMP.text = SelectButtonGenerator.MusicList[int.Parse(NotesGenerator.musicNum) - 1][1] + " BPM:" + SelectButtonGenerator.MusicList[int.Parse(NotesGenerator.musicNum) - 1][2];
        for (int i = 0; i < 4; i++) difficultyTMPs[i].text = SelectButtonGenerator.MusicList[int.Parse(NotesGenerator.musicNum) - 1][3 + i];
    }
}
