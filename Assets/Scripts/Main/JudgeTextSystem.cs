using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JudgeTextSystem : MonoBehaviour
{
    //AudioSorceとノーツ音
    AudioSource[] audioSources;
    public AudioClip[] NoteSE = new AudioClip[2];
    private void Start()
    {
        transform.GetChild(NotesGenerator.Combo_type).gameObject.SetActive(true);
        transform.GetChild(NotesGenerator.Combo_type).gameObject.GetComponent<TextMeshPro>().text = NotesGenerator.Combo.ToString();
        GetComponent<Animator>().speed = 1 / SettingManager.JudgeSpeed;
    }

    //音を鳴らす関数
    public void PlaySound(int Judge)
    {
        //AudioSorceを取得
        audioSources = GameObject.FindGameObjectWithTag("NotesSEGenerator").GetComponents<AudioSource>();
        //音を鳴らす
        audioSources[1].PlayOneShot(NoteSE[Judge]);
    }
    //自身を破壊するだけの悲しい関数
    public void destroy()
    {
        Destroy(gameObject);
    }
}
