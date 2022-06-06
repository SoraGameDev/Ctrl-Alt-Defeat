using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour {

    //        HEALTH VALUES        //
    public float maxHealth;
    public float currentHealth;

    //   BOSS DEFEATED: REFERENCES   //
    public GameObject goldKeyPrefab;
    public GameObject explosionParent;
    float minForceApplied = 3;
    float maxForceApplied = 5;

    //     DROPPED CONTAINERS    //
    public GameObject containerPrefab;
    public int containersDropped;
    public float radius;
    public List<GameObject> dataContainerList = new List<GameObject>();

    //    FINALE DOOR   //
    public Animator finalDoorAnimator;

    void Start () {

        currentHealth = maxHealth;
    }

	void Update () {
	
        if(currentHealth <= 0)
        {
            KillBoss();
        }
	}

    //Public function called from elsewhere when health must be depleated
    public void ReduceHealth(float reduceBy)
    {
        currentHealth -= reduceBy;
    }

    //When the boss is out of health, this function is called
    void KillBoss()
    {
        finalDoorAnimator.SetBool("BossDied", true);

        //Make the explosion
        foreach (ParticleSystem child in explosionParent.GetComponentsInChildren<ParticleSystem>()) {
            child.transform.SetParent(gameObject.transform.parent);
            child.Play();

            if(child.GetComponent<AudioSource>()) {
                child.GetComponent<AudioSource>().Play();
            }
        }

        FlingEatenContainers();

        GameObject goldKey = GameObject.Instantiate(goldKeyPrefab);
        goldKey.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        gameObject.SetActive(false);
    }


    void FlingEatenContainers()
    {

        float angle = 0;
        Vector3 centre = transform.position;
        
        for (int i = 0; i < containersDropped; i++)
        {

            angle += ((Mathf.PI * 2) / containersDropped);

            float x = Mathf.Sin(angle) * radius;
            float z = Mathf.Cos(angle) * radius;

            //Define the container's position on the circle
            Vector3 position = centre + new Vector3(x, 3, z);

            GameObject dataContainer = GameObject.Instantiate(containerPrefab);

            //Give container its position and a rotation facing the boss
            dataContainer.transform.position = position;
            dataContainer.transform.LookAt(transform);

            //Give container velocity, causing it to throw itself away from the boss (i.e. in the direction from the boss to the container)
            Vector3 directionToContainer = dataContainer.transform.position - transform.position;
            dataContainer.GetComponent<Rigidbody>().AddForce(directionToContainer * Random.Range(minForceApplied, maxForceApplied), ForceMode.Impulse);
            
        }

    }


}
