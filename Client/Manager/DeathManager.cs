using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour 
{
    public static DeathManager Instance;
    public GameObject DeathScreen;
    public UnityEngine.UI.Text EXPText;

    public Transform SpawnPoint;        // 맵 부활을 선택했을 시 부활 위치
    public GameObject Player;

    int looseEXP;

    // Use this for initialization
	void Awake ()
    {
        if (Instance == null)
            Instance = this;
        else if(Instance != this)
            Debug.LogError("Not Single DeathManager!");
    }

    public void Death()
    {
        DeathScreen.SetActive(true);
        looseEXP = CharactorManager.Instance.LooseEXP();
        EXPText.text = "" + looseEXP;
    }

    public void SceneSpawn()        // 맵 부활을 선택했을 시에 동작
    {
        Player.transform.position = SpawnPoint.position;
        Player.GetComponent<PlayerHealth>().HPReset();
        Player.GetComponent<PlayerMagic>().MPReset();
    }
}
