using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour 
{
    public static EquipmentManager Instance;

    public GameObject Player;
    public GameObject[] EquipSlots;
    public GameObject sellbutton;

    public GameObject Iteminfo;
    public UnityEngine.UI.Image ItemInfoImg;
    public UnityEngine.UI.Text ItemInfoText;

    public UnityEngine.UI.Text ButtonText;

    GameObject choiceSlot;
    public Sprite DefaultImg;

    int itemcharge = 0;
    int equipmentslots = 99;

    Item[] EquipInfo;                                        // 0 : 무기, 1 : 상의, 2 : 머리, 3 : 신발, 4이상 = 포션류
    
    public GameObject SkillMenu;
    public GameObject Menubuttons;
    public GameObject BackGrounds;
    public GameObject choiceBackGround;

    // Use this for initialization
    void Awake ()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Debug.LogError("Not Single EquipmentManager!");

        EquipInfo = new Item[EquipSlots.Length];
    }

    public void EquipItem(int Itemtype, Item item_info)
    {
        if (EquipInfo[Itemtype] != null)                     //장착하려는 슬롯이 비어있지 않을때 먼저 꺼낸다.
            EquipInfo[Itemtype].AddItem();

        EquipInfo[Itemtype] = item_info;

        EquipSlots[Itemtype].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = item_info.DefaultImg;

        PlayerStatSetting(Itemtype, item_info);
    }

    void PlayerStatSetting(int Itemtype, Item iteminfo)
    {
        CharactorManager.Instance.PlayerStatSetting(Itemtype, iteminfo);
    }
    
    public void ItemInfoShow(GameObject obj, Sprite Img, int stat, int charge)
    {
        itemcharge = charge;
        choiceSlot = obj;
        
        Iteminfo.SetActive(true);

        ItemInfoImg.sprite = Img;
        ItemInfoText.text = "" + stat;
    }

    public void ItemInfoShow(Item obj)
    {
        itemcharge = obj.itemCharge;
        Iteminfo.SetActive(true);

        ItemInfoImg.sprite = obj.DefaultImg;
        ItemInfoText.text = "" + obj.stat;
    }

    public void ItemCheck()
    {
        if (ButtonText.text == "장착")
            choiceSlot.GetComponent<Slot>().ItemCheck();
        else if (ButtonText.text == "해제")
            ItemCancel();
    }

    void ItemCancel()
    {
        EquipInfo[equipmentslots].AddItem();

        CharactorManager.Instance.PlayerStatSetting(equipmentslots);

        EquipInfo[equipmentslots] = null;
        EquipSlots[equipmentslots].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = DefaultImg;
    }

    public void ItemSell()
    {
        if (ButtonText.text == "장착")
        {
            CharactorManager.Instance.TakeMoney(itemcharge);
            choiceSlot.GetComponent<Slot>().ItemDelete();
        }
    }

    public void EquipmentSlots(int i)
    {
        equipmentslots = i;
        ItemInfoShow(EquipInfo[i]);
        ButtonText.text = "해제";
        sellbutton.GetComponent<UnityEngine.UI.Button>().interactable = false;
    }

    public void ChangeText()
    {
        ButtonText.text = "장착";
        sellbutton.GetComponent<UnityEngine.UI.Button>().interactable = true;
    }

    public int typeReturn(int i)
    {
        return EquipInfo[i].typecheck();
    }

    public void PotionEquipComp()
    {
        SkillMenu.SetActive(false);
        Menubuttons.SetActive(true);
        BackGrounds.SetActive(true);
        choiceBackGround.SetActive(false);
    }
}
