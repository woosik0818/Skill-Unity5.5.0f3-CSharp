using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSlotManager : MonoBehaviour {

    public static SkillSlotManager Instance;

    public UnityEngine.UI.Button[] SkillSlots;

    slotinfo[] slots;

    public int SlotsCount = 4;

    struct slotinfo
    {
        int type;           //타입이 0이면 스킬, 1이면 hp포션,2 는 mp포션
        int stats;          //스킬은 데미지, 포션은 회복량
        public Sprite img;
    }

	// Use this for initialization
	void Awake ()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Debug.LogError("Not Single SkillSlotManager!");

        slots = new slotinfo[SlotsCount];
    }
	
    public void SkillEquip(int number)
    {
        SkillSlots[number].image.sprite = slots[number].img;
    }
}
