using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class VirusEnemySpawner : MonoBehaviour
{

    //public GameObject virusEnemyPrefab;

    public GameObject spawnerPlayer;

    public List<GameObject> virusEnemyTypes = new List<GameObject>();

    public Flowchart flowchart;
        
    public bool isZipping = false;


    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("ZipSwitch", 0 ,1);

        EnemiesSingleton.Instance.spawnAnotherWave += ZipSwitch;
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if(EnemiesSingleton.Instance.WavesEnded == true)
        {

            flowchart.SendFungusMessage("WavesEnded");

        }
        //ZipSwitch();
       // SwitchOnOff();

        /*foreach (GameObject enemy in allEnemies) 
        {
            if(enemy.GetComponent<VirusEnemyStats>().health <= 0) 
            {
                allEnemies.Remove(enemy);
            } 
            
        }*/
    }

    void SpawnVirusEnemy() 
    {
        int randomIndex = Random.Range(0, virusEnemyTypes.Count);

        GameObject virusEnemy = GameObject.Instantiate(virusEnemyTypes[randomIndex]);
        virusEnemy.transform.position = transform.position;
        virusEnemy.name = "Virus Enemy";
        
        virusEnemy.GetComponent<EnemyLook>().player = spawnerPlayer;
        virusEnemy.GetComponent<EnemyAIStates>().player = spawnerPlayer;

        EnemiesSingleton.Instance.AddEnemy(virusEnemy);
        
    }

 

    //ZipSwitch spawns a single enemy
    void ZipSwitch() 
    {
        //if(isZipping == true) 
        {
            for (int i = 0; i < EnemiesSingleton.Instance.currentWave * 2; i++)
            {

                SpawnVirusEnemy();
                isZipping = false;
            }
        }
           
    }

   public void SwitchOnOff()
    {
       
        
            //isZipping = true;
            ZipSwitch();
            
        
    }

    
}
