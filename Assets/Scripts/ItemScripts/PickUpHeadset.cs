using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
public class PickUpHeadset : MonoBehaviour
{
    public GameObject headset;
    public GameObject PlayerUI;
    public GameObject PickUpUI;
    public Flowchart flowchart;

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
            headset.SetActive(false);
            GetComponent<Collectible>().AddToInventory();
            PlayerUI.SetActive(true);
            PickUpUI.SetActive(false);
            flowchart.SendFungusMessage("HeadsetReceived");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PickUpUI.SetActive(false);
    }

}
