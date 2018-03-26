using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingIMG : MonoBehaviour 
{    
    public UnityEngine.UI.Text IMG_Text;

    public Sprite[] IMG;

    int index;

    string[] text = {
    "슬라임은 가장 기본적이고 약한 몬스터입니다.",
        "포탈은 사냥터와 집을 연결해줍니다.",
    "킹슬라임은 거대하고 강력한, 붉은색의 변종 슬라임입니다."};

	// Use this for initialization
	void Start ()
    {
        index = Random.Range(0, IMG.Length);
        GetComponent<UnityEngine.UI.Image>().sprite = IMG[index];
        IMG_Text.text = text[index];
    }
}
