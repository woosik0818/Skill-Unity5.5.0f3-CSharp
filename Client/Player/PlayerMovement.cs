using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]  
public class PlayerMovement: MonoBehaviour {
	
	protected Animator avatar;
	protected PlayerAttack playerAttack;
	protected PlayerHealth playerHealth;
	protected PlayerMagic playerMagic;

	float lastAttackTime, lastskillTime, lastDashTime, lastHpTime, lastMpTime;
	public bool attacking = false;
	//public bool dashing = false;
	public bool Hp = false;
	public bool Mp = false;
	public bool attackmp = false;

	void Start () 
	{
		avatar = GetComponent<Animator>();
		playerAttack = GetComponent<PlayerAttack> ();
		playerHealth = GetComponent<PlayerHealth> ();
		playerMagic = GetComponent<PlayerMagic> ();
	}
    
	float h, v;

	public void OnStickChanged(Vector2 stickPos)
	{
		h = stickPos.x;
		v = stickPos.y;
	}

	void Update () 
	{

		if(avatar)
		{
			float back = 1f;
			if (v < 0f)
				back = -1f;
			
			avatar.SetFloat("Speed", (h*h+v*v));
            avatar.SetFloat("Direction", back * (Mathf.Atan2(h, v) * 180.0f / 3.14159f));

		    Rigidbody rigidbody = GetComponent<Rigidbody>();

            if(rigidbody)
            {
				Vector3 speed = rigidbody.velocity;
				speed.x = 4 * h;
				speed.z = 4 * v;
				rigidbody.velocity = speed;
				if(h != 0f && v != 0f){
					transform.rotation = Quaternion.LookRotation(new Vector3(h, 0f, v));
				}

            }

		}		
	}

	public void OnAttackDown()
	{
		attacking = true;
		avatar.SetBool ("Combo", true);
		StartCoroutine (StartAttack ());
	}

	public void OnAttackUp()
	{
		avatar.SetBool ("Combo", false);
		attacking = false;
	}

	IEnumerator StartAttack()
	{
		if(Time.time - lastAttackTime>1f)
		{
			lastAttackTime = Time.time;
			while(attacking){
				avatar.SetTrigger ("AttackStart");
				playerAttack.NormalAttack ();
				yield return new WaitForSeconds (1f);
			}
		}
	}

	public void OnSkillDown()
	{
		if (Time.time - lastskillTime > 1f) {
			lastskillTime = Time.time;	
			//attackmp = playerAttack.SkillAttack ();
			avatar.SetTrigger("Skill");
            playerAttack.SkillAttack();
        }
	}

	//public void OnSkillUp()
	//{
	//	avatar.SetBool ("Skill", false);
	//}

	public void OnDashDown()
	{
		if(Time.time - lastDashTime > 1f){
			lastDashTime = Time.time;
			//dashing = true;
			avatar.SetTrigger ("Dash");
			playerAttack.DashAttack ();
		}
	}

	public void OnDashUp()
	{
		//dashing = false;
	}

	public void OnHpDown()
	{
		if (Time.time - lastHpTime > 1f) {
			lastHpTime = Time.time;	
			Hp = true;
			playerHealth.HpPotion ();
		}
	}

	public void OnHpUp()
	{
		Hp = false;
	}

	public void OnMpDown()
	{
		if (Time.time - lastMpTime > 1f) {
			lastMpTime = Time.time;	
			Mp = true;
			playerMagic.MpPotion ();
		}
	}

	public void OnMpUp()
	{
		Mp = false;
	}
}
