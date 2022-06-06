using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIStates : MonoBehaviour

    
{

    NavMeshAgent nmAgent;

    public GameObject player;

    Vector3 lastSeenHere;

    EnemyLook lookScript;

    public Animator animator;

    public enum State 
    {
        IDLING,
        CHASING,
        ALERT,
        ATTACKING,
        DEAD
    }

    public State state;

    // Start is called before the first frame update
    void Start()
    {

        nmAgent = GetComponent<NavMeshAgent>();
        lookScript = GetComponent<EnemyLook>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.IDLING) 
        {
            lookScript.StayStill();
            if (lookScript.playerIsVisible == true) 
            {
                state = State.CHASING;
            }
        }
        else if (state == State.CHASING) 
        {
            if (lookScript.player != null) 
            {
                
                if (Vector3.Distance(transform.position, player.transform.position) > 2) 
                {
                    nmAgent.SetDestination(player.transform.position);

                }
                else 
                {
                    nmAgent.SetDestination(transform.position);
                    //Invoke("ReturnToChasing", 2);
                    state = State.ATTACKING;
                }
                //if (lookScript.playerIsVisible != true) 
                //{
                //    lastSeenHere = lookScript.player.transform.position;
                //    state = State.ALERT;
                //}
                //if (lookScript.playerIsVisible == true) 
                //{
                //    state = State.CHASING;
                //}
            }
            
            if (lookScript.playerIsVisible != true)
            {
                lastSeenHere = lookScript.player.transform.position;
                state = State.ALERT;
            }
            //if (lookScript.playerIsVisible == true) 
            //{
            //    state = State.CHASING;
            //}
            //else 
            //{
            //    state = State.IDLING;
            //}

        }
        else if (state == State.ALERT) 
        {
            

            if(lookScript.playerIsVisible == true) 
            {
                state = State.CHASING;
            }
            else 
            {
                Invoke("ReturnToIdling", 4);
            }
            nmAgent.SetDestination(lastSeenHere);
        }
        else if (state == State.ATTACKING) 
        {
            if(Vector3.Distance(transform.position, player.transform.position) > 2) 
            {
                state = State.IDLING;
            }
            
        }
        else if (state == State.DEAD) 
        {
            Destroy(gameObject);
        }
    }

    void ReturnToChasing() 
    {
        state = State.CHASING;
    }

    void ReturnToIdling() 
    {
        state = State.IDLING;
    }


    //The Following piece of code is responsible for the aatcak of the virus.
    //This decreases the health of the player.

    private void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.name == "Player") 
        {

            if (GetComponent<VirusEnemyStats>().attackLoaded == true) 
            {
                GetComponent<VirusEnemyStats>().attackLoaded = false;
                //Trigger animation here also

                other.gameObject.GetComponent<PlayerStats>().health -= 10;
                GetComponent<VirusEnemyStats>().Reload();
                Debug.Log("Reloaded..");
            }

        }
    }
}
