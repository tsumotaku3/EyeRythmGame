using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButtonController : MonoBehaviour
{
    Animator windowAnim;
    private void Start()
    {
        windowAnim = GameObject.FindGameObjectWithTag("Eye").GetComponent<Animator>();
    }

    public void Onclick()
    {
        windowAnim.SetBool("isSelect",false);
    }
}
