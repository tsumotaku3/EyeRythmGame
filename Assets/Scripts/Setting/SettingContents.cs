using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingContents : MonoBehaviour
{
    //値を表示するテキスト
    [SerializeField] TMP_Text v_text;
    //スライダー形式
    [SerializeField]
    Slider mySlider;
    //設定する項目名(保存する変数名と一緒)
    [SerializeField]
    string SettingName;

    //AudioMixer
    [SerializeField]
    AudioMixer mixer;
    //AudioSource
    [SerializeField]
    AudioSource myAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        switch (SettingName)
        {
            case "NoteSpeed":
                mySlider.value = SettingManager.NoteSpeed * 10;
                v_text.text = SettingManager.NoteSpeed.ToString("f1");
                break;
            case "NoteSize":
                mySlider.value = SettingManager.NoteSize * 10;
                v_text.text = SettingManager.NoteSize.ToString("f1");
                break;
            case "JudgeSpeed":
                mySlider.value = SettingManager.JudgeSpeed * 10;
                v_text.text = SettingManager.JudgeSpeed.ToString("f1");
                break;
            case "sys_BGM":
                mySlider.value = SettingManager.sys_BGM + 80;
                v_text.text = mySlider.value.ToString() + "%";
                mixer.SetFloat("sys_BGM",SettingManager.sys_BGM);
                break;
            case "sys_SE":
                mySlider.value = SettingManager.sys_SE + 80;
                v_text.text = mySlider.value.ToString() + "%";
                mixer.SetFloat("sys_SE", SettingManager.sys_SE);
                break;
            case "game_BGM":
                mySlider.value = SettingManager.game_BGM + 80;
                v_text.text = mySlider.value.ToString() + "%";
                mixer.SetFloat("game_BGM", SettingManager.game_BGM);
                break;
            case "game_TSE":
                mySlider.value = SettingManager.game_TSE + 80;
                v_text.text = mySlider.value.ToString() + "%";
                mixer.SetFloat("game_TSE", SettingManager.game_TSE);
                break;
            case "game_SSE":
                mySlider.value = SettingManager.game_SSE + 80;
                v_text.text = mySlider.value.ToString() + "%";
                mixer.SetFloat("game_SSE", SettingManager.game_SSE);
                break;
            case "JudgeOffset":
                v_text.text = SettingManager.JudgeOffset.ToString("f1");
                break;
            case "MusicOffset":
                v_text.text = SettingManager.MusicOffset.ToString("f1");
                break;
        }
    }

    float delta = 10;
    bool isfirst = true;
    // Update is called once per frame
    void Update()
    {
        delta += Time.deltaTime;
        //テスト音を鳴らす
        if(delta > myAudioSource.clip.length && isPlay)
        {
            if(!isfirst)myAudioSource.Play();
            delta = 0;
            isPlay = false;
            if(isfirst) isfirst = false;
        }
    }

    bool isPlay = false;
    //スライダーの値が変わったとき
    public void OnValueChange()
    {
        switch (SettingName)
        {
            case "NoteSpeed":
                SettingManager.NoteSpeed = mySlider.value / 10;
                v_text.text = SettingManager.NoteSpeed.ToString("f1");
                break;
            case "NoteSize":
                SettingManager.NoteSize = mySlider.value / 10;
                v_text.text = SettingManager.NoteSize.ToString("f1");
                break;
            case "JudgeSpeed":
                SettingManager.JudgeSpeed = mySlider.value / 10;
                v_text.text = SettingManager.JudgeSpeed.ToString("f1");
                break;
            case "sys_BGM":
                SettingManager.sys_BGM = mySlider.value - 80;
                v_text.text = mySlider.value.ToString()+ "%";
                mixer.SetFloat("sys_BGM", SettingManager.sys_BGM);
                break;
            case "sys_SE":
                SettingManager.sys_SE = mySlider.value - 80;
                v_text.text = mySlider.value.ToString() + "%";
                isPlay = true;
                mixer.SetFloat("sys_SE", SettingManager.sys_SE);
                break;
            case "game_BGM":
                SettingManager.game_BGM = mySlider.value - 80;
                v_text.text = mySlider.value.ToString() + "%";
                mixer.SetFloat("game_BGM", SettingManager.game_BGM);
                break;
            case "game_TSE":
                SettingManager.game_TSE = mySlider.value - 80;
                v_text.text = mySlider.value.ToString() + "%";
                isPlay = true;
                mixer.SetFloat("game_TSE", SettingManager.game_TSE);
                break;
            case "game_SSE":
                SettingManager.game_SSE = mySlider.value - 80;
                v_text.text = mySlider.value.ToString() + "%";
                isPlay = true;
                mixer.SetFloat("game_SSE", SettingManager.game_SSE);
                break;
        }
    }

    //ボタン調整タイプでボタンが押されたとき
    public void OnClick(float changeValue)
    {
        switch (SettingName)
        {
            case "JudgeOffset":
                SettingManager.JudgeOffset = Mathf.Clamp(SettingManager.JudgeOffset + changeValue, -10f,10f);
                v_text.text = SettingManager.JudgeOffset.ToString("f1");
                break;
            case "MusicOffset":
                SettingManager.MusicOffset = Mathf.Clamp(SettingManager.MusicOffset + changeValue, -10f, 10f);
                v_text.text = SettingManager.MusicOffset.ToString("f1");
                break;

        }
    }
}
