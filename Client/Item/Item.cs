using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum TYPE 
    {
        HPPotion, 
        MPPotion, 
        Weapon, 
        Armor
    }

    public TYPE type;           // 아이템의 타입.

    public Sprite DefaultImg;   // 기본 이미지.
    public int MaxCount;        // 겹칠수 있는 최대 숫자.
    
    public int stat;
    public int itemCharge;
    public int ItemNumber;

    public void AddItem()
    {
        // 싱글톤을 이용해서 인벤토리 스크립트를 가져온다.
        Inventory iv = ItemManager.Instance.getInventory();

        // 아이템 획득에 실패할 경우.
        if (!iv.AddItem(this))
            Debug.Log("아이템이 가득 찼습니다.");
    }

    public int typecheck()
    {
        if (type == TYPE.Weapon)
            return 0;
        else if (type == TYPE.Armor)
            return 1;
        else
            return 99;
    }

    void OnTriggerEnter(Collider _col)
    {
        string name = LayerMask.LayerToName(_col.gameObject.layer);
        // 플레이어와 충돌하면.
        if (name == "Player")
        {
            AddItem();
        }
    }
}
