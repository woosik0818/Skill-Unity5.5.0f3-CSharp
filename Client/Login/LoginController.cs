using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using System;
using Boomlagoon.JSON;


public class LoginController : MonoBehaviour {
	User user = new User ();
    //string user_id;
    //string photo_url;
    //string facebook_name;
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
        void Update () {

	}

    //public void LoginFacebook() {

    //	FB.Init (delegate() {
    //		FB.LogInWithReadPermissions (
    //			new List<string> () { "public_profile", "email", "user_friends" },
    //			delegate(ILoginResult result) {
    //				//Debug.Log (result.RawResult);
    //				user.FacebookID = result.ResultDictionary ["user_id"].ToString ();
    //				user.FacebookPhotoURL = "http://graph.facebook.com/" + user.FacebookID + "/picture?type-square";
    //                   PlayerPrefs.SetString("FacebookPhotoURL", user.FacebookPhotoURL);

    //				Debug.Log (user.FacebookPhotoURL);

    //				FB.API ("/me", HttpMethod.GET, delegate (IGraphResult meResult) {
    //                       //Debug.Log (meResult.RawResult);
    //                       user.FacebookName = meResult.ResultDictionary ["name"].ToString ();
    //                       PlayerPrefs.SetString("Name", user.FacebookName);
    //					Debug.Log ("Facebook Name : " + user.FacebookName);
    //					Post();
    //                   });

    //				FB.API ("/me/friends", HttpMethod.GET, delegate (IGraphResult friendResult) {
    //					//Debug.Log (friendResult.RawResult);
    //					FriendResult res = JsonUtility.FromJson<FriendResult> (friendResult.RawResult);
    //					Debug.Log (res.summary.total_count);

    //				});



    //			});

    //	});

    //   }

    public void LoginFacebook()
    {

        FB.Init(delegate () {
            FB.ActivateApp();

            CharactorManager.Instance.FacebookLogin(delegate (bool isSuccess, string response) {
                if(isSuccess)
                {
                    StartCoroutine(LoadDataFromFacebook());
                }
            });
        });
    }

    public IEnumerator LoadDataFromFacebook()
    {
        CharactorManager.Instance.LoadFacebookMe(delegate(bool isSuccess, string reponse) {
            
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

                //int ResultCode = (int)response["ResultCode"].Number;

                //if (ResultCode == 1 || ResultCode == 2)
                //{
                    JSONObject Data = response["Data"].Obj;

                    CharactorManager.Instance.UserID = (int)Data["UserID"].Number;
                    CharactorManager.Instance.AccessToken = Data["AccessToken"].Str;
                //    StartCoroutine(LoadDataFromGameServer());
                //}

                //CharactorManager.Instance.UserID = (int)Data["UserID"].Number;
                StartCoroutine(LoadDataFromGameServer());
            });
    }

    public IEnumerator LoadDataFromGameServer()
    {
        CharactorManager.Instance.Refresh(delegate () {
            finished[2] = true;
        });
        //RankSingleton.Instance.LoadTotalRank(delegate () {
        //    finished[3] = true;
        //});
        //RankSingleton.Instance.LoadFriendRank(delegate () {
        //    finished[4] = true;
        //});
        while (!finished[2] || !finished[3] || !finished[4])
        {
            yield return new WaitForSeconds(0.1f);
        }
        Scenemanagement.Instance.SceneMove("Lobby");
        //yield return null;
        //LoadNextScene();
    }
    // NEED!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    //	public void LoadNextScene()
    //	{
    //		Scenemanagement.LoadSceneMove();
    //	}
    // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    public void Post(){
		//User user = new User ();
		//user.FacebookID = user_id;
		//user.FacebookName = facebook_name;
		//user.FacebookPhotoURL = photo_url;

		string body = JsonUtility.ToJson (user);



        HTTPClient.Instance.POST ("http://skill-server.azurewebsites.net/Login/Facebook", 
        	body, 

            delegate (WWW www) {
				Debug.Log (www.text);
				LoginResult result = JsonUtility.FromJson<LoginResult> (www.text);
				Debug.Log (result.Message);
            });
	}

	/// <summary>
	/// Problelm of Get()
	/// </summary>
	//public void Get(Action callback){
	//	HTTPClient.Instance.GET("http://skill-server.azurewebsites.net/Login/Facebook", 

	//		delegate (WWW www){
				
	//			Debug.Log (www.text);
	//			JSONObject response = JSONObject.Parse(www.text);
	//			int ResultCode = (int)response["ResultCode"].Number;
	//			JSONObject data = response["Data"].Obj;
	//			print((int)data["Level"].Number);
	//			print("???????????????????");
	//			callback();
	//		});
		
	//}

/*
	public string GetID(){
	
		return user_id;
	}

	public string GetURL(){

		return photo_url;
	}

	public string GetName(){
		
		return facebook_name;
	}
*/
}


[Serializable]
public class Friend {
	public string name;
	public string user_id;


}

[Serializable]
public class FriendSummary
{
	public int total_count;

}

[Serializable]
public class FriendResult {

	public Friend[] data;
	public FriendSummary summary;
}