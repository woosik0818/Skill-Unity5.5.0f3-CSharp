using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillAttack : MonoBehaviour {

    public int Skill;
    public float RetentionTime = 1f;
    public float Nuckback = 2f;

    private void Awake()
    {
        Invoke("DestroyObj", 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (other.tag == "Enemy")
        {
            StartCoroutine(enemy.StartDamage(CharactorManager.Instance.GetAttack() * Skill, transform.position, 0.5f, Nuckback));
        }
    }

    void DestroyObj()
    {
        Destroy(gameObject);
    }
}
