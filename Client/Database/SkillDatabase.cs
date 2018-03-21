using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDatabase : MonoBehaviour {

    public static SkillDatabase Instance;
    public GameObject[] database;
    
    public GameObject player;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Debug.LogError("Not Single SkillDatabase!");
    }

    public int DataNum()                        //스킬 데이터베이스의 길이 전송
    {
        return database.Length;
    }

    public GameObject SkillCall(int skill_num)
    {
        return database[skill_num];
    }

    public void SkillUsing(int skillNumber)
    {
        switch (skillNumber)
        {
            case 0:
                player.GetComponent<PlayerMovement>().OnDashDown();
                break;
            case 1:
                player.GetComponent<PlayerMovement>().OnSkillDown();
                break;
            case 2:
                player.GetComponent<PlayerHealth>().HpPotion();
                break;
            case 3:
                player.GetComponent<PlayerMagic>().MpPotion();
                break;
        }

    }
}
