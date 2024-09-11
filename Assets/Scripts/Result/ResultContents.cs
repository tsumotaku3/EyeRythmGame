using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultContents : MonoBehaviour
{
    //”»’è‚©ƒXƒRƒA‚©
    [SerializeField]
    bool isJudge;
    //”»’è‚ÌŽí—Þ
    [SerializeField]
    int judge_type;
    //TMP
    [SerializeField]
    TextMeshProUGUI myTMP;
    // Start is called before the first frame update
    void Start()
    {
        if (isJudge)
        {
            myTMP.text = NotesGenerator.result[judge_type].ToString();
        }
        else
        {
            myTMP.text = (1000000 - (1000000 / NotesGenerator.result.Length * NotesGenerator.result[1] * 0.5f) - (1000000 / NotesGenerator.result.Length * NotesGenerator.result[2])).ToString("N0");
        }
    }
}
