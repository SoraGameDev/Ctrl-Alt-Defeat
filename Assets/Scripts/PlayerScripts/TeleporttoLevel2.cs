using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporttoLevel2 : MonoBehaviour
{
    public GameObject player;
    public GameObject Level2Spawn;

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
        player.transform.position = Level2Spawn.transform.position;
        player.transform.rotation = Level2Spawn.transform.rotation;
    }
}
