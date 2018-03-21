using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStartManager : MonoBehaviour {

    public static SceneStartManager Instance;

    public GameObject player;

    public int[] p_Item;

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
    void Start () {
        if (Instance == null)
            Instance = this;
        UpdatePlayerData();
        SetLevelText(CharactorManager.Instance.GetLevel());
    }

    void UpdatePlayerData()
    {
        STR_Text.text = "" + CharactorManager.Instance.GetStr();
        DEX_Text.text = "" + CharactorManager.Instance.GetDex();
        INT_Text.text = "" + CharactorManager.Instance.GetInt();
        CON_Text.text = "" + CharactorManager.Instance.GetCon();

        StatPoint_Text.text = "" + CharactorManager.Instance.GetStat();
        Money_Text.text = "" + CharactorManager.Instance.GetMoney();

        HP_Text.text = "" + CharactorManager.Instance.GetHP();
        MP_Text.text = "" + CharactorManager.Instance.GetMP();
        Attack_Text.text = "" + CharactorManager.Instance.GetAttack();
        Defence_Text.text = "" + CharactorManager.Instance.GetDefence();
        Critical_Text.text = "" + CharactorManager.Instance.GetCritical();
        CriticalDam_Text.text = "" + CharactorManager.Instance.GetCriticalDam();
        MagicDamage_Text.text = "" + CharactorManager.Instance.GetMagicDam();
    }

    public void SetLevelText(int level)
    {
        LevelText.text = "" + level;
        UpdatePlayerData();
    }

    public void StatAllowButton()
    {
        CharactorManager.Instance.AllowChange();
        UpdatePlayerData();
    }
    public void STRUP()
    {
        CharactorManager.Instance.StrUp();
        UpdatePlayerData();
    }
    public void DEXUP()
    {
        CharactorManager.Instance.DexUp();
        UpdatePlayerData();
    }
    public void INTUP()
    {
        CharactorManager.Instance.IntUp();
        UpdatePlayerData();
    }
    public void CONUP()
    {
        CharactorManager.Instance.ConUp();
        UpdatePlayerData();
    }
    public void StatNotAllow()
    {
        CharactorManager.Instance.ReturnTemp();
        UpdatePlayerData();
    }

    public void EXPMaxValue()
    {
        EXPBar.maxValue = CharactorManager.Instance.GetMaxEXP();
        ChangeEXP();
    }
    public void ChangeEXP()
    {
        EXPBar.value = CharactorManager.Instance.GetCurrentEXP();
    }
    public void MoneyUp()
    {
        Money_Text.text ="" + CharactorManager.Instance.GetMoney();
        UpdatePlayerData();
    }
    public void PlayerSet()
    {
        player.GetComponent<PlayerHealth>().HpStat(CharactorManager.Instance.GetHP());
        player.GetComponent<PlayerMagic>().MPStat(CharactorManager.Instance.GetMP());
    }
}
