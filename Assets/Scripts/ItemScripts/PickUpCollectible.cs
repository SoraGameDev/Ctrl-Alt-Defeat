using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCollectible : MonoBehaviour
{
    public GameObject Collectible;
    public GameObject PickUpUI;

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
            Collectible.SetActive(false);
            PickUpUI.SetActive(false);
            GetComponent<Collectible>().AddToInventory();


        }
    }


    private void OnTriggerExit(Collider other)
    {
        PickUpUI.SetActive(false);
    }
}
