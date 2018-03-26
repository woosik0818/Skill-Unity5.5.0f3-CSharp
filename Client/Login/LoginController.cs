using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using System;
using Boomlagoon.JSON;

public class LoginController : MonoBehaviour 
{
	User user = new User ();

    // Use this for initialization
    public GameObject BtnFacebook;
    bool[] finished = new bool[10];

    void Start()
    {
        for (int i = 0; i < finished.Length; i++)
        {
            finished[i] = false;
        }
    }

    // Update is called once per frame
    void Update () 
    {

	}

    public void LoginFacebook()
    {

        FB.Init(delegate () 
        {
            FB.ActivateApp();
            CharactorManager.Instance.FacebookLogin(delegate (bool isSuccess, string response) 
            {
                if(isSuccess)
                {
                    StartCoroutine(LoadDataFromFacebook());
                }
            });
        });
    }

    public IEnumerator LoadDataFromFacebook()
    {
        CharactorManager.Instance.LoadFacebookMe(delegate(bool isSuccess, string reponse) 
        {
            
        });

        yield return new WaitForSeconds(0.1f);
        LoginGameServer();
    }

    public void LoginGameServer()
    {
        JSONObject body = new JSONObject();
        body.Add("FacebookID", CharactorManager.Instance.FacebookID);
        body.Add("FacebookAccessToken", CharactorManager.Instance.FacebookAccessToken);
        body.Add("FacebookName", CharactorManager.Instance.Name);
        body.Add("FacebookPhotoURL", CharactorManager.Instance.FacebookPhotoURL);

        HTTPClient.Instance.POST("http://skill-server.azurewebsites.net/Login/Facebook",
            body.ToString(),
            delegate (WWW www)
            {
                Debug.Log(www.text);
                JSONObject response = JSONObject.Parse(www.text);
                JSONObject Data = response["Data"].Obj;

                CharactorManager.Instance.UserID = (int)Data["UserID"].Number;
                CharactorManager.Instance.AccessToken = Data["AccessToken"].Str;
                
                StartCoroutine(LoadDataFromGameServer());
            });
    }

    public IEnumerator LoadDataFromGameServer()
    {
        CharactorManager.Instance.Refresh(delegate () 
        {
            finished[2] = true;
        });

        while (!finished[2] || !finished[3] || !finished[4])
        {
            yield return new WaitForSeconds(0.1f);
        }

        Scenemanagement.Instance.SceneMove("Lobby");
    }
   
    public void Post()
    {
		string body = JsonUtility.ToJson (user);

        HTTPClient.Instance.POST ("http://skill-server.azurewebsites.net/Login/Facebook", 
        	body, 
            delegate (WWW www) 
            {
				Debug.Log (www.text);
				LoginResult result = JsonUtility.FromJson<LoginResult> (www.text);
				Debug.Log (result.Message);
            });
	}
}


[Serializable]
public class Friend 
{
	public string name;
	public string user_id;
}

[Serializable]
public class FriendSummary
{
	public int total_count;
}

[Serializable]
public class FriendResult 
{
	public Friend[] data;
	public FriendSummary summary;
}