using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooltimeManager : MonoBehaviour 
{
    public static CooltimeManager Instance;
    public UnityEngine.UI.Image[] filter;
    public UnityEngine.UI.Text[] cooltimeText;

    public float[] Cooltime;
    public int[] Cooltimetemp;

    int UIslotCount;

    // Use this for initialization
    void Awake () 
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Debug.LogError("Not Single CooltimeManager!");

        for (int i = 0; i < filter.Length; i++)
        {
            filter[i].fillAmount = 0;
            cooltimeText[i].text = "";
        }
    }
}
