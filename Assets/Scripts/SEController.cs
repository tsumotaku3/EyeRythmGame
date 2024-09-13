using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEController : MonoBehaviour
{
    //AudioSource
    AudioSource myAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    public void OnClick(AudioClip clip)
    {
        myAudioSource.PlayOneShot(clip);
    }
}
