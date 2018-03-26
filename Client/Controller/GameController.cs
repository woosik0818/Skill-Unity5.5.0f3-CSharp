using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour 
{
	
	// Use this for initialization
	void Start () {

		User user = new User ();
		string body = JsonUtility.ToJson(user);

		HTTPClient.Instance.POST("http://skill-server.azurewebsites.net/Login/Facebook", 
			body, 
			delegate (WWW www)
            {
				Debug.Log(www.text);
				LoginResult result = JsonUtility.FromJson<LoginResult>(www.text);
				Debug.Log(result.Message);
			});
	}

	// Update is called once per frame
	void Update () 
    {

	}
}