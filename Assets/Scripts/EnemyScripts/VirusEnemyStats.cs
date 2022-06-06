using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusEnemyStats : MonoBehaviour
{
    public float health;

    public bool attackLoaded = true;
    public float attackReloadTime;

    public VirusEnemySpawner virsuenemyspawner;
    private VirusEnemySpawner virusenemyspawner;



    // Use this for initialization
    void Start() {

        virusenemyspawner = GetComponent<VirusEnemySpawner>();

    }

    // Update is called once per frame
    void Update() {

        KillEnemy();
    }

    void KillEnemy() {
        if (health <= 0)
        {
            //RemoveFromList();
            //Debug.Log("Enemy removed from list");

            EnemiesSingleton.Instance.RemoveEnemy(gameObject);

            Destroy(gameObject);
            Debug.Log("Enemy destroyed...");

        }
    }

    

    public void TakeDamage(float damage) {
        health = health - damage;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name == "Bullet") {
            TakeDamage(25);
        }
    }

    public void Reload() 
    {
        Invoke("ReloadProcess", attackReloadTime);
    }

    void ReloadProcess() 
    {
        attackLoaded = true;
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.name == "Player") {

            if (attackLoaded == true) {
                attackLoaded = false;
                //Trigger animation here also

                other.gameObject.GetComponent<PlayerStats>().health -= 10;

            }

        }
    }
}
