using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSensor : MonoBehaviour
{
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        animator.SetBool("YellowOpen", true);
        //SoundManager.Instance.PlayClip("dooropen");
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("YellowOpen", false);
    }
}
