using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMagic : MonoBehaviour
{
	// 주인공의 시작 마나입니다. 기본 100으로 설정되있습니다.
	public float startingMP;
	// 주인공의 현재 MP입니다. 
	public float currentMP;
	// 체력게이지 UI와 연결된 변수입니다.
	public Image mpBar;
    public Image mpBar_VR;
	protected PlayerHealth playerHealth;
	// 애니메이터 콘트롤러에 매개변수를 전달하기 위해 연결한 Animator 콤포넌트
	Animator anim;
	// 플레이어의 움직임을 관리하는 PlayerMovement 스크립트 콤포넌트
	PlayerMovement playerMovement;
	bool isDead;

	void Start(){
		playerHealth = GetComponent<PlayerHealth> ();
	}

	// 오브젝트가 시작하면 호출되는 Awake() 함수입니다.
	void Awake ()
	{
		// Player 게임 오브젝트에 붙어있는 Animator 콤포넌트를 찾아서 변수에 넣습니다.
		anim = GetComponent <Animator> ();
		// Player 게임 오브젝트에 붙어있는 PlayerMovement 콤포넌트를 찾아서 변수에 넣습니다.
		playerMovement = GetComponent <PlayerMovement> ();
        // 현재 체력을 최대 체력으로 설정합니다.
        startingMP = CharactorManager.Instance.GetMP();
        MPReset();
	}

	// 플레이어가 공격받았을 때 호출되는 함수입니다.
	public void useSkill (int amount)
	{

		// 공격을 받으면 amount만큼 체력을 감소시킵니다.
		currentMP -= amount;

        // 체력게이지에 변경된 체력값을 표시합니다.
        mpBar.fillAmount = currentMP / startingMP;
        mpBar_VR.fillAmount = currentMP / startingMP;


    }

	public void MpPotion ()
	{
		isDead = playerHealth.isDead;
		if (isDead == false) {
			if (currentMP < startingMP) {
				if ((currentMP + 20) <= startingMP) {
					currentMP += 20;
				} else {
					currentMP = startingMP;
				}
			}
		}
        mpBar.fillAmount = currentMP / startingMP;
        mpBar_VR.fillAmount = currentMP / startingMP;
    }

    public void MPStat(int amount)
    {
        startingMP = amount;
        mpBar.fillAmount = currentMP / startingMP;
        mpBar_VR.fillAmount = currentMP / startingMP;
    }

    public void MPReset()
    {
        currentMP = startingMP;
        mpBar.fillAmount = currentMP / startingMP;
        mpBar_VR.fillAmount = currentMP / startingMP;
    }
}