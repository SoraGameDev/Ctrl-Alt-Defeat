using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour {
    
    public List<GameObject> dataContainers = new List<GameObject>();  


	// Use this for initialization
	void Start () {

        Invoke("SpawnDataContainer", 2.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnDataContainer()
    {
        int randomIndex = Random.Range(0, dataContainers.Count);
        //GameObject container = GameObject.Instantiate(dataContainerPrefab);
        GameObject container = GameObject.Instantiate(dataContainers[randomIndex]);
        container.transform.position = transform.position + new Vector3 (Random.Range(-1f, 1f), Random.Range(-2f, 2f), 0);
        container.name = "Container";

        container.GetComponent<DataContainerObject>().speed = 3;
        container.GetComponent<DataContainerObject>().direction = transform.forward;

        //Temporarily Inactive
        Invoke("SpawnDataContainer", Random.Range (4, 10));
    }
}
