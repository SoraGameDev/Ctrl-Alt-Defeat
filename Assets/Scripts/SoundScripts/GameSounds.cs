using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSounds : MonoBehaviour
{

    public List<AudioClip> soundEffects;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.Initialise(soundEffects);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
