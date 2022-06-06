using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class WinScript : MonoBehaviour
{
    public Flowchart winFlowchart;

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
        winFlowchart.SendFungusMessage("BossDead");
        Cursor.lockState = CursorLockMode.None;
    }
}
