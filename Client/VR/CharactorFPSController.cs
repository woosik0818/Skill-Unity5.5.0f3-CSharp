using UnityEngine;
using System.Collections;
 
public class CharactorFPSController : MonoBehaviour
{
    public GameObject Main_Camera;
    Rigidbody rig;

    float MouseX;
    float speed = 8f;
    float rotSpeed = 5.0f;
    // Use this for initialization
    void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            VRSwitcher.Instance.VRSwitching();
            Main_Camera.GetComponent<FollowingCamera>().LookTarget();
        }

        if (!(GetComponent<PlayerHealth>().isDead))
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(-Vector3.forward * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(-Vector3.right * speed * Time.deltaTime);
            }

            MouseX = Input.GetAxis("Mouse X");

            transform.Rotate(Vector3.up * rotSpeed * MouseX);


            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                GetComponent<PlayerMovement>().OnAttackDown();
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                GetComponent<PlayerMovement>().OnAttackUp();
            }

            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                SkillEquipManager.Instance.SkillUse(0);
            }
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                SkillEquipManager.Instance.SkillUse(1);
            }
            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                SkillEquipManager.Instance.SkillUse(2);
            }
            if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                SkillEquipManager.Instance.SkillUse(3);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                SkillEquipManager.Instance.UISlotNumCount();
            }

            if (Scenemanagement.Instance.GetOnTheTrigger())
                if (Input.GetKeyDown(KeyCode.Space))                 //포탈이동
                {
                    Scenemanagement.Instance.SceneMove();
                }
        }
    }
}