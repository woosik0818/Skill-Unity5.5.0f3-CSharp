using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEquipManager : MonoBehaviour 
{
    public static SkillEquipManager Instance;
    public GameObject[] EquipSlots;
    public GameObject[] UIButtons;
    public GameObject sellbutton;
    public GameObject[] BasicSkill;

    public UnityEngine.UI.Text slottext;
    public UnityEngine.UI.Text UIText;

    public GameObject Skillinfo;
    public UnityEngine.UI.Image SkillInfoImg;
    public UnityEngine.UI.Text SkillInfotext;

    public UnityEngine.UI.Text ButtonText;

    SkillInfo[] SkillEquipInfo;
    public Sprite DefaultImg;

    int slotCount = 0;
    int UIslotCount = 0;
    bool isChange = false;

    public GameObject ChoiceBackground;
    GameObject choiceSlot;
    SkillInfo NowChoiceSkill;

    int equipmentslots = 99;
    
    // Use this for initialization
    void Awake ()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Debug.LogError("Not Single SkillEquipManager!");

        SkillEquipInfo = new SkillInfo[EquipSlots.Length * 3];
        for(int i = 0; i < 4; i++)
            SkillEquipInfo[i] = BasicSkill[i].GetComponent<SkillInfo>();

        UIText.text = "1";
    }

    public void SlotChange()
    {
        int temp;
        slotCount++;
        if (slotCount >= 3)
            slotCount = 0;
        temp = slotCount + 1;

        slottext.text = "" + temp;

        for(int i = 0; i < EquipSlots.Length; i++)
        {
            if (SkillEquipInfo[i + slotCount * 4] != null)
                EquipSlots[i].GetComponent<UnityEngine.UI.Image>().sprite = SkillEquipInfo[i + slotCount * 4].DefaultImg;
            else
                EquipSlots[i].GetComponent<UnityEngine.UI.Image>().sprite = DefaultImg;
        }
    }

    void UISlotChange()
    {
        for (int i = 0; i < UIButtons.Length; i++)
        {
            if (SkillEquipInfo[i + UIslotCount * 4] != null)
                UIButtons[i].GetComponent<UnityEngine.UI.Image>().sprite = SkillEquipInfo[i + UIslotCount * 4].DefaultImg;
            else
                UIButtons[i].GetComponent<UnityEngine.UI.Image>().sprite = DefaultImg;
        }
    }

    public void UISlotNumCount()
    {
        UIslotCount++;
        if (UIslotCount >= 3)
            UIslotCount = 0;

        int temp = UIslotCount + 1;

        UIText.text = "" + temp;

        UISlotChange();
    }

    public void SkillInfoShow(GameObject obj, Sprite Img, int stat, int charge)
    {
        choiceSlot = obj;
        Skillinfo.SetActive(true);

        SkillInfoImg.sprite = Img;
        SkillInfotext.text = "" + stat;
    }

    public void SkillInfoShow(SkillInfo obj)
    {
        Skillinfo.SetActive(true);

        SkillInfoImg.sprite = obj.DefaultImg;
        SkillInfotext.text = "" + obj.stat;
    }

    public void EquipmentSlots(int i)
    {
        equipmentslots = i + (slotCount * 4);
        if (!isChange)      //스킬슬롯을 눌렀을 때
        {
            SkillInfoShow(SkillEquipInfo[equipmentslots]);
            ButtonText.text = "해제";
            sellbutton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

        else                //스킬을 장착하려고 할 때
        {
            if(SkillEquipInfo[equipmentslots] != null)
                SkillOff();
            SkillEquipInfo[equipmentslots] = NowChoiceSkill;
            EquipSlots[i].GetComponent<UnityEngine.UI.Image>().sprite = NowChoiceSkill.DefaultImg;
            isChange = false;
            ChoiceBackground.SetActive(false);
        }

        UISlotChange();
    }

    public void SkillEquipReady(SkillInfo obj)
    {
        isChange = true;
        NowChoiceSkill = obj;
    }

    public void ChangeText()
    {
        ButtonText.text = "장착";
        sellbutton.GetComponent<UnityEngine.UI.Button>().interactable = true;
    }

    public void SkillCheck()
    {
        if (ButtonText.text == "장착")
        {
            ChoiceBackground.SetActive(true);
            choiceSlot.GetComponent<SkillSlot>().ItemCheck();
        }

        else if (ButtonText.text == "해제")
        {
            SkillOff();
            UISlotChange();
        }
    }

    void SkillOff()
    {
        SkillEquipInfo[equipmentslots].AddItem();
        SkillEquipInfo[equipmentslots] = null;
        EquipSlots[equipmentslots - (slotCount * 4)].GetComponent<UnityEngine.UI.Image>().sprite = DefaultImg;
    }

    public void SkillUse(int slotNumber)
    {
        if (SkillEquipInfo[((UIslotCount * 4) + slotNumber)] != null)        //누른 버튼의 스킬정보가 들어있는 경우에만
        {
            SkillEquipInfo[((UIslotCount * 4) + slotNumber)].UseSkill();     //누른 버튼의 스킬정보
        }
    }
}
