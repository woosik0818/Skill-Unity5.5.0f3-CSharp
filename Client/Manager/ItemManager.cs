using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {
    
    public static ItemManager Instance;
    public GameObject inventory;

    Inventory IV;

    Item[] itemslots;
    // Use this for initialization
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Debug.LogError("Not Single ItemManager!");
        
        IV = inventory.GetComponent<Inventory>();

        IV.create();

        int slotnum = inventory.transform.GetChildCount();

        for(int i = 0; i < slotnum; i++)
        {
            if(inventory.transform.GetChild(i).tag == "Slot")
            {
                Slot obj = inventory.transform.GetChild(i).GetComponent<Slot>();
                obj.Memory();
            }
        }
    }
    public Inventory getInventory()
    {
        return IV;
    }


}
