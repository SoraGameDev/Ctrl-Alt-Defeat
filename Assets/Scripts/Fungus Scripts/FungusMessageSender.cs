using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
public class FungusMessageSender : MonoBehaviour
{
    public Flowchart flowchart;
    public string Message;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        flowchart.SendFungusMessage(Message);


    }
}
