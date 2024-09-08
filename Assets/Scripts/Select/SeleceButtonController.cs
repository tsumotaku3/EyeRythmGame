using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SeleceButtonController : MonoBehaviour
{
    //�Ή�����y�Ȕԍ�
    public int MusicNumber;
    //�I�ȃE�B���h�E�̃A�j���[�^�[
    Animator windowAnim;

    //�{�^����Image
    Image myImage;
    //�q�̃^�C�g���\���e�L�X�g
    TextMeshProUGUI titleTMP;
    // Start is called before the first frame update
    void Start()
    {
        //�A�j���[�^�[���擾
        windowAnim = GameObject.FindGameObjectWithTag("Eye").GetComponent<Animator>();
        //Image���擾
        myImage = GetComponent<Image>();
        //TMP���擾
        titleTMP = transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        //�W���P�b�g���擾&�ݒ�
        myImage.sprite = SelectButtonGenerator.Jackets[MusicNumber - 1];
        //�^�C�g�����擾&�ݒ�
        titleTMP.text = SelectButtonGenerator.MusicList[MusicNumber - 1][0];
    }

    //�{�^���������ꂽ���̏���
    public void OnClick()
    {
        //�A�j���[�V������L����
        windowAnim.SetBool("isSelect",true);
        //�y�Ȕԍ�����
        NotesGenerator.musicNum = MusicNumber.ToString("d3");
    }
}
