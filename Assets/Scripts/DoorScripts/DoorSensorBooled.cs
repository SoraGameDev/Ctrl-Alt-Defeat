using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSensorBooled : MonoBehaviour
{
    public Animator animator;
    public bool DoorLocked;
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
        if (DoorLocked == true)
        {
            animator.SetBool("YellowOpen", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (DoorLocked == true)
        {
            animator.SetBool("YellowOpen", false);
        }
    }
    public void UnlockDoor()
    {
        DoorLocked = true;
    }

    public void LockDoor()
    {
        DoorLocked = false;

    }
}
