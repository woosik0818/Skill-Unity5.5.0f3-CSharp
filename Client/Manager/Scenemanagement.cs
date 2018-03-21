using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenemanagement : MonoBehaviour {

    public static Scenemanagement Instance;
    string Destination;
    bool isOnTheTirgger = false;
    // Use this for initialization
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Debug.LogError("Not Single Scenemanagement!");
    }
    public void SceneMove(string name)
    {
        LoadingSceneManager.LoadScene(name);
    }
    public void SceneMove()
    {
        CharactorManager.Instance.SetVRMode();
        LoadingSceneManager.LoadScene(Destination);
    }

    public void setDestination(string name)
    {
        Destination = name;
    }

    public bool GetOnTheTrigger()
    {
        return isOnTheTirgger;
    }
    public void triggerIn()
    {
        isOnTheTirgger = true;
    }
    public void triggerOut()
    {
        isOnTheTirgger = false;
    }
}
