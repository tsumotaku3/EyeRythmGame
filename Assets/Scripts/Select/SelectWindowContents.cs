using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;

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
    //AudioSource
    AudioSource myAudioSource;

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    public float delta;
    // Update is called once per frame
    void Update()
    {
        //���낢��ݒ肷��
        myImage.sprite = SelectButtonGenerator.Jackets[int.Parse(NotesGenerator.musicNum) - 1];
        titleTMP.text = SelectButtonGenerator.MusicList[int.Parse(NotesGenerator.musicNum) - 1][0];
        subTMP.text = SelectButtonGenerator.MusicList[int.Parse(NotesGenerator.musicNum) - 1][1] + " BPM:" + SelectButtonGenerator.MusicList[int.Parse(NotesGenerator.musicNum) - 1][2];
        for (int i = 0; i < 4; i++) difficultyTMPs[i].text = SelectButtonGenerator.MusicList[int.Parse(NotesGenerator.musicNum) - 1][3 + i];

        //���y�֌W�̂��
        if (delta > 0)
        {
            if (delta > 18)
            {
                myAudioSource.volume = Mathf.Lerp(0, 0.1f,(20 - delta) / 2);
            }else if (delta < 2)
            {
                myAudioSource.volume = Mathf.Lerp(0, 0.1f, delta / 2);
            }
            delta -= Time.deltaTime;
        }
        else
        {
            delta = 20;
            myAudioSource.time = float.Parse(SelectButtonGenerator.MusicList[int.Parse(NotesGenerator.musicNum) - 1][7]);
        }
    }

    public async void OnOpen()
    {
        //�Ȃ����[�h
        var musicHandle = Addressables.LoadAssetAsync<AudioClip>("m_" + NotesGenerator.musicNum);
        var music = await musicHandle.Task;
        myAudioSource.clip = music;
        myAudioSource.Play();
        delta = 20; myAudioSource.time = float.Parse(SelectButtonGenerator.MusicList[int.Parse(NotesGenerator.musicNum) - 1][7]);
    }
    public async void OnClose()
    {
        //�Ȃ������[�X
        Addressables.Release("m_" + NotesGenerator.musicNum);
        delta = 2;
        await Task.Delay(2000);
        myAudioSource.Pause();
    }
}
