using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomInitiation : MonoBehaviour
{
    //    ACTIVATED GAMEOBJECTS    //
    public GameObject boss;
    public GameObject containerSpawners;

    // Start is called before the first frame update
    void Start()
    {
        boss.SetActive(false);
        containerSpawners.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == "Player") {

            boss.SetActive(true);
            containerSpawners.SetActive(true);
        }
    }
}
