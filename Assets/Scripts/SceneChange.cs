using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    //Animator
    Animator myAnimator;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    //ÉVÅ[ÉìÇïœÇ¶ÇÈä÷êî
    public void DoSceneChange(string sceneName)
    {
        StartCoroutine(LoadScene(0,sceneName));
    }

    public IEnumerator LoadScene(float delay,string sceneName)
    {
        yield return new WaitForSeconds(delay);
        myAnimator.SetBool("isChange", true);
        yield return new WaitForSeconds(1);
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        while (!async.isDone)
        {
            yield return null;
        }
    }
}
