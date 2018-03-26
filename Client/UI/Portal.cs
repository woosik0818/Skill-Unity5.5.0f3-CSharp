using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour 
{
    public Transform Maincamera;

	void Update () 
    {
        transform.LookAt(Maincamera);
	}
}
