using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossStates : MonoBehaviour {

    //     NAVIGATION     //
    public Transform centrePosition;
    NavMeshAgent agent;
    bool atCentre;

    //      STATES      //
    BossSenses senses;
    public State state;
    bool canSeePlayer;

    //     STATISTICS     //
    int normalSpeed = 7;
    int attackSpeed = 80;
    int normalAcceleration = 8;
    int attackAcceleration = 30;
    float turnSpeed = 0.5f;
    bool turnDirectionChosen;
    int randomNumber;
    int damageInflicted = 20;
    float kickForce = 50f;
    float kickRange = 7f;
    bool canPush = true;

    //    PLAYER RELATED    //
    public GameObject player;
   public bool targetChosen;
    Vector3 lastKnownPosition;

    //   PARTICLE EFFECTS   //
    ParticleSystem smokeTrail;
    bool playingSmoke;


    //    INVOKE SECURITY    //
    // i.e. To make sure a function invoked earlier doesn't play a later time even if the conditions allow it
    float randomInvokeID = 0f;
    float cancelAttackTime = 10f;

    //     ANIMATION     //
    public Animator animator;
    bool readyToLaugh;

    public enum State
    {
        SEARCHING,
        TRACKING,
        ATTACKING,
        RETURNING,
        RESETTING

        /* Description of States:
        SEARCHING: Trying to spot the player
        TRACKING: Has spotted the player, readying to attack
        ATTACKING: Running towards the player
        RETURNING: Can not find the player, traveling to centre of map
        */
    }
    
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        senses = GetComponent<BossSenses>();

        smokeTrail = transform.Find("SmokeTrail").GetComponent<ParticleSystem>();
        smokeTrail.Stop();

        agent.speed = normalSpeed;
        agent.acceleration = normalAcceleration;
    }
	
	void Update () {


        //         DEAL  DAMAGE        //

        //Damage the player and send them flying
        if (senses.canSeePlayer == true && (state == State.ATTACKING || state == State.TRACKING) && (Vector3.Distance(transform.position, player.transform.position) < kickRange)) {

            if (canPush == true)
            {
                //Stop the boss from going inside the player
                agent.SetDestination(transform.position);

                canPush = false;

                player.GetComponent<Rigidbody>().velocity = Vector3.zero;

                //Apply a force to the player in the direction of the player relative to the boss (back the way they came)
                player.transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
                player.GetComponent<Rigidbody>().AddForce((new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position) * kickForce, ForceMode.Impulse);

                //Damage the player
                player.GetComponent<PlayerStats>().health -= damageInflicted;

                //Laugh after hitting
                animator.SetTrigger("laugh");
                animator.SetBool("attacking", false);

                //Take a pause between hits to avoid applying too much force to the player at once through multiple hits
                Invoke("allowPushing", 2f);
            }
        }

        // Constantly check if the boss can see the player. If it can (and its not already looking at/for the player), begin tracking the player
        else if (senses.canSeePlayer == true && (state == State.SEARCHING || state == State.RETURNING)) {
            agent.SetDestination(transform.position);
            turnDirectionChosen = false;
            state = State.TRACKING;
        }



        //            STATES           //

        if (state == State.SEARCHING)
        {
            animator.SetBool("attacking", false);

            //Reset targetChosen
            targetChosen = false;

            //Decide whether to turn left or right when in the centre of the map
            if (turnDirectionChosen == false)
            {
                randomNumber = Random.Range(0, 2);
                turnDirectionChosen = true;
            }

            if (randomNumber == 0)
            {
                 //Continuously turn left
                 transform.Rotate(Vector3.up, -100f * Time.deltaTime);
            }

            else
            {
                //Continuously turn right
                transform.Rotate(Vector3.up, 100f * Time.deltaTime);
            }
            
        }
        
        else if(state == State.TRACKING)
        {
            if (senses.canSeePlayer == true)
            {
                animator.SetBool("attacking", true);

                //Locking the y axis of the player's direction to stop the boss from tilting up and down
                Vector3 directionToPlayer = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position;

                //Constantly rotate to face the direction of the player
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(directionToPlayer), Time.deltaTime * 200f);

                if (targetChosen == false) {
                    targetChosen = true;

                   
                    randomInvokeID += 1;
                    StartCoroutine(AttackCharged(randomInvokeID));
                }
            }
            else
            {

                state = State.RETURNING;
            }
        }
        
        else if(state == State.ATTACKING)
        {
            animator.SetBool("attacking", true);

            //Ensure the attack cancels itself if the boss is not reaching the designated position
            StartCoroutine(AttackTimedOut(randomInvokeID));

            //Increasing boss speed and accleration for faster attack
            agent.speed = attackSpeed;
            agent.acceleration = attackAcceleration;

            //Start the smoke trail as the boss runs
            if(playingSmoke == false)
            {
                playingSmoke = true;
                smokeTrail.gameObject.SetActive(true);
                smokeTrail.Play();
            }

            agent.SetDestination(lastKnownPosition);

            if (Vector3.Distance(transform.position, lastKnownPosition) < kickRange)
            {
                agent.SetDestination(transform.position);

                //Stop the smoke trail when the boss reaches the target position
                smokeTrail.Stop();
                playingSmoke = false;

                /*  INCLUDE BELOW IF YOU WANT A RANDOM LAUGHT EVERY FEW ATTACKS
                int laughOrNot = Random.Range(0, 5);

                if(readyToLaugh == true)
                {
                    readyToLaugh = false;

                    if(laughOrNot == 1)
                    {
                        animator.SetTrigger("laugh");
                        animator.SetBool("attacking", false);
                    }
                }
                */

                Invoke("ReturnToCentre", 3);
            }

        }

        else if(state == State.RETURNING)
        {
            animator.SetBool("attacking", false);

            //Reset targetChosen
            targetChosen = false;

            //Set destination to the centre of the map
            agent.SetDestination(centrePosition.position);

            if (senses.canSeePlayer == true)
            {
                agent.SetDestination(transform.position);
                state = State.TRACKING;
            }

            //Return to searching
            atCentre = true;
            state = State.SEARCHING;
        }

        else if (state == State.RESETTING) {

            animator.SetBool("attacking", false);

            //Reset statistics and effects
            agent.speed = normalSpeed;
            agent.acceleration = normalAcceleration;
            smokeTrail.Stop();
            playingSmoke = false;

            //Set destination to the centre of the map regardless of whether the player can be seen or not
            agent.SetDestination(centrePosition.position);

            //Return to searching when at centre position
            if (Vector3.Distance(transform.position, centrePosition.position) < 5) {
                atCentre = true;
                state = State.SEARCHING;
            }
        }
    }


    //          COROUTINES           //

    private IEnumerator AttackCharged(float givenID) {

        yield return new WaitForSeconds(1.5f);

        //Check if the pin has been overwritten, i.e. abort this particular function call
        if (givenID == randomInvokeID && senses.canSeePlayer == true)
        {
            //Designate the target position and attack
            lastKnownPosition = player.transform.position;
            readyToLaugh = true;
            state = State.ATTACKING;

        }
        else
        {
           // Debug.Log(givenID + " does not equal " + randomInvokeID);
        }

        yield break;
    }


    //The purpose of this function is to cancel an attack if the boss is not reaching the desired position, bug prevention
    private IEnumerator AttackTimedOut(float givenID) {

        yield return new WaitForSeconds(cancelAttackTime);

        //If the boss is attacking and its the same attack as x seconds ago
        if (givenID == randomInvokeID && state == State.ATTACKING)
        {
            //Cancel the attack
            state = State.RESETTING;
        }

        yield break;
    }


    void ReturnToCentre()
    {
        //Reset boss speed stats
        agent.speed = normalSpeed;
        agent.acceleration = normalAcceleration;

        targetChosen = false;

        state = State.RETURNING;
    }


    void allowPushing()
    {
        canPush = true;
    }
}
