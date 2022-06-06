using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPickUpUI : MonoBehaviour
{

    public GameObject pickupui;
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
        pickupui.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        pickupui.SetActive(false);
    }
}
