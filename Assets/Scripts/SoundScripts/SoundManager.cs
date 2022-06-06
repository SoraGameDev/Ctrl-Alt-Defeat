using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    private static SoundManager instance;
    public static SoundManager Instance {

        get {

            return instance ?? (instance = new GameObject("SoundManager").AddComponent<SoundManager>());
        }
    }

    public List<AudioClip> soundEffects = new List<AudioClip>();

    AudioSource audioSource;

    public void Initialise(List<AudioClip> clipsIn) 
    {
        soundEffects = clipsIn;
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayClip(string clipname) 
    {
        foreach(AudioClip clip in soundEffects) 
        {
            if(clip.name == clipname) 
            {
                audioSource.PlayOneShot(clip);
            }
        }
    }
}
