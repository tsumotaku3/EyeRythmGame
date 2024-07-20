using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JudgeTextSystem : MonoBehaviour
{
    //AudioSorce�ƃm�[�c��
    AudioSource[] audioSources;
    public AudioClip[] NoteSE = new AudioClip[2];

    private void Start()
    {
        transform.GetChild(NotesGenerator.Combo_type).gameObject.SetActive(true);
        transform.GetChild(NotesGenerator.Combo_type).gameObject.GetComponent<TextMeshPro>().text = NotesGenerator.Combo.ToString();
    }

    //����炷�֐�
    public void PlaySound(int Judge)
    {
        //AudioSorce���擾
        audioSources = GameObject.FindGameObjectWithTag("NotesSEGenerator").GetComponents<AudioSource>();
        //����炷
        audioSources[1].PlayOneShot(NoteSE[Judge]);
    }
    //���g��j�󂷂邾���̔߂����֐�
    public void destroy()
    {
        Destroy(gameObject);
    }
}
