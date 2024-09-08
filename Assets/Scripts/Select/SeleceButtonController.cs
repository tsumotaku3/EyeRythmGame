using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SeleceButtonController : MonoBehaviour
{
    //対応する楽曲番号
    public int MusicNumber;
    //選曲ウィンドウのアニメーター
    Animator windowAnim;

    //ボタンのImage
    Image myImage;
    //子のタイトル表示テキスト
    TextMeshProUGUI titleTMP;
    // Start is called before the first frame update
    void Start()
    {
        //アニメーターを取得
        windowAnim = GameObject.FindGameObjectWithTag("Eye").GetComponent<Animator>();
        //Imageを取得
        myImage = GetComponent<Image>();
        //TMPを取得
        titleTMP = transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        //ジャケットを取得&設定
        myImage.sprite = SelectButtonGenerator.Jackets[MusicNumber - 1];
        //タイトルを取得&設定
        titleTMP.text = SelectButtonGenerator.MusicList[MusicNumber - 1][0];
    }

    //ボタンが押された時の処理
    public void OnClick()
    {
        //アニメーションを有効化
        windowAnim.SetBool("isSelect",true);
        //楽曲番号を代入
        NotesGenerator.musicNum = MusicNumber.ToString("d3");
    }
}
