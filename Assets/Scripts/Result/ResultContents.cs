using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResultContents : MonoBehaviour
{
    //判定かスコアかコンボか
    [SerializeField]
    int type;
    //判定の種類
    [SerializeField]
    int judge_type;
    //TMP
    [SerializeField]
    TextMeshProUGUI myTMP,TMP1,TMP2;
    //ジャケをいれるImage
    [SerializeField]
    Image jackImg;
    //フルコンorAP表示
    [SerializeField]
    GameObject[] eventText = new GameObject[2];
    //スコアのグラデーションプリセット
    [SerializeField]
    TMP_ColorGradient[] eventGrad = new TMP_ColorGradient[2];
    // Start is called before the first frame update
    void Awake()
    {
        if (type == 0)
        {
            myTMP.text = NotesGenerator.result[judge_type].ToString();
        }
        else if(type == 1)
        {
            int score = (int)(1000000 - (1000000 / (NotesGenerator.result[0] + NotesGenerator.result[1] + NotesGenerator.result[2]) * NotesGenerator.result[1] * 0.5f) - (1000000 / (NotesGenerator.result[0] + NotesGenerator.result[1] + NotesGenerator.result[2]) * NotesGenerator.result[2]));
            NotesGenerator.Score = score;
            myTMP.text = score.ToString("N0");
            string[] lank = {"SSS","SS+","SS","S+","S","AAA+","AAA","AA+","AA","A+","A","B","C"};
            for(int i = 0;i < 13; i++)
            {
                if (score == 1000000)
                {
                    TMP2.text = "SSS+";
                    break;
                }
                if (score >= 990000 - 10000 * i)
                {
                    TMP2.text = lank[i];
                    break;
                }
            }
            if (score < 870000)
            {
                TMP2.text = "D";
            }
            if (NotesGenerator.result[2] == 0)
            {
                if (NotesGenerator.result[1] == 0)
                {
                    eventText[0].SetActive(true);
                }
                else
                {
                    eventText[1].SetActive(true);
                }
            }
            if (score >= 950000)
            {
                TMP2.colorGradientPreset = eventGrad[0];
            }else if (score >= 890000)
            {
                TMP2.colorGradientPreset = eventGrad[1];
            }
        }else if (type == 2)
        {
            myTMP.text = NotesGenerator.BestCombo.ToString();
        }
        else if(type == 3)
        {
            myTMP.text = SelectButtonGenerator.MusicList[int.Parse(NotesGenerator.musicNum) - 1][0];
            switch (NotesGenerator.difficulty)
            {
                case 0:
                    TMP1.text = "Easy " + SelectButtonGenerator.MusicList[int.Parse(NotesGenerator.musicNum) - 1][3 + NotesGenerator.difficulty];
                    break;
                case 1:
                    TMP1.text = "Normal " + SelectButtonGenerator.MusicList[int.Parse(NotesGenerator.musicNum) - 1][3 + NotesGenerator.difficulty];
                    break;
                case 2:
                    TMP1.text = "Hard " + SelectButtonGenerator.MusicList[int.Parse(NotesGenerator.musicNum) - 1][3 + NotesGenerator.difficulty];
                    break;
                case 3:
                    TMP1.text = "Impossible " + SelectButtonGenerator.MusicList[int.Parse(NotesGenerator.musicNum) - 1][3 + NotesGenerator.difficulty];
                    break;
            }
            jackImg.sprite = SelectButtonGenerator.Jackets[int.Parse(NotesGenerator.musicNum) - 1];
        }
    }
}
