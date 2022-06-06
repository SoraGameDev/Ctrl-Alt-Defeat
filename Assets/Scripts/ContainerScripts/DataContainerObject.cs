using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Script by Luke, amended by Kevin
/// 
/// NOTE OF CONCERN
/// For some reason, code in this script continues to execute after the gameObject has been destroyed.
/// This should not happen and I haven't been able to find a way to fix it. It is likely a Unity bug of some sort.
/// With that in mind, do not add any code in this script below/after a Destory function is called. It may cause issues later on.
/// -Kevin
/// </summary>

public class DataContainerObject : MonoBehaviour
{

    //       MOVEMENT       //
    Rigidbody body;
    public float speed;
    public Vector3 direction;
    public bool isMoving;

    //     DAMAGE STATS     //
    public float damageRange = 8;
    public float grenadeDamage = 100;

    //      DAMAGABLES     //
    public List<GameObject> enemiesToDamage = new List<GameObject>();

    public List<GameObject> enemiesSpawned = new List<GameObject>();

    void Start()
    {

        body = GetComponent<Rigidbody>();

        damageRange = 8;

        //Confirm box is moving from the get-go
        isMoving = true;

        //Assign the damage range to the size of the sphere collider (trigger)
        //damageRange = GetComponent<SphereCollider>().radius;

        enemiesSpawned = EnemiesSingleton.Instance.enemiesSpawned;

    }


    void Update()
    {

        //AddOrRemove();

    }

    private void FixedUpdate()
    {


        //Every frame, check if isMoving is true and if so, continue to translate the box in the given direction
        if (isMoving == true)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        //If a collision occurs and it is the ground, explode.
        //must later include all othe objects at ground level ie cages.
        if (collision.gameObject.name == "Ground")
        {

            AddOrRemove();

            Explode();

        }
        else if (collision.gameObject.name == "Destroy Trigger")
        {
            Destroy(gameObject);
        }

    }

    public void Drop()
    {
        isMoving = false;
        body.isKinematic = false;
        body.AddForce(0, -1000f, 0, ForceMode.Force);

    }

    void Explode()
    {

        DealDamage();
        Debug.Log("Damage Dealt");

        Destroy(gameObject);
    }

    void DealDamage()
    {

        //Find the distance to each enemy and apply damage appropriatly (grenade effect)
        foreach (GameObject enemy in enemiesToDamage)
        {
            RaycastHit hit;
            Ray ray = new Ray();

            //Find the distance to the enemy in question
            ray.origin = transform.position;
            ray.direction = enemy.transform.position - transform.position;

            if (Physics.Raycast(ray, out hit))
            {

                if (hit.collider.gameObject == enemy)
                {
                    float objectDistance = hit.distance;

                    float damageInflicted = (1 - (objectDistance / damageRange)) * grenadeDamage;

                    enemy.GetComponent<VirusEnemyStats>().TakeDamage(damageInflicted);

                }
            }
        }


    }

    void AddOrRemove()
    {
        foreach (GameObject enemy in EnemiesSingleton.Instance.enemiesSpawned)
        {

            RaycastHit hit;
            Ray ray = new Ray();

            ray.origin = transform.position;
            ray.direction = enemy.transform.position - transform.position;



            if (Physics.Raycast(ray, out hit))
            {

                if (Vector3.Distance(transform.position, enemy.transform.position) < damageRange && !enemiesToDamage.Contains(enemy))
                {
                    enemiesToDamage.Add(enemy);
                    Debug.Log("Added Here");
                    //DealDamage();
                    //Debug.Log("Damage Dealt here");
                }
                else if (Vector3.Distance(transform.position, enemy.transform.position) >= damageRange)
                {
                    if (enemiesToDamage.Contains(enemy))
                    {
                        enemiesToDamage.Remove(enemy);
                    }
                }
            }


            /*if (Vector3.Distance(transform.position, enemy.transform.position) < damageRange && !enemiesToDamage.Contains(enemy)) 
            {
                enemiesToDamage.Add(enemy);
            }
            else 
            {
                if (enemiesToDamage.Contains(enemy)) 
                {
                    enemiesToDamage.Remove(enemy);
                }
            }*/
        }

    }


    /*
    private void OnTriggerStay(Collider other)
    {

        if(other.gameObject.tag == "Enemy" && !enemiesToDamage.Contains(other.gameObject))
        {
            enemiesToDamage.Add(other.gameObject);
        }
    }*/



}