using UnityEngine;
// 유니티의 UI 모듈을 연결할 때에는 이 UnityEngine.UI 네임스페이스를 포함해야 합니다 
using UnityEngine.UI; 
using System.Collections;
using System.Text;
using Boomlagoon.JSON;
using System;
using System.IO;
using System.Net;

public class UpgradeController : MonoBehaviour {


    public static UpgradeController Instance;
    //static UpgradeController _instance;
    //public static UpgradeController Instance
    //{
    //    get
    //    {
    //        if (!_instance)
    //        {
    //            GameObject container = new GameObject("UpgradeController");
    //            _instance = container.AddComponent(typeof(UpgradeController)) as UpgradeController;

    //            DontDestroyOnLoad(container);
    //        }

    //        return _instance;
    //    }
    //}

    // txtUpgradeStatus: 업그레이드 현황을 표시하는 텍스트 오브젝트
    // txtHealth: 캐릭터의 체력 업그레이드 버튼의 텍스트 오브젝트입니다.
    // txtDefense: 캐릭터의 방어력 업그레이드 버튼의 텍스트 오브젝트입니다.
    // txtDamage: 캐릭터의 데미지 업그레이드 버튼의 텍스트 오브젝트입니다.
    // txtSpeed: 캐릭터의 스피드 업그레이드 버튼의 텍스트 오브젝트입니다.
    public Text txtUpgradeStatus, txtHealth, txtDefense, txtDamage, txtSpeed;

	void Start () {
		
// 1) UpgradeController가 화면에 나타나면서 NotificationCenter에
// 캐릭터의 정보가 변경되면 자신의 UpdatePlayerData()함수를 호출하도록 등록합니다.
		//NotificationCenter.Instance.Add(NotificationCenter.Subject.PlayerData,UpdatePlayerData);
		
// 2) 그리고 시작하자마자 먼저 UserSingleton에서 최신 캐릭터 정보를 화면에 반영하도록 
// UpdatePlayerData() 함수를 호출합니다.
		//UpdatePlayerData();
        Instance = this;
	}

    // UserSingleton에 저장된 유저의 데이터를 화면에 반영하는 함수입니다. 
    //void UpdatePlayerData()
    //{

    // UserSingleton에 저장된 캐릭터의 4가지 능력치를 화면에 표시하는 스크립트입니다.
    // string 끼리 + 연산은 성능에 좋지 않으므로, string.Format() 함수를 활용하여
    // UserSingleton에 저장된 유저 능력치를 화면에 표시하겠습니다. 
    //txtUpgradeStatus.text = string.Format("{0}\n{1}\n{2}\n{3}",
    //	CharactorManager.Instance.GetHP(),
    //	CharactorManager.Instance.GetDefence(),
    //	CharactorManager.Instance.GetAttack(),
    //	CharactorManager.Instance.GetDex());

    //}

    //public void ConfirmUpgrade(string UpgradeType)
    //{

    //    DialogDataConfirm confirm = new DialogDataConfirm("", "", delegate (bool yn)
    //    {

    //        if (yn == true)
    //        {
    //            Upgrade(UpgradeType);
    //        }

    //    });

    //    DialogManager.Instance.Push(confirm);

    //}

    public void Upgrade()
    {
        JSONObject obj = new JSONObject();
        obj.Add("UserID" , CharactorManager.Instance.UserID);
        obj.Add("Level", CharactorManager.Instance.GetLevel());
        obj.Add("StatPoint", CharactorManager.Instance.GetStat());
        obj.Add("Str", CharactorManager.Instance.GetStr());
        obj.Add("Dex", CharactorManager.Instance.GetDex());
        obj.Add("Int", CharactorManager.Instance.GetInt());
        obj.Add("Con", CharactorManager.Instance.GetCon());
        obj.Add("MaxExperience", CharactorManager.Instance.GetMaxEXP());
        obj.Add("Experience", CharactorManager.Instance.GetCurrentEXP());
        obj.Add("Money", CharactorManager.Instance.GetMoney());

        Debug.Log(obj.ToString());

        HTTPClient.Instance.POST(Singleton.Instance.HOST + "/Upgrade/Execute", obj.ToString(), delegate (WWW www)
        {
            Debug.Log(www.text);
            JSONObject res = JSONObject.Parse(www.text);
            int ResultCode = (int)res["ResultCode"].Number;
        });
    }
    // test// User클래스보내기
    //public void Upgrade()
    //{
    //    User user = new User();
    //    user.UserID = CharactorManager.Instance.UserID;
    //    user.Level = CharactorManager.Instance.GetLevel();
    //    user.StatPoint = CharactorManager.Instance.GetStat();
    //    user.Str = CharactorManager.Instance.GetStr();
    //    user.Dex = CharactorManager.Instance.GetDex();
    //    user.Int = CharactorManager.Instance.GetInt();
    //    user.Con = CharactorManager.Instance.GetCon();
    //    user.MaxExperience = CharactorManager.Instance.GetMaxEXP();
    //    user.Experience = CharactorManager.Instance.GetCurrentEXP();
    //    user.Money = CharactorManager.Instance.GetMoney();

    //    string body = JsonUtility.ToJson(user);

    //    HTTPClient.Instance.POST(Singleton.Instance.HOST + "/Upgrade/Execute", body, delegate (WWW www)
    //        {
    //            Debug.Log(www.text);

    //            JSONObject res = JSONObject.Parse(www.text);
    //            int ResultCode = (int)res["ResultCode"].Number;
    //        });
    //}

    public void Upgrade(string UpgradeType, int stat)
    {
        JSONObject obj = new JSONObject();
        obj.Add("UserID", CharactorManager.Instance.UserID);
        obj.Add("UpgradeType", UpgradeType);
        obj.Add("Stat", stat);

        Debug.Log(obj.ToString());
        HTTPClient.Instance.POST(Singleton.Instance.HOST + "/Upgrade/Execute/" + CharactorManager.Instance.UserID, obj.ToString(), delegate (WWW www)
        {
            Debug.Log(www.text);
            JSONObject res = JSONObject.Parse(www.text);
            int ResultCode = (int)res["ResultCode"].Number;

            if (ResultCode == 1)
            { // Success!
              // Upgrade Success => Load User data again
                CharactorManager.Instance.Refresh(delegate ()
                {
                    NotificationCenter.Instance.Notify(NotificationCenter.Subject.PlayerData);
                });
                // Alert Dialog
                //DialogDataAlert alert = new DialogDataAlert(Language.Instance.GetLanguage("Upgrade Success Title"),
                //                                            Language.Instance.GetLanguage("Upgrade Success"),delegate() {

                //} );
                //DialogManager.Instance.Push(alert);


            }
            else if (ResultCode == 4) // Max Level
            {
                // Alert Dialog
                //DialogDataAlert alert = new DialogDataAlert(Language.Instance.GetLanguage("Upgrade Failed Title"),
                //                                            Language.Instance.GetLanguage("Max Level"), delegate() {

                //});
                //DialogManager.Instance.Push(alert);

            }
            else if (ResultCode == 5) // Not enough diamond
            {
                // Alert Dialog
                //DialogDataAlert alert = new DialogDataAlert(Language.Instance.GetLanguage("Upgrade Failed Title"),
                //                                            Language.Instance.GetLanguage("Not Enouhg Diamond"), delegate() {

                //});
                //DialogManager.Instance.Push(alert);

            }
        });
    }

    void UpgradeStr()
    {
        Debug.Log("UpgradeStr");

        Upgrade("Str", CharactorManager.Instance.GetStr());

        //String title = Language.Instance.GetLanguage("Str Upgrade Confirm");
        //String message = String.Format(Language.Instance.GetLanguage("Diamonds are required"), CharactorManager.Instance.GetHP() * 2);

        //DialogDataConfirm confirm = new DialogDataConfirm(
        //    title,
        //    message,
        //    delegate (bool yn)
        //    {

        //        if (yn)
        //        {

        //        }

        //    });
        //DialogManager.Instance.Push(confirm);

    }
    void UpgradeDex()
    {
        Debug.Log("UpgradeDex");

        Upgrade("Dex", CharactorManager.Instance.GetDex());

        //String title = Language.Instance.GetLanguage("Dex Upgrade Confirm");
        //String message = String.Format(Language.Instance.GetLanguage("Diamonds are required"), CharactorManager.Instance.GetDefence() * 2);

        //DialogDataConfirm confirm = new DialogDataConfirm(
        //    title,
        //    message,
        //    delegate (bool yn)
        //    {

        //        if (yn)
        //        {
        //            Upgrade("Dex", CharactorManager.Instance.GetDex());
        //        }

        //    });

        //DialogManager.Instance.Push(confirm);
    }
    void UpgradeCon()
    {
        Debug.Log("UpgradeCon");

        Upgrade("Con", CharactorManager.Instance.GetCon());

        //String title = Language.Instance.GetLanguage("Con Upgrade Confirm");
        //String message = String.Format(Language.Instance.GetLanguage("Diamonds are required"), CharactorManager.Instance.GetAttack() * 2);

        //DialogDataConfirm confirm = new DialogDataConfirm(
        //    title,
        //    message,
        //    delegate (bool yn)
        //    {

        //        if (yn)
        //        {
        //            Upgrade("Con", CharactorManager.Instance.GetCon());
        //        }

        //    });
        //DialogManager.Instance.Push(confirm);

    }
    void UpgradeInt()
    {
        Debug.Log("UpgradeInt");

        Upgrade("Int", CharactorManager.Instance.GetInt());

        //String title = Language.Instance.GetLanguage("Int Upgrade Confirm");
        //String message = String.Format(Language.Instance.GetLanguage("Diamonds are required"), CharactorManager.Instance.GetDex() * 2);

        //DialogDataConfirm confirm = new DialogDataConfirm(
        //    title,
        //    message,
        //    delegate (bool yn)
        //    {

        //        if (yn)
        //        {
        //            Upgrade("Int", CharactorManager.Instance.GetInt());
        //        }

        //    });
        //DialogManager.Instance.Push(confirm);
    }

    void UpgradeStatPoint()
    {
        Debug.Log("UpgradeStatus");

        Upgrade("StatPoint", CharactorManager.Instance.GetStat());

        //String title = Language.Instance.GetLanguage("Status Upgrade Confirm");
        //String message = String.Format(Language.Instance.GetLanguage("Diamonds are required"), CharactorManager.Instance.GetDex() * 2);

        //DialogDataConfirm confirm = new DialogDataConfirm(
        //    title,
        //    message,
        //    delegate (bool yn)
        //    {

        //        if (yn)
        //        {
        //            Upgrade("Status", CharactorManager.Instance.GetStat());
        //        }

        //    });
        //DialogManager.Instance.Push(confirm);
    }

    void UpgradeLevel()
    {
        Debug.Log("UpgradeLevel");

        Upgrade("Level", CharactorManager.Instance.GetLevel());
        //String title = Language.Instance.GetLanguage("Level Upgrade Confirm");
        //String message = String.Format(Language.Instance.GetLanguage("Diamonds are required"), CharactorManager.Instance.GetDex() * 2);

        //DialogDataConfirm confirm = new DialogDataConfirm(
        //    title,
        //    message,
        //    delegate (bool yn)
        //    {

        //        if (yn)
        //        {
        //            Upgrade("Level", CharactorManager.Instance.GetLevel());
        //        }

        //    });
        //DialogManager.Instance.Push(confirm);
    }

    void UpgradeMaxEXP()
    {
        Debug.Log("UpgradeMaxEXP");

        Upgrade("MaxExperience", CharactorManager.Instance.GetMaxEXP());

        //String title = Language.Instance.GetLanguage("MaxEXP Upgrade Confirm");
        //String message = String.Format(Language.Instance.GetLanguage("Diamonds are required"), CharactorManager.Instance.GetDex() * 2);

        //DialogDataConfirm confirm = new DialogDataConfirm(
        //    title,
        //    message,
        //    delegate (bool yn)
        //    {

        //        if (yn)
        //        {
        //            Upgrade("MaxEXP", CharactorManager.Instance.GetMaxEXP());
        //        }

        //    });
        //DialogManager.Instance.Push(confirm);
    }

    public void UpgradeCurrentEXP()
    {
        Debug.Log("UpgradeCurrentEXP");

        Upgrade("Experience", CharactorManager.Instance.GetCurrentEXP());

        //String title = Language.Instance.GetLanguage("CurrentEXP Upgrade Confirm");
        //String message = String.Format(Language.Instance.GetLanguage("Diamonds are required"), CharactorManager.Instance.GetDex() * 2);

        //DialogDataConfirm confirm = new DialogDataConfirm(
        //    title,
        //    message,
        //    delegate (bool yn)
        //    {

        //        if (yn)
        //        {
        //            Upgrade("CurrentEXP", CharactorManager.Instance.GetCurrentEXP());
        //        }

        //    });
        //DialogManager.Instance.Push(confirm);
    }
    public void UpgradeMoney()
    {
        Debug.Log("UpgradeMoney");

        Upgrade("Money", CharactorManager.Instance.GetMoney());

        //String title = Language.Instance.GetLanguage("Money Upgrade Confirm");
        //String message = String.Format(Language.Instance.GetLanguage("Diamonds are required"), CharactorManager.Instance.GetDex() * 2);

        //DialogDataConfirm confirm = new DialogDataConfirm(
        //    title,
        //    message,
        //    delegate (bool yn)
        //    {

        //        if (yn)
        //        {
        //            Upgrade("Money", CharactorManager.Instance.GetMoney());
        //        }

        //    });
        //DialogManager.Instance.Push(confirm);
    }

    public void UpdateUserLevel()
    {
        UpgradeLevel();
        UpgradeStatPoint();
        UpgradeMaxEXP();
    }

    public void UpdateUserInfo()
    {
        UpgradeStr();
        UpgradeDex();
        UpgradeCon();
        UpgradeInt();
        UpgradeStatPoint();
    }

    public void UpdateMonsterDeath()
    {
        UpgradeCurrentEXP();
        UpgradeMoney();
    }
}