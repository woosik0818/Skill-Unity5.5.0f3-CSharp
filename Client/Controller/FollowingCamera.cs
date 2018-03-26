using UnityEngine;
using System.Collections;

// FollowingCamera
// 주인공 캐릭터를 카메라가 일정한 거리를 유지한 채로 따라다니게 합니다
public class FollowingCamera : MonoBehaviour
{
	public float distanceAway = 7f;			
	public float distanceUp   = 4f;

    // 따라다닐 객체를 지정합니다.
    public Transform follow;

    Quaternion CameraRotation;

    private void Awake()
    {
        CameraRotation = transform.rotation;
    }

    void LateUpdate ()
	{
        Vector3 targetPos = follow.position + Vector3.up * distanceUp - Vector3.forward * distanceAway;
        // 카메라의 위치를 distanceUp 만큼 위에, distanceAway 만큼 앞에 위치시킵니다.

        transform.position = Vector3.Slerp(transform.position, targetPos, Time.deltaTime * 7f);
    }

    public void LookTarget()
    {
        transform.rotation = CameraRotation;
    }
}