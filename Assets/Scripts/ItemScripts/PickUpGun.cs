using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;
public class PickUpGun : MonoBehaviour   
{
    public Flowchart flowchart;
    public GameObject PickUpUI;
    public GameObject Gun;
    public GameObject GunOnFloor;
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
        PickUpUI.SetActive(true);

        if (Input.GetKeyDown(KeyCode.E))
        {

            GunOnFloor.SetActive(false);
            Gun.SetActive(true);
            PickUpUI.SetActive(false);
            GetComponent<Collectible>().AddToInventory();
            flowchart.SendFungusMessage("GunReceived");
        }

    }

    private void OnTriggerExit(Collider other)
    {

        PickUpUI.SetActive(false);
    }

}
