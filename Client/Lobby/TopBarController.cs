using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TopBarController : MonoBehaviour {

// txtName: 유저의 이름
// txtLevel: 유저의 레벨
// txtExp: 유저의 경험치
// txtDiamond: 유저의 다이아몬드 수
// sliderExp: 경험치 게이지 
	public Text txtName, txtLevel, txtExp, txtDiamaon;
	public Slider sliderExp;
    public RawImage UserIMG;

     void Start () {
// 1) UpgradeController가 화면에 나타나면서 NotificationCenter에
// 캐릭터의 정보가 변경되면 자신의 UpdatePlayerData()함수를 호출하도록 등록합니다.
		//NotificationCenter.Instance.Add(NotificationCenter.Subject.PlayerData,UpdatePlayerData);

        //NotificationCenter.Instance.Add (NotificationCenter.Subject.ClickDiaPlus, OnClickDiaPlus);

        // 2) 그리고 시작하자마자 먼저 UserSingleton에서 최신 캐릭터 정보를 화면에 반영하도록 
        // UpdatePlayerData() 함수를 호출합니다.
        StartCoroutine(PhotoURL());
        UpdatePlayerData();
	}

	//void OnClickDiaPlus(){
	//	txtName.text = "NEW NAME!!!";
	//}

	void UpdatePlayerData()
	{
		txtName.text = CharactorManager.Instance.Name;
        txtLevel.text = "Lv " + CharactorManager.Instance.GetLevel().ToString();
		txtExp.text = CharactorManager.Instance.GetCurrentEXP().ToString() + " / " + CharactorManager.Instance.GetMaxEXP().ToString();
		sliderExp.maxValue = CharactorManager.Instance.GetMaxEXP();
		sliderExp.value = CharactorManager.Instance.GetCurrentEXP();
        //txtDiamaon.text = CharactorManager.Instance.Diamond.ToString();
    }

    IEnumerator PhotoURL()
    {
        WWW www = new WWW(CharactorManager.Instance.FacebookPhotoURL);

        yield return www;

        UserIMG.texture = www.texture;
    }
        // Update is called once per frame
    void Update () {
	}
}
