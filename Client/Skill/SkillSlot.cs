using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour 
{
    Stack<SkillInfo> slot;         // 슬롯을 스택으로 만든다.
    public Text text;         // 아이템에 개수를 표현해줄 텍스트.
    public Sprite DefaultImg; // 슬롯에 있는 아이템을 다 사용할 경우 아무것도 없는 이미지를 넣어줄 필요가 있다.

    // 슬롯에 존재하는 아이템이 뭔지 반환.
    public SkillInfo ItemReturn() { return slot.Peek(); }
    // 겹칠수 있는 한계치를 넘으면.
    public bool ItemMax(SkillInfo skill) { return ItemReturn().MaxCount > slot.Count; }
    // 슬롯이 현재 비어있는지? 에 대한 플래그 반환.
    public bool isSlots() { return isSlot; }

    // 현재 슬롯이 비어있는지?
    public bool isSlot = false;

    public void Memory()
    {
        slot = new Stack<SkillInfo>();
    }


    void Start()
    {
        // 텍스트 컴포넌트의 RectTransform을 가져온다.
        // 텍스트 객체의 부모 객체의 x지름을 가져온다.
        // 폰트의 크기를 부모 객체의 x지름 / 2 만큼으로 지정해준다.
        RectTransform rect = text.gameObject.GetComponent<RectTransform>();
        float Size = text.gameObject.transform.parent.GetComponent<RectTransform>().sizeDelta.x;
        text.fontSize = (int)(Size * 0.5f);
    }

    public void AddItem(SkillInfo item)
    {
        // 스택에 아이템 추가.
        slot.Push(item);

        // 아이템이 2개 이상일 때
        if (slot.Count > 1)
            text.text = slot.Count.ToString();

        // 슬롯이 비어있었다면
        if (!isSlot)
        {
            // 이미지 변경.
            transform.GetChild(0).GetComponent<Image>().sprite = item.DefaultImg;
            // 아이템이 있다고 변경.
            isSlot = true;
        }
    }

    public void SkillInfo()
    {
        if (isSlot)
        {
            SkillEquipManager.Instance.SkillInfoShow(gameObject, slot.Peek().DefaultImg, slot.Peek().stat, slot.Peek().itemCharge);
            SkillEquipManager.Instance.ChangeText();
        }
    }

    public void ItemCheck()
    {
        if (isSlot)            //빈 슬롯이 아니라면
        {
            SkillInfo obj = slot.Pop();
            SkillEquipManager.Instance.SkillEquipReady(obj);
            if (slot.Count == 0)
            {
                transform.GetChild(0).GetComponent<Image>().sprite = DefaultImg;
                isSlot = false;
            }
        }

        else
        {
            print("노노");
        }
    }

    public void ItemDelete()
    {
        if (isSlot)
        {
            slot.Pop();
            if (slot.Count == 0)
            {
                transform.GetChild(0).GetComponent<Image>().sprite = DefaultImg;
                isSlot = false;
            }
        }
    }
}
