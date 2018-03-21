using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour {

    public static SkillManager Instance;
    public GameObject Skillinventory;

    SkillInventory IV;

    SkillInfo[] itemslots;
    // Use this for initialization
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Debug.LogError("Not Single SkillManager!");

        IV = Skillinventory.GetComponent<SkillInventory>();

        IV.create();

        int slotnum = Skillinventory.transform.GetChildCount();

        for (int i = 0; i < slotnum; i++)
        {
            if (Skillinventory.transform.GetChild(i).tag == "Slot")
            {
                SkillSlot obj = Skillinventory.transform.GetChild(i).GetComponent<SkillSlot>();
                obj.Memory();
            }
        }
    }
    public SkillInventory getInventory()
    {
        return IV;
    }
}
