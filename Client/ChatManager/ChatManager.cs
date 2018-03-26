using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatManager : MonoBehaviour 
{
    public string charactorID = "임재형";

    public UnityEngine.UI.InputField InputText;
    public UnityEngine.UI.Text OutputText;

    const int Maxchat = 20;                             // 채팅의 최대 갯수

    static string[] chatlist = new string[Maxchat];    // 채팅을 배열로 받아서 화면에 출력    0,1,2

    static int chatcount = 0;    // 현재 채팅의 갯수를 저장하여 화면에 출력되는 채팅의 갯수를 조절할 때 사용

    public void SendClick()
    {
        if (InputText.text != "")
        {
            if (chatcount == Maxchat)                   // count가 배열의 최대 크기만큼 커진다면
            {
                for (int i = 0; i < Maxchat - 1; i++)
                {
                    chatlist[i] = chatlist[i + 1];          // 채팅창의 가장 나중 채팅기록을 삭제 후 재배치(가장 마지막 리스트에 최근꺼 입력)
                }
                chatcount--;
            }

            chatlist[chatcount] = charactorID + " : " + InputText.text;           // 채팅리스트 가장 마지막에 텍스트 입력     현재는 2에 입력

            OutputText.text = "";   // 화면에 출력되는 output text를 초기화 후 리스트에 있는 채팅으로 재구성
            for (int i = 0; i != chatcount + 1; i++)
            {
                OutputText.text = OutputText.text + chatlist[i] + "\n";
            }

            InputText.text = "";                            //입력 텍스트창 초기화

            chatcount++;
        }
    }

    // Use this for initialization
    void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}
}