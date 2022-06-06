using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
public class FungusMessageSenderInteractable : MonoBehaviour
{
    public string message;
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
        if (Input.GetKey(KeyCode.E))
        {

            flowchart.SendFungusMessage(message);


        }
    }
}
