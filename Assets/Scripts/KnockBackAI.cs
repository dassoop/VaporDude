using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackAI : MonoBehaviour
{
    public LayerMask playerLayer;
    public EnemyHealth enemyHealth;
    public Rigidbody2D rbPlayer;
    public PlayerMovement playerMovement;
    public float attackRange = 5f;
    public float knockBackForce = 5000f;

    private void Update()
    {
        Collider2D hitPlayer = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);

        if (hitPlayer != null)
        {
            Debug.Log("KnockBackPlayer");
            rbPlayer.AddForce(new Vector2(knockBackForce * playerMovement.knockBackDirection, 0f));
            enemyHealth.Die();
        }
    }

    public void AttackPlayer()
    {
        //Collider2D hitPlayer = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
