using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItem : MonoBehaviour {

    public GameObject[] enemyItem;

    public void ItemDrop()
    {
//        if (Random.Range(0, 100) < 20)
//            return;

        int i = Random.Range(0, enemyItem.Length);
        enemyItem[i].GetComponent<Item>().AddItem();
    }
}
