using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.UI;
public class ZippingStation : MonoBehaviour {

    public GameObject player;
    public Flowchart flowchart;
    public GameObject canvas;
    public bool zippingStart;
    public bool enemiesdefeated;
    public bool zippingcompleted;
    public List<GameObject> spawnpoints;
    public GameObject enemyPrefab;
    public List<GameObject> enemies;
	// Use this for initialization
	void Start () {
        enemiesdefeated = false;
        zippingcompleted = false;
        zippingStart = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (zippingStart == true)
        {
            zippingStart = false;
            for (int i = 0; i < 4; i++)
            {
                GameObject enemy = GameObject.Instantiate(enemyPrefab);

                int randomSpawn = Random.Range(0, spawnpoints.Count);

                Vector3 spawnPosition = spawnpoints[randomSpawn].transform.position;

                spawnPosition += new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2));

                enemy.transform.position = spawnPosition;

                enemies.Add(enemy);

            }
        }

        if(enemies.Count == 0)
        {

            enemiesdefeated = true;
        }
        else
        {
            enemiesdefeated = false;
        }

        if(enemiesdefeated == true && zippingcompleted == true)
        {
            zippingcompleted = false;
            flowchart.SendFungusMessage("zippingcomplete");
            flowchart.StopBlock("ZippingStart");
        }

	}


    private void OnTriggerStay(Collider player)
    {

        canvas.SetActive(true);

        if (Input.GetKey(KeyCode.E))
        {
            flowchart.SendFungusMessage("zippingstarted");
            Destroy(canvas);

            zippingStart = true;
            zippingcompleted = true;
        }


    }

    private void OnTriggerExit(Collider player)
    {
        canvas.SetActive(false);
    }

    
}
