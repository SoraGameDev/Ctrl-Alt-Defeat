using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargeStation : MonoBehaviour
{
    public GameObject PickUpUI;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other) {

        PickUpUI.SetActive(true);
        if(Input.GetKeyDown("e")) {

            player.GetComponent<PlayerStats>().ammo = player.GetComponent<PlayerStats>().maxAmmo;
            player.GetComponent<PlayerStats>().health = player.GetComponent<PlayerStats>().maxHealth;
            SoundManager.Instance.PlayClip("healthregen");
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        PickUpUI.SetActive(false);
    }
}
