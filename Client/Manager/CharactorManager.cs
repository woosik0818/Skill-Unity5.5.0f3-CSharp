using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Facebook.Unity;
using Boomlagoon.JSON;

public class CharactorManager : MonoBehaviour
{
    // UserID 입니다. 서버 상에서 유저를 식별하는 고유번호입니다.
    public int UserID
    {
        get
        {
            return PlayerPrefs.GetInt("UserID");
        }

        set
        {
            PlayerPrefs.SetInt("UserID", value);
        }
    }

    // AccessToken은 서버 API에 접근하기 위한 API의 역할을 합니다.
    public string AccessToken
    {
        get
        {
            return PlayerPrefs.GetString("AccessToken");
        }

        set
        {
            PlayerPrefs.SetString("AccessToken", value);
        }
    }

    // 페이스북 아이디입니다. 페이스북의 고유번호입니다. App Scoped User ID입니다.
    public string FacebookID
    {
        get
        {
            return PlayerPrefs.GetString("FacebookID");
        }

        set
        {
            PlayerPrefs.SetString("FacebookID", value);
        }
    }

    // 페이스북에 인증할 수 있는 유저의 개인 비밀번호 키입니다.
    public string FacebookAccessToken
    {
        get
        {
            return PlayerPrefs.GetString("FacebookAccessToken");
        }

        set
        {
            PlayerPrefs.SetString("FacebookAccessToken", value);
        }
    }

    // 유저의 이름입니다. 기본으로 페이스북의 이름을 가져와 적용합니다.
    public string Name
    {
        get
        {
            return PlayerPrefs.GetString("Name");
        }

        set
        {
            PlayerPrefs.SetString("Name", value);
        }
    }

    // 페이스북의 프로필사진 주소입니다.
    public string FacebookPhotoURL
    {
        get
        {
            return PlayerPrefs.GetString("FacebookPhotoURL");
        }

        set
        {
            PlayerPrefs.SetString("FacebookPhotoURL", value);
        }
    }

    static CharactorManager _instance;
    public static CharactorManager Instance {
        get
        {
            if (!_instance)
            {
                GameObject container = new GameObject("CharactorManager");
                _instance = container.AddComponent(typeof(CharactorManager)) as CharactorManager;

                DontDestroyOnLoad(container);
            }
            return _instance;
        }
    }

    public GameObject player;

    //플레이어의 기본 정보
    public int level = 1;
    public int HP, MP, Money;
    public int attack, defence;
    public int MagicDamage;
    public float Critical, CriticalDamage;
    public int CurrentEXP = 0, EXPMax = 100;

    //플레이어의 스텟 관련정보
    public int Str; //attack damage
    public int Dex; //critical damage & percentage
    public int Int; //mp size & magic damage
    public int Con; //hp size & shield
    public int Str_Temp, Dex_Temp, Int_Temp, Con_Temp;         //스텟 변경시 임시로 저장할 공간
    public int statPoint = 0;
    public int statPoint_Temp;

    Transform playerPosition;           //플레이어의 위치좌표 저장
    public int sceneNumber;                    //Scene의 번호 저장
    public int VRCheck = 0;

    //플레이어의 장비 관련정보
    public int[] p_Item;

    //스테이터스 창의 정보출력 Object들
    public UnityEngine.UI.Text STR_Text;
    public UnityEngine.UI.Text DEX_Text;
    public UnityEngine.UI.Text INT_Text;
    public UnityEngine.UI.Text CON_Text;

    public UnityEngine.UI.Text StatPoint_Text;

    public UnityEngine.UI.Text Money_Text;

    //수치 계산 후 정보
    public UnityEngine.UI.Text HP_Text;
    public UnityEngine.UI.Text MP_Text;
    public UnityEngine.UI.Text Attack_Text;
    public UnityEngine.UI.Text Defence_Text;
    public UnityEngine.UI.Text Critical_Text;
    public UnityEngine.UI.Text CriticalDam_Text;
    public UnityEngine.UI.Text MagicDamage_Text;

    //UI 상단 레벨 Text 관련 변수, 경험치바
    public UnityEngine.UI.Text LevelText;
    public UnityEngine.UI.Slider EXPBar;

    // Use this for initialization
    void Awake()
    {
        CharactorSpec();
    }

    public void FacebookLogin(Action<bool, string> callback, int retryCount = 0)
    {

        FB.LogInWithReadPermissions(new List<string>() { "public_profile", "email", "user_friends" }, delegate (ILoginResult result)
        {
            if (result.Error != null && retryCount >= 3)
            {
                Debug.LogError(result.Error);

                callback(false, result.Error);
                return;
            }

            if (result.Error != null)
            {
                Debug.LogError(result.Error);

                retryCount = retryCount + 1;
                FacebookLogin(callback, retryCount);
                return;
            }

            Debug.Log(result.RawResult);

            Debug.Log("FB Login Result: " + result.RawResult);


            // 페이스북 로그인 결과를 JSON 파싱합니다.
            JSONObject obj = JSONObject.Parse(result.RawResult);

            // 페이스북 아이디를 UserSingleton에 저장합니다. 
            // 이 변수는 게임이 껏다 켜져도 유지되도록 환경변수에 저장하도록 구현되있습니다.
            CharactorManager.Instance.FacebookID = obj["user_id"].Str;
            CharactorManager.Instance.FacebookPhotoURL = "http://graph.facebook.com/" + CharactorManager.Instance.FacebookID + "/picture?type=square";

            // 페이스북 액세스 토큰을 UserSingleton에 저장힙니다.
            // 이 변수 역시 게임이 껏다 켜져도 유지됩니다.
            CharactorManager.Instance.FacebookAccessToken = obj["access_token"].Str;

            callback(true, result.RawResult);
        });
    }

    public void LoadFacebookMe(Action<bool, string> callback, int retryCount = 0)
    {

        FB.API("/me", HttpMethod.GET, delegate (IGraphResult result)
        {
            if (result.Error != null && retryCount >= 3)
            {
                Debug.LogError(result.Error);

                callback(false, result.Error);
                return;
            }

            if (result.Error != null)
            {
                Debug.LogError("Error occured, start retrying.. " + result.Error);

                retryCount = retryCount + 1;
                LoadFacebookMe(callback, retryCount);
                return;
            }

            Debug.Log(result.RawResult);

            JSONObject meObj = JSONObject.Parse(result.RawResult);
            CharactorManager.Instance.Name = meObj["name"].Str;

            callback(true, result.RawResult);
        });
    }

    //유저의 정보를 서버로부터 받아와서 최신 정보로 업데이트 하는 함수입니다. 
    //콜백변수로, 로드가 완료되면 다시 호출한 스크립트로 로드가 완료되었다고 호출할 수 있습니다.
    public void Refresh(Action callback)
    {
        HTTPClient.Instance.GET(Singleton.Instance.HOST + "/api/values/" + CharactorManager.Instance.UserID,
        delegate (WWW www)
        {
            Debug.Log(www.text);
            JSONObject data = JSONObject.Parse(www.text);

            CharactorManager.Instance.level = (int)data["Level"].Number;
            CharactorManager.Instance.CurrentEXP = (int)data["Experience"].Number;
            CharactorManager.Instance.Money = (int)data["Money"].Number;
            CharactorManager.Instance.Str = (int)data["Str"].Number;
            CharactorManager.Instance.Dex = (int)data["Dex"].Number;
            CharactorManager.Instance.Con = (int)data["Con"].Number;
            CharactorManager.Instance.Int = (int)data["Int"].Number;
            CharactorManager.Instance.statPoint = (int)data["StatPoint"].Number;
            CharactorManager.Instance.EXPMax = (int)data["MaxExperience"].Number;
            callback();

            CharactorSpec();
        });
    }

    //플레이어의 정보를 받아오는 함수
    public int GetAttack()
    {
        return attack;
    }

    public int GetDefence()
    {
        return defence;
    }

    public int GetHP()
    {
        return HP;
    }

    public int GetMP()
    {
        return MP;
    }

    public int GetMoney()
    {
        return Money;
    }

    public int GetLevel()
    {
        print("레벨 : " + level);
        return level;
    }

    public int GetCurrentEXP()
    {
        return CurrentEXP;
    }

    public int GetMaxEXP()
    {
        return EXPMax;
    }

    public float GetCritical()
    {
        return Critical;
    }

    public float GetCriticalDam()
    {
        return CriticalDamage;
    }

    public int GetMagicDam()
    {
        return MagicDamage;
    }

    //플레이어가 레벨업 했을 시에 호출
    public void LevelUP()
    {
        statPoint += 5;
        level++;
        CharactorSpec();
        SceneStartManager.Instance.SetLevelText(level);
    }

    //스텟 정보를 받아오는 함수
    public int GetStr()
    {
        return Str;
    }

    public int GetDex()
    {
        return Dex;
    }

    public int GetInt()
    {
        return Int;
    }

    public int GetCon()
    {
        return Con;
    }

    public int GetStat()
    {
        return statPoint;
    }

    public int GetStrTemp()
    {
        return Str_Temp;
    }

    public int GetDexTemp()
    {
        return Dex_Temp;
    }

    public int GetIntTemp()
    {
        return Int_Temp;
    }

    public int GetConTemp()
    {
        return Con_Temp;
    }

    public int GetStatTemp()
    {
        return statPoint_Temp;
    }

    //스텟의 증가
    public void StrUp()
    {
        if (statPoint > 0)
        {
            Str += 1;
            statPoint--;
            CharactorSpec();
        }
    }

    public void DexUp()
    {
        if (statPoint > 0)
        {
            Dex += 1;
            statPoint--;
            CharactorSpec();
        }
    }

    public void IntUp()
    {
        if (statPoint > 0)
        {
            Int += 1;
            statPoint--;
            CharactorSpec();
        }
    }

    public void ConUp()
    {
        if (statPoint > 0)
        {
            Con += 1;
            statPoint--;
            CharactorSpec();
        }
    }

    //소지 금액의 변화
    public void MoneyUp(int amount)
    {
        Money += amount;
    }

    public void MoneyDown(int amount)
    {
        Money -= amount;
    }

    //데미지 및 수치 계산
    public void CharactorSpec()
    {
        attack = Str * 2;
        Critical = (float)0.05 * (float)Dex;
        CriticalDamage = (float)0.03 * (float)Dex;
        MP = 10 * Int + ((level - 1) * 5);
        MagicDamage = 3 * Int;
        HP = 20 * Con + ((level - 1) * 10);
        defence = 1 * Con;
    }

    //temp를 저장하고 스텟을 변경했을 때 한번 더 호출
    public void SetTemp()
    {
        Str_Temp = Str;
        Dex_Temp = Dex;
        Int_Temp = Int;
        Con_Temp = Con;
        statPoint_Temp = statPoint;
    }

    public void ReturnTemp()
    {
        Str = Str_Temp;
        Dex = Dex_Temp;
        Int = Int_Temp;
        Con = Con_Temp;
        statPoint = statPoint_Temp;
        CharactorSpec();
    }

    public void AllowChange()      //변경완료
    {
        SetTemp();
        CharactorSpec();
        SceneStartManager.Instance.PlayerSet();
        UpgradeController.Instance.Upgrade();
    }

    //스테이터스를 화면에 출력
    void StatusShow()
    {
        HP_Text.text = "" + HP;
        MP_Text.text = "" + MP;
        Attack_Text.text = "" + attack;
        Defence_Text.text = "" + defence;
        Critical_Text.text = "" + Critical + "%";
        CriticalDam_Text.text = "" + CriticalDamage + "%";
        MagicDamage_Text.text = "" + MagicDamage;
    }

    //저장하지 않고 인벤토리나 스텟창을 종료하는 경우에 호출. 이전상태로 되돌린다
    public void CheckChange()
    {
        if (statPoint != statPoint_Temp)
        {
            ReturnTemp();
            CharactorSpec();
            StatusShow();
        }
    }

    //경험치를 입수하고 레벨업이 되는지 체크.
    public void TakeEXP(int amount)
    {
        CurrentEXP += amount;
        if (CurrentEXP >= EXPMax)
        {
            CurrentEXP -= EXPMax;
            EXPMax = (int)(EXPMax * 1.3);
            SceneStartManager.Instance.EXPMaxValue();
            LevelUP();
        }
        SceneStartManager.Instance.ChangeEXP();
    }

    public void TakeMoney(int amount)
    {
        Money += amount;
        SceneStartManager.Instance.MoneyUp();
    }

    void SetMoney()
    {
        Money_Text.text = "" + Money;
    }

    //플레이어의 장비 착용에 따른 능력치 변화
    public void PlayerStatSetting(int Itemtype, Item iteminfo)
    {
        p_Item[Itemtype] = iteminfo.stat;
        CharactorSpec();
    }

    public void PlayerStatSetting(int Itemtype) //장비 착용해제
    {
        p_Item[Itemtype] = 0;
        CharactorSpec();
    }

    public int LooseEXP() //사망시 경험치 잃음(MAX 경험치의 30퍼센트)
    {
        print(EXPMax);
        int temp;
        temp = (int)(EXPMax * 0.3);
     
        if (temp >= CurrentEXP)
        {
            temp = CurrentEXP;
            CurrentEXP = 0;
        }

        else
            CurrentEXP -= temp;

        SceneStartManager.Instance.ChangeEXP();
        CharactorSpec();
        UpgradeController.Instance.Upgrade();

        return temp;
    }

    public void PlayerResurrection()  //플레이어 맵 부활
    {
        player.GetComponent<PlayerHealth>().HPReset();
        player.GetComponent<PlayerMagic>().MPReset();
    }

    public void VRChange()
    {
        if (VRCheck == 0)
            VRCheck = 1;
        else
            VRCheck = 0;
    }

    public void SetVRMode()
    {
        PlayerPrefs.SetInt("VR", VRCheck);
    }

    public void GetVRMode()
    {
        VRCheck = PlayerPrefs.GetInt("VR", 0);
        if (VRCheck == 1)
        {
            VRSwitcher.Instance.Switching();
        }
    }
}
