using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject projectile;
    public Transform attackPoint;
    private float attackDelay;

    private void Start()
    {
        Attack();
    }

    public void Attack()
    {
        StartCoroutine(AttackPause());
    }

    IEnumerator AttackPause()
    {
        attackDelay = Random.Range(1.5f, 3.5f);
        Instantiate(projectile, attackPoint.position, attackPoint.rotation);
        yield return new WaitForSeconds(attackDelay);
        Attack();
    }
}
