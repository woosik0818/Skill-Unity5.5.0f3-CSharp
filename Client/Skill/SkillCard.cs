using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCard : MonoBehaviour
{
    public static SkillCard Instance;
    public GameObject Skill1;
    public GameObject Skill2;
    GameObject NewSkill;

    //Item item1Type;
    //Item item2Type;
    //Item newSkill;

    public enum TYPE { HPPotion, MPPotion, Weapon, Armor }

    int type1;           // 아이템의 타입.
    int type2;
    int newType;

    // Use this for initialization
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Debug.LogError("Not Single SkillManager!");

        //item1Type = Skill1.GetComponent<Slot>().ItemReturn();
        //item2Type = Skill2.GetComponent<Slot>().ItemReturn();
        //newSkill = NewSkill.GetComponent<Slot>().ItemReturn();

        //type1 = item1Type.typecheck();
        //type2 = item2Type.typecheck();
        //newType = newSkill.typecheck();
    }

    public void makeSkill()
    {
        //item1Type = Skill1.GetComponent<Slot>().ItemReturn();
        //item2Type = Skill2.GetComponent<Slot>().ItemReturn();
        //newSkill = NewSkill.GetComponent<Slot>().ItemReturn();

        //type1 = item1Type.typecheck();
        //type2 = item2Type.typecheck();
        //newType = newSkill.typecheck();

        type1 = EquipmentManager.Instance.typeReturn(0);
        type2 = EquipmentManager.Instance.typeReturn(1);

        print(type1);
        print(type2);
        switch (type1)
        {
            case 0:
                switch (type2)
                {
                    case 0:
                        newType = 0;
                        print("Make Weapon");
                        break;
                    case 1:
                        newType = 99;
                        print("Make HPPotion");
                        break;
                    case 99:
                        print("Make Nothing");
                        break;
                }
                break;
            case 1:
                switch (type2)
                {
                    case 0:
                        newType = 99;
                        print("Make HPPotion");
                        break;
                    case 1:
                        newType = 1;
                        print("Make Armor");
                        break;
                    case 99:
                        print("Make Nothing");
                        break;
                }
                break;
            case 99:
                switch (type2)
                {
                    case 0:
                        print("Make Nothing");
                        break;
                    case 1:
                        print("Make Nothing");
                        break;
                    case 99:
                        print("Make Nothing");
                        break;
                }
                break;
        }
    }

}