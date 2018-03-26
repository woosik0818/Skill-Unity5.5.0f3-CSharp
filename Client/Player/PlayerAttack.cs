using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour 
{
    public GameObject NormarTergetPref;
    public GameObject SkillTargetPref;
    public GameObject DashPref;

	public int SkillMP = 20;
	// 캐릭터의 공격 반경입니다. 
	// 타겟의 Trigger로 어떤 몬스터가 공격 반경 안에 들어왔는지 판정합니다.
	public Transform normalTarget;
	protected PlayerMagic mp;

	// 다른 스크립트에서 이 스크립트로 바로 접근하기 위한 통로라고 보시면 됩니다. 
	// PlayerAttack.Instance 이런 식으로 바로 접근 가능합니다.
	public static PlayerAttack Instance;
	// 주인공 캐릭터에 붙어있는 오디오 소스입니다. 
	// 이 오디오 소스로 주인공 캐릭터의 목소리를 재생합니다.
	public AudioSource audioSource;

	// 이 PlayerAttack 스크립트가 붙어있는 게임 오브젝트가 씬에 생성될 때 호출됩니다.
	void Start()
	{
		// Instance에 자기 자신을 할당합니다. 
		// 이로 인해, 외부 스크립트에서 PlayerAttack.Instance 이렇게 접근 가능합니다.
		Instance = this;
        // 현재 이 게임 오브젝트에 붙어있는 오디오 소스 콤포넌트를 변수에 할당합니다.
        audioSource = GetComponent<AudioSource>();
	}

	public void NormalAttack()
	{
        // 일반 공격을 할 때 재생될 수 있는 주인공 캐릭터의 목소리들입니다. 
        // 이 목소리들 중 하나가 랜덤으로 재생됩니다.
        string[] attackSound = {"VoiceSample/13.attack_B1", "VoiceSample/13.attack_B1", "VoiceSample/14.attack_B2", "VoiceSample/15.attack_B3", "VoiceSample/16.attack_C1", "VoiceSample/17.attack_C2", "VoiceSample/18.attack_C3"};

        // 목소리 리스트 중에서 하나를 랜덤으로 재생시키는 함수입니다.
        PlayRandomVoice(attackSound);

        GameObject obj = Instantiate(NormarTergetPref, normalTarget.position, normalTarget.rotation);
        obj.transform.SetParent(transform.GetChild(0));
    }

	public void DashAttack()
	{
        // 대시 공격을 할 때 재생될 수 있는 주인공 캐릭터의 목소리들입니다. 
        // 이 목소리들 중 하나가 랜덤으로 재생됩니다.
        string[] attackSound = {"VoiceSample/10.attack_A1", "VoiceSample/11.attack_A2", "VoiceSample/12.attack_A3"};

        // 목소리 리스트 중에서 하나를 랜덤으로 재생시키는 함수입니다.
        PlayRandomVoice(attackSound);

        // 대시 공격을 할 때에는 주인공에게도 이펙트가 발생하도록 합니다.
        PlayEffect("SkillAttack2");

        GameObject obj = Instantiate(DashPref, transform.GetChild(0).transform.position, transform.GetChild(0).transform.rotation);
        obj.transform.SetParent(transform.GetChild(0));
    }

	public void SkillAttack()
	{
        mp = GetComponent<PlayerMagic>();
        if (mp.currentMP >= SkillMP)
        {
            mp.useSkill(SkillMP);

            // 스킬 공격을 할 때 재생될 수 있는 주인공 캐릭터의 목소리들입니다. 
            // 이 목소리들 중 하나가 랜덤으로 재생됩니다.
            string[] attackSound = { "VoiceSample/10.attack_A1", "VoiceSample/11.attack_A2", "VoiceSample/12.attack_A3", "VoiceSample/13.attack_B1", "VoiceSample/14.attack_B2", "VoiceSample/15.attack_B3", "VoiceSample/16.attack_C1", "VoiceSample/17.attack_C2", "VoiceSample/18.attack_C3" };

            // 목소리 리스트 중에서 하나를 랜덤으로 재생시키는 함수입니다.
            PlayRandomVoice(attackSound);

            GameObject obj = Instantiate(SkillTargetPref, transform.GetChild(0).transform.position, transform.GetChild(0).transform.rotation);
            obj.transform.SetParent(transform.GetChild(0));
        }
    }
	
	// 랜덤으로 목소리를 재생하는 함수입니다.
	void PlayRandomVoice(string[] attackSound)
	{
		// 간단하게 string 리스트의 길이 중 0 부터 길이 - 1 사이의 숫자를 아무거나 선택합니다.
		// UnityEngine.Random 라이브러리를 사용합니다.
		int rand = UnityEngine.Random.Range(0,attackSound.Length);

		// 주인공 캐릭터에 붙어있는 오디오 소스를 활용해서 재생합니다. 
		audioSource.PlayOneShot(Resources.Load(attackSound[rand]) as AudioClip);

	}

	// 주인공 캐릭터에게 발생하는 이펙트를 생성합니다.
	void PlayEffect(string prefabName)
	{
		if(prefabName == "SkillAttack1")
        {
			GameObject effect = SkillAttack1Pool.Instance.GetObject();
			effect.transform.position = transform.position+ new Vector3(0f,0.5f,-0.5f);
			effect.GetComponent<SkillAttack1>().Play();
		}

        else if(prefabName == "SkillAttack2")
        {
			GameObject effect = SkillAttack2Pool.Instance.GetObject();
			effect.transform.position = transform.position+ new Vector3(0f,0.5f,-0.5f);
			effect.GetComponent<SkillAttack2>().Play();
		}
	}
}
