using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JudgeTextSystem : MonoBehaviour
{
    //AudioSorce‚Æƒm[ƒc‰¹
    AudioSource[] audioSources;
    public AudioClip[] NoteSE = new AudioClip[2];

    private void Start()
    {
        transform.GetChild(NotesGenerator.Combo_type).gameObject.SetActive(true);
        transform.GetChild(NotesGenerator.Combo_type).gameObject.GetComponent<TextMeshPro>().text = NotesGenerator.Combo.ToString();
    }

    //‰¹‚ğ–Â‚ç‚·ŠÖ”
    public void PlaySound(int Judge)
    {
        //AudioSorce‚ğæ“¾
        audioSources = GameObject.FindGameObjectWithTag("NotesSEGenerator").GetComponents<AudioSource>();
        //‰¹‚ğ–Â‚ç‚·
        audioSources[1].PlayOneShot(NoteSE[Judge]);
    }
    //©g‚ğ”j‰ó‚·‚é‚¾‚¯‚Ì”ß‚µ‚¢ŠÖ”
    public void destroy()
    {
        Destroy(gameObject);
    }
}
