using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionManager : MonoBehaviour
{

    public static FusionManager Instance;
    public GameObject fusioninventory;

    FusionInventory IV;

    SkillInfo[] itemslots;
    // Use this for initialization
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Debug.LogError("Not Single SkillManager!");

        IV = fusioninventory.GetComponent<FusionInventory>();

        IV.create();

        int slotnum = fusioninventory.transform.GetChildCount();

        for (int i = 0; i < slotnum; i++)
        {
            if (fusioninventory.transform.GetChild(i).tag == "Slot")
            {
                FusionSlot obj = fusioninventory.transform.GetChild(i).GetComponent<FusionSlot>();
                obj.Memory();
            }
        }
    }
    public FusionInventory getInventory()
    {
        return IV;
    }
}
