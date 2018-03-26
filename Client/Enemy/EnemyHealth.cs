using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EnemyHealth : MonoBehaviour 
{
    ParticleSystem Particle1 = null;
    ParticleSystem Particle2 = null;

    public int startingHealth = 100;
	public int currentHealth;

	public int exp = 10;
	public int money = 100;

	public float flashSpeed = 5f;
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

	public float sinkSpeed = 1f;
	
	protected GameObject player;

	AudioSource playerAudio;
	bool isDead;
	bool isSinking;
	bool damaged;
		
	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		playerAudio = GetComponent <AudioSource> ();
		currentHealth = startingHealth;

        Particle1 = transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
        Particle2 = transform.GetChild(2).gameObject.GetComponent<ParticleSystem>();
    }

    public void Damage(int damage, Vector3 playerPosition, float pushBack, string effectPrefab = "", string audio = "")
    {
        // 공격은 죽지 않았을때만 받습니다.
        if (!isDead)
        {
            // 가끔 MissingReferenceException 예외가 발생하는데 발생해도 스킵하도록 예외처리합니다.
            try
            {
                // 데미지1: 데미지를 몬스터에 체력에 반영합니다.
                TakeDamage(damage);

                // 데미지2: 몬스터를 뒤로 밀려나게 합니다. 뭔가 타격 받을 때 액션성을 더해줍니다.
                PushBack(playerPosition, pushBack);

                // 데미지3: 몬스터가 데미지를 입었을 때 화면에 받은 데미지가 뜨도록 합니다.
                ShowDamageText(damage);

                // 데미지4: 데미지의 이펙트를 화면에 재생합니다. 프리팹의 경로를 매개변수로 전달합니다. Resources 폴더 하위 기준입니다.
                //DamageEffect(effectPrefab);

                // 데미지5: 데미지를 입었을 때 재생할 오디오파일의 경로입니다. Resources 폴더 하위 기준입니다.
                PlaySound(audio);

            }

            catch (MissingReferenceException e)
            {
                // 이 예외는 발생해도 그냥 무시하겠습니다.
                Debug.Log(e.ToString());
            }
        }
    }

    void PushBack(Vector3 playerPosition, float pushBack)
    {
        // 주인공 캐릭터의 위치와 몬스터의 위치의 차이 벡터를 구합니다.
        Vector3 diff = playerPosition - transform.position;
        // 주인공과 몬스터 사이의 차이를 정규화시킵니다. (거리가 1로 만드는 것을 정규화라고 함)
        diff = diff / diff.sqrMagnitude;
        // 현재 몬스터의 RigidBody에 힘을 가합니다. 
        // 플레이어 반대방향으로 밀려나는데, pushBack만큼 비례해서 더 밀려납니다.
        GetComponent<Rigidbody>().AddForce(diff * -10000f * pushBack);
    }

    void PlaySound(string audio)
    {
        if (audio != "")
        {
            // 몬스터에는 AudioSource 콤포넌트가 없으므로,
            // AudioSource.PlayClipAtPoint 함수로 몬스터의 위치에서 바로 재생합니다.
            AudioSource.PlayClipAtPoint(Resources.Load(audio) as AudioClip, transform.position, 0.1f);
        }
    }

    public void TakeDamage (int amount)
	{
		damaged = true;
		
		currentHealth -= amount;

        Particle1.Play();
        Particle2.Play();
        gameObject.GetComponent<AudioSource>().Play();

        ShowDamageText(amount);

        if (currentHealth <= 0 && !isDead)
		{
            CharactorManager.Instance.TakeEXP(exp);
            CharactorManager.Instance.TakeMoney(money);
			Death ();
        }
	}

    void ShowDamageText(int damage)
    {

        // 데미지를 화면에 표시할 DamageText 프리팹을 화면에 생성합니다.
    
        GameObject damageObj = DamageTextPool.Instance.GetObject();  //Instantiate(Resources.Load("Prefab/DamageText"), transform.position+ new Vector3(0f,0.5f,-0.5f), new Quaternion()) as GameObject;
        damageObj.transform.position = transform.position + new Vector3(0f, 0.5f, -0.5f);

        damageObj.transform.rotation = new Quaternion();

        // DamageText 프리팹에 표시할 데미지 수치를 입력합니다.
        damageObj.transform.GetChild(0).GetComponent<Text>().text = damage.ToString();
        damageObj.GetComponent<DamageText>().Play (damage);
    }

    public IEnumerator StartDamage(int damage, Vector3 playerPosition, float delay, float pushBack)
	{
		yield return new WaitForSeconds(delay);

		try
        {
			TakeDamage(damage);
			
			Vector3 diff = playerPosition - transform.position;
			diff = diff / diff.sqrMagnitude;
			GetComponent<Rigidbody>().AddForce((transform.position - new Vector3(diff.x,diff.y,0f))*50f*pushBack);

		}

        catch(MissingReferenceException e)
		{
			Debug.Log (e.ToString());
		}
	}
	
	void Update ()
	{
		if(damaged)
		{
			transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_OutlineColor", flashColour);
		}
	
        else
		{
			transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.Lerp (transform.GetChild(0).GetComponent<Renderer>().material.GetColor("_OutlineColor"), Color.black, flashSpeed * Time.deltaTime));
		}
		damaged = false;
		
		// If the enemy should be sinking...
		if(isSinking)
		{
			// ... move the enemy down by the sinkSpeed per second.
			transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
		}
	}

    void Death()
    {
        // The enemy is dead.
        isDead = true;

        // Turn the collider into a trigger so shots can pass through it.
        BoxCollider collider = transform.GetComponentInChildren<BoxCollider>();
        collider.isTrigger = true;

        StartSinking();
        GetComponent<EnemyItem>().ItemDrop();

        UpgradeController.Instance.Upgrade();
    }
	
	public void StartSinking ()
	{
		// Find and disable the Nav Mesh Agent.
		GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;
		
		// Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
		GetComponent <Rigidbody> ().isKinematic = true;
		
		// The enemy should no sink.
		isSinking = true;
		
		// After 2 seconds destory the enemy.
		Destroy (gameObject, 2f);
	}
}