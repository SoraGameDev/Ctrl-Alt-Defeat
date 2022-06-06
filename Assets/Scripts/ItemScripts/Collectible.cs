using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InventoryManager.Instance.AddItemToItemsToCollect(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddToInventory() 
    {
        InventoryManager.Instance.AddItemToItemsCollected(gameObject);
    }
}
