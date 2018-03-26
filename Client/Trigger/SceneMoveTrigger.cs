using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMoveTrigger : MonoBehaviour 
{
    public GameObject btn;
    public string movename;
    public string DestinationName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            btn.SetActive(true);
            btn.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = movename;
            Scenemanagement.Instance.setDestination(DestinationName);
            Scenemanagement.Instance.triggerIn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            btn.SetActive(false);
            Scenemanagement.Instance.triggerOut();
        }
    }
}
