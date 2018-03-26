using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Boomlagoon.JSON;
using System.Collections.Generic;

public class StageController : MonoBehaviour 
{

	// StageController를 싱글톤 클래스처럼 아무곳에서나 접근하기 위한 static 변수입니다.
	public static StageController Instance;

	// Stage의 포인트를 관리하는 변수입니다. 몬스터를 하나 잡을때마다 포인트가 증가하게 됩니다.
	public int StagePoint = 0;

	// 화면 상단에 표시될 포인트의 Text 오브젝트를 가리키는 변수입니다.
	public Text PointText;

	// Start 함수에서 Instance 변수를 설정해줍니다.
	void Start () 
    {
		Instance = this;

		SlimePool.Instance.Init();
		DamageTextPool.Instance.Init();
		SkillAttack1Pool.Instance.Init();
		SkillAttack2Pool.Instance.Init();
    }

	// 플레이어가 몬스터를 죽였을 때, 포인트를 증가시켜주는 함수입니다.
	public void AddPoint(int Point)
	{
		StagePoint += Point;
		PointText.text = StagePoint.ToString();
	}
}
