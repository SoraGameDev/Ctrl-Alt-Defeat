using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataContainerExploder : MonoBehaviour
{
    //      DAMAGABLES     //
    public List<GameObject> enemiesToDamage = new List<GameObject>();

    //     DAMAGE STATS     //
    public float damageRange;
    public float grenadeDamage = 100;

    public GameObject theParent;


    // Start is called before the first frame update
    void Start()
    {

        //Assign the damage range to the size of the sphere collider (trigger)
        damageRange = theParent.GetComponent<SphereCollider>().radius;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Explode()
    {
        DealDamage();
        Destroy(theParent);
    }

    void DealDamage()
    {

        //Find the distance to each enemy and apply damage appropriatly (grenade effect)
        foreach (GameObject enemy in enemiesToDamage)
        {
            RaycastHit hit;
            Ray ray = new Ray();

            //Find the distance to the enemy in question
            ray.origin = theParent.transform.position;
            ray.direction = enemy.transform.position - theParent.transform.position;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == enemy)
                {
                    float objectDistance = hit.distance;

                    if (objectDistance < damageRange)
                    {
                        Debug.Log(objectDistance);
                        Debug.Log(damageRange);

                        float damageInflicted = (1 - (objectDistance / damageRange)) * grenadeDamage;
                        Debug.Log(damageInflicted);

                        enemy.GetComponent<BossHealth>().ReduceHealth(damageInflicted);
                    }

                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Destroy Trigger")
        {
            Destroy(theParent);
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Enemy" && !enemiesToDamage.Contains(other.gameObject))
        {
            enemiesToDamage.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Enemy" && enemiesToDamage.Contains(other.gameObject))
        {
            enemiesToDamage.Remove(other.gameObject);
        }
    }
}
