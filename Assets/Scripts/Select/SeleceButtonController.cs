using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class SeleceButtonController : MonoBehaviour
{
    public int MusicNumber;
    Animator windowAnim;
    // Start is called before the first frame update
    void Start()
    {
        windowAnim = GameObject.FindGameObjectWithTag("Eye").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        windowAnim.SetBool("isSelect",true);
    }
}
