using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {

    public static ItemDatabase Instance;
    public GameObject[] database;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Debug.LogError("Not Single ItemDatabase!");
    }

    public int DataNum()                        //아이템 데이터베이스의 길이 전송
    {
        return database.Length;
    }

    public void RefreshInventory(int i)         //아이템 번호를 받아와서 GetComponent<Item>().Additem() 으로 인벤토리에 넣기
    {
        database[i].GetComponent<Item>().AddItem();
    }
}
