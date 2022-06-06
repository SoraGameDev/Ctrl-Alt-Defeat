using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    int levelToLoad;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FadeToNextLevel ()
    {

        LevelFade(0);
    }


    public void LevelFade (int levelIndex)
    {
        animator.SetTrigger("FadeOut");
        
    }

    public void FadeFinished()
    {

        SceneManager.LoadScene(0);

    }
}
