using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSenses : MonoBehaviour {

    public GameObject player;
    float fieldOfView = 114f;
    public bool canSeePlayer;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Look();
    }

    void Look()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, player.transform.position - transform.position);

        Debug.DrawRay(ray.origin, ray.direction, Color.red);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == player)
            {
                //If the player is within the field of view:
                if (CheckFieldOfView())
                {
                    Debug.DrawRay(ray.origin, ray.direction, Color.green);

                    canSeePlayer = true;
                    
                }
                else
                {
                    canSeePlayer = false;
                }

            }
            else
            {
                canSeePlayer = false;
            }
        }
    }

    bool CheckFieldOfView()
    {
        bool isInFieldOfView = true;

        Vector3 directionToPlayer = player.transform.position - transform.position;

        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        if(angle < fieldOfView/2)
        {
            isInFieldOfView = true;
        }
        else
        {
            isInFieldOfView = false;
        }

        return isInFieldOfView;
        
    }
}
