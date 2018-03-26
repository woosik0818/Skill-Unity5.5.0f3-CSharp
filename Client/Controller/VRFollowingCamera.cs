using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRFollowingCamera : MonoBehaviour 
{    
    public Transform Target;
	
    // Update is called once per frame
	void LateUpdate () 
    {
        transform.LookAt(Target);
    }
}