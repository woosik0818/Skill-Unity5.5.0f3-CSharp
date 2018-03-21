using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooltimeManager : MonoBehaviour {

    public static CooltimeManager Instance;

    public UnityEngine.UI.Image[] filter;
    public UnityEngine.UI.Text[] cooltimeText;

    public float[] Cooltime;
    public int[] Cooltimetemp;

    int UIslotCount;

    // Use this for initialization
    void Awake () {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Debug.LogError("Not Single CooltimeManager!");

        for (int i = 0; i < filter.Length; i++)
        {
            filter[i].fillAmount = 0;
            cooltimeText[i].text = "";
        }
    }
    // Update is called once per frame
    //void Update()
    //{
    //    for (int i = 0; i < Cooltime.Length - 1; i++)
    //    {
    //        if (Cooltime[i] > 0)                 //쿨타임이 남아있는 스킬의 쿨타임을 전체적으로 계산
    //        {
    //            Cooltime[i] = Cooltime[i] - 1 * Time.deltaTime;
    //            if (i / 4 == UIslotCount)
    //            {
    //                filter[i % 4].fillAmount = Cooltime[i] / Cooltimetemp[i];
    //                cooltimeText[i % 4].text = "" + (int)Cooltime[i];
    //            }
    //        }
    //        else
    //        {
    //            if (i / 4 == UIslotCount)
    //            {
    //                cooltimeText[i % 4].text = "";
    //            }
    //        }
    //    }
    //}

    //public void skilluse(int slotnum, int m_cooltime)
    //{
    //    filter[slotnum].fillAmount = 1;
    //    Cooltime[slotnum + UIslotCount * 4] = m_cooltime;
    //    Cooltimetemp[slotnum + UIslotCount * 4] = m_cooltime;
    //}

    //public float leftCooltime(int slotnum, int slotcount)
    //{
    //    return Cooltime[slotnum + slotcount * 4];
    //}

    //public void SlotChange(int m_slot)              //스킬셋을 넘겼을 때 해당 스킬셋의 쿨타임이 표시되도록
    //{
    //    UIslotCount = m_slot;
    //    for (int i = 0; i < 4; i++)
    //    {
    //        filter[i].fillAmount = Cooltime[UIslotCount * 4 + i];
    //        if (Cooltime[UIslotCount * 4 + i] != 0)
    //            cooltimeText[i].text = "" + (int)Cooltime[UIslotCount * 4 + i];
    //        else
    //            cooltimeText[i].text = "";
    //    }
    //}
}
