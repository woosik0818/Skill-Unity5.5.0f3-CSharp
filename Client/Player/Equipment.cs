using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour 
{
    public Item[] EquipItem;

    public GameObject[] EquipSlots;

    private void Awake()
    {
        for(int i = 0; i < EquipSlots.Length; i++)
        {

        }
    }
}
