using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    private static InventoryManager instance;
    public static InventoryManager Instance 
    {

        get {

            return instance ?? (instance = new GameObject("InventoryManagerGO").AddComponent<InventoryManager>());

        }

    }

    public List<GameObject> itemsToCollect = new List<GameObject>();

    public List<GameObject> itemsCollected = new List<GameObject>();

    //public delegate void AddToItemsToCollect(GameObject thisObject);
    //public AddToItemsToCollect collectible;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItemToItemsToCollect(GameObject thisItem) 
    {

        if (!itemsToCollect.Contains(thisItem)) 
        {

            itemsToCollect.Add(thisItem);

        }

    }

    public void AddItemToItemsCollected(GameObject thisItem) 
    {
        if (!itemsCollected.Contains(thisItem)) 
        {
            itemsToCollect.Remove(thisItem);
            itemsCollected.Add(thisItem);
        }
    }
}
