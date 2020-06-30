using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour

{
    public Transform attackPoint;
    public float attackRange = 5f;
    public LayerMask enemyLayers;
    public EnemyHealth enemyHealth;
    public GameObject projectile;
    public float playerHealth = 100f;
    public Animator animator;
    public PlayerMovement playerMovement;
    public AudioManager audioManager;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MeleeAttack();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RangedAttack();
        }

        if (playerHealth <= 0)
        {
            //StartCoroutine(Hit());
            playerMovement.Die();   
        }

        //if (playerMovement.holdingWall == true)
        //{
        //    FlipAnimation();
        //}


    }

    private void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement>();
    }

    public void MeleeAttack()
    {
        animator.SetBool("isSwinging", true);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            //Debug.Log(enemy);
            HitEnemy();
            enemyHealth = enemy.GetComponent<EnemyHealth>();
            enemyHealth.health -= 10;
        }

        if (playerMovement.holdingWall == true)
        {
            StartCoroutine(WallAttackFlip());
        }
    }

    public void RangedAttack()
    {
        audioManager.Play("Shot");

        if (playerMovement.holdingWall == true)
        {
            gameObject.transform.Rotate(0f, 180f, 0f);
            Instantiate(projectile, attackPoint.position, attackPoint.rotation);
            gameObject.transform.Rotate(0f, 180f, 0f);
        }
        else
        {
            Instantiate(projectile, attackPoint.position, attackPoint.rotation);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void HitEnemy()
    {
        //Debug.Log("MeleeAttack");
    }

    IEnumerator Hit()
    {
        //Debug.Log("HitEvent");
        animator.SetTrigger("Hit");
        yield return new WaitForSeconds(.2f);
        //Destroy(gameObject);
    }

    void MeleeAnimFinish()
    {
        animator.SetBool("isSwinging", false);

    }

    IEnumerator WallAttackFlip()
    {
        transform.Rotate(0f, 180f, 0f);
        yield return new WaitForSeconds(.5f);
        transform.Rotate(0f, 180f, 0f);
    }

    public void PlaySword()
    {
        audioManager.Play("Sword");
    }

    public void PlayShot()
    {
        audioManager.Play("Shot");
    }

    public void PlayMove()
    {
        audioManager.Play("Step");
    }
}
