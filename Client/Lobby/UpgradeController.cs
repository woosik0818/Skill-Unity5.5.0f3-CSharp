using UnityEngine;
using UnityEngine.UI; 
using System.Collections;
using System.Text;
using Boomlagoon.JSON;
using System;
using System.IO;
using System.Net;

public class UpgradeController : MonoBehaviour 
{
    public static UpgradeController Instance;
    public Text txtUpgradeStatus, txtHealth, txtDefense, txtDamage, txtSpeed;

	void Start () 
    {
        Instance = this;
	}

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
            }

            else if (ResultCode == 4) // Max Level
            {
                // Alert Dialog
                DialogDataAlert alert = new DialogDataAlert(Language.Instance.GetLanguage("Upgrade Failed Title"),
                                                            Language.Instance.GetLanguage("Max Level"), delegate() {

                });
                DialogManager.Instance.Push(alert);
            }

            else if (ResultCode == 5) // Not enough diamond
            {
                // Alert Dialog
                DialogDataAlert alert = new DialogDataAlert(Language.Instance.GetLanguage("Upgrade Failed Title"),
                                                            Language.Instance.GetLanguage("Not Enouhg Diamond"), delegate() {

                });
                DialogManager.Instance.Push(alert);
            }
        });
    }

    void UpgradeStr()
    {
        Debug.Log("UpgradeStr");
        Upgrade("Str", CharactorManager.Instance.GetStr());
    }

    void UpgradeDex()
    {
        Debug.Log("UpgradeDex");
        Upgrade("Dex", CharactorManager.Instance.GetDex());
    }

    void UpgradeCon()
    {
        Debug.Log("UpgradeCon");
        Upgrade("Con", CharactorManager.Instance.GetCon());
    }

    void UpgradeInt()
    {
        Debug.Log("UpgradeInt");
        Upgrade("Int", CharactorManager.Instance.GetInt());
    }

    void UpgradeStatPoint()
    {
        Debug.Log("UpgradeStatus");
        Upgrade("StatPoint", CharactorManager.Instance.GetStat());
    }

    void UpgradeLevel()
    {
        Debug.Log("UpgradeLevel");
        Upgrade("Level", CharactorManager.Instance.GetLevel());
    }

    void UpgradeMaxEXP()
    {
        Debug.Log("UpgradeMaxEXP");
        Upgrade("MaxExperience", CharactorManager.Instance.GetMaxEXP());
    }

    public void UpgradeCurrentEXP()
    {
        Debug.Log("UpgradeCurrentEXP");
        Upgrade("Experience", CharactorManager.Instance.GetCurrentEXP());
    }

    public void UpgradeMoney()
    {
        Debug.Log("UpgradeMoney");
        Upgrade("Money", CharactorManager.Instance.GetMoney());
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