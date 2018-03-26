using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRSwitcher : MonoBehaviour 
{
    public static VRSwitcher Instance;

    public GameObject Main_Camera;
    public GameObject UI;
    public GameObject GvrViewer;
    public GameObject player;

    bool isVR = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void VRSwitching()
    {
        CharactorManager.Instance.VRChange();
        Switching();
    }

    public void Switching()
    {
        //메인 카메라의 following camera를 종료, GVR Head를 On, GvrViewer의 동작을 가능하도록 토글.
        Main_Camera.GetComponent<FollowingCamera>().enabled = !Main_Camera.GetComponent<FollowingCamera>().enabled;
        Main_Camera.GetComponent<GvrHead>().enabled = !Main_Camera.GetComponent<GvrHead>().enabled;
        GvrViewer.GetComponent<GvrViewer>().VRModeEnabled = !GvrViewer.GetComponent<GvrViewer>().VRModeEnabled;
        GvrViewer.GetComponent<GvrViewer>().enabled = !GvrViewer.GetComponent<GvrViewer>().enabled;

        Main_Camera.GetComponent<VRFollowingCamera>().enabled = !Main_Camera.GetComponent<VRFollowingCamera>().enabled;

        //UI 표시종료
        UI.SetActive(!UI.active);

        //키보드&마우스입력 On/Off
        player.GetComponent<CharactorFPSController>().enabled = !player.GetComponent<CharactorFPSController>().enabled;
    }
}
