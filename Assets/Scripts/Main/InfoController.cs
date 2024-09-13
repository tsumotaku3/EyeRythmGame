using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoController : MonoBehaviour
{
    //ジャケ絵のImage
    [SerializeField]
    Image jackImg;
    //タイトル、コンポーザー、難易度表示のTMP
    [SerializeField]
    TextMeshProUGUI titleTMP, compTMP, difTMP;
    // Start is called before the first frame update
    void Start()
    {
        jackImg.sprite = SelectButtonGenerator.Jackets[int.Parse(NotesGenerator.musicNum) - 1];
        titleTMP.text = SelectButtonGenerator.MusicList[int.Parse(NotesGenerator.musicNum) - 1][0];
        compTMP.text = SelectButtonGenerator.MusicList[int.Parse(NotesGenerator.musicNum) - 1][1];
        switch (NotesGenerator.difficulty)
        {
            case 0:
                difTMP.text = "Easy " + SelectButtonGenerator.MusicList[int.Parse(NotesGenerator.musicNum) - 1][3 + NotesGenerator.difficulty];
                break;
            case 1:
                difTMP.text = "Normal " + SelectButtonGenerator.MusicList[int.Parse(NotesGenerator.musicNum) - 1][3 + NotesGenerator.difficulty];
                break;
            case 2:
                difTMP.text = "Hard " + SelectButtonGenerator.MusicList[int.Parse(NotesGenerator.musicNum) - 1][3 + NotesGenerator.difficulty];
                break;
            case 3:
                difTMP.text = "Impossible " + SelectButtonGenerator.MusicList[int.Parse(NotesGenerator.musicNum) - 1][3 + NotesGenerator.difficulty];
                break;
        }
    }
}
