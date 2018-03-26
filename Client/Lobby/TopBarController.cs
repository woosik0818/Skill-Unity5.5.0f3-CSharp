using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TopBarController : MonoBehaviour 
{
// txtName: 유저의 이름
// txtLevel: 유저의 레벨
// txtExp: 유저의 경험치
// txtDiamond: 유저의 다이아몬드 수
// sliderExp: 경험치 게이지 
	public Text txtName, txtLevel, txtExp, txtDiamaon;
	public Slider sliderExp;
    public RawImage UserIMG;

    void Start () 
    {
        StartCoroutine(PhotoURL());
        UpdatePlayerData();
	}

	void UpdatePlayerData()
	{
		txtName.text = CharactorManager.Instance.Name;
        txtLevel.text = "Lv " + CharactorManager.Instance.GetLevel().ToString();
		txtExp.text = CharactorManager.Instance.GetCurrentEXP().ToString() + " / " + CharactorManager.Instance.GetMaxEXP().ToString();
		sliderExp.maxValue = CharactorManager.Instance.GetMaxEXP();
		sliderExp.value = CharactorManager.Instance.GetCurrentEXP();
    }

    IEnumerator PhotoURL()
    {
        WWW www = new WWW(CharactorManager.Instance.FacebookPhotoURL);
        yield return www;
        UserIMG.texture = www.texture;
    }

    // Update is called once per frame
    void Update () 
    {
	
    }
}
