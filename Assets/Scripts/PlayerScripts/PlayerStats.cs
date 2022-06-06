using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public int health;
    public int ammo;

    public int maxHealth;
    public int maxAmmo;

    public bool isAlive = true;


    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        maxAmmo = ammo;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReducePlayerHealth()
    {
        health -= 10;
    }

    public void ReducePlayerAmmo()
    {
        ammo -= 1;
    }

    public void Die()
    {
        if (health <= 0)
        {
            Debug.Log("died, send to respawn point");
            isAlive = false;

        }
    }
}