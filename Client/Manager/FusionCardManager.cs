using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionCardManager : MonoBehaviour
{

    public static FusionCardManager Instance;

    public GameObject[] CardSlot;
    public GameObject FusionSlot;
    public GameObject sellbutton;
    public GameObject FusionBackGround;
    public GameObject FusionButton;

    MatInfo[] CardInfo;

    public GameObject Card_info;
    public UnityEngine.UI.Image CardInfoImg;
    public UnityEngine.UI.Text CardInfotext;
    public UnityEngine.UI.Text ButtonText;

    MatInfo NowChoiceCard;

    int CardslotNum = 0;
    int Cardcountnum = 0;

    GameObject choiceSlot;

    public Sprite DefaultImg;

    // Use this for initialization
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Debug.LogError("Not Single FusionCardManager!");

        //SkillEquipInfo = new SkillInfo[EquipSlots.Length * 3];
        //SkillEquipInfo[0] = BasicSkill[0].GetComponent<SkillInfo>();
        //SkillEquipInfo[1] = BasicSkill[1].GetComponent<SkillInfo>();

        CardInfo = new MatInfo[3];
    }

    public void CardInfoShow(GameObject obj, Sprite Img, int charge)
    {
        choiceSlot = obj;
        Card_info.SetActive(true);

        CardInfoImg.sprite = Img;
    }

    public void CardInfoShow(MatInfo mat)
    {
        Card_info.SetActive(true);
        CardInfoImg.sprite = mat.DefaultImg;
    }

    public void CardCheck()                             //장착버튼을 눌렀을 때.
    {
        if (ButtonText.text == "장착")
        {
            CardSlot[Cardcountnum].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = CardInfoImg.sprite;
            choiceSlot.GetComponent<FusionSlot>().ItemCheck();
            CardInfo[Cardcountnum] = NowChoiceCard;
            Cardcountnum++;
            if(Cardcountnum >= 3)
            {
                FusionBackGround.SetActive(true);
                FusionButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
            }
            else
            {
                FusionBackGround.SetActive(false);
                FusionButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
            }
        }
        else if (ButtonText.text == "해제")
        {
            CardOff();
        }
        FusionSlot.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = DefaultImg;
    }

    public void ChangeText()
    {
        ButtonText.text = "장착";
        sellbutton.GetComponent<UnityEngine.UI.Button>().interactable = true;
    }

    public void EquipmentSlots(int i)
    {
        CardslotNum = i;
        CardInfoShow(CardInfo[i]);
        ButtonText.text = "해제";
        sellbutton.GetComponent<UnityEngine.UI.Button>().interactable = false;
    }

    void CardOff()                                          //재료카드 꺼내기
    {
        CardInfo[CardslotNum].AddItem();
        
        int temp = CardslotNum;
        /////////////////////////////////////////////////////////
        while (temp < Cardcountnum - 1)
        {
            print(temp);
            CardInfo[temp] = CardInfo[temp + 1];
            CardSlot[temp].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = CardSlot[temp + 1].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite;
            temp++;
        }
        Cardcountnum--;
        CardInfo[Cardcountnum] = null;
        CardSlot[Cardcountnum].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = DefaultImg;
        /////////////////////////////////////////////////////////
        //for(int i = 0; i < Cardcountnum; i++)
        //{
        //    print("11");
        //    if (CardInfo[i] == null)
        //    {
        //        print("22");
        //        CardInfo[i] = CardInfo[i + 1];
        //        CardSlot[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = CardSlot[i + 1].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite;
        //    }
        //}
    }

    public void CardEquipReady(MatInfo mat)
    {
        NowChoiceCard = mat;
    }

    public void Fusion()                    //합성 버튼을 눌렀을 때 호출
    {
        int length = SkillDatabase.Instance.DataNum();

        GameObject create_skill = SkillDatabase.Instance.SkillCall(Random.Range(0, length));        //스킬 생성 정보를 넘겨주기

        FusionSlot.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = create_skill.GetComponent<SkillInfo>().DefaultImg;    //스킬 합성창에 새로운 스킬 이미지 등록
        FusionSlot.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = "";
        create_skill.GetComponent<SkillInfo>().AddItem();

        for (int i = 0; i < 3; i++)              //합성과 동시에 저장하고 있던 정보들 초기화
        {
            CardSlot[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = DefaultImg;
            CardInfo[i] = null;
        }
        NowChoiceCard = null;
        CardslotNum = 0;
        Cardcountnum = 0;

        FusionBackGround.SetActive(false);
        FusionButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
    }
}
