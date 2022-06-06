using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLook : MonoBehaviour {

    public GameObject player;

    NavMeshAgent nmAgent;

    public bool playerIsVisible;

    



	// Use this for initialization
	void Start () {

        nmAgent = GetComponent<NavMeshAgent>();
		
	}
	
	// Update is called once per frame
	void Update () {


        if(player != null) 
        {
            Look();
        }
        else 
        {
            StayStill();
        }
        

	}

    void Look()
    {
        RaycastHit hit;

        Vector3 playerDirection = player.transform.position - transform.position;

        Ray ray = new Ray(transform.position, playerDirection);

        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider.gameObject.name == "Player")
            {
                playerIsVisible = true;

                

            }
            else
            {
                playerIsVisible = false;
            }
        }
    }

    public void StayStill() 
    {
        nmAgent.SetDestination(transform.position);
    }
}
