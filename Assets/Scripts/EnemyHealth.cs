using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 10;
    public Animator animator;
    public CircleCollider2D enemyCollider;
    public AudioManager audioManager;
    public bool hasExploded = false;

    private void Awake()
    {
        hasExploded = false;
    }

    private void Update()
    {
        if (health <= 0)
        {

            StartCoroutine(Death());
        }
    }

    public void Die()
    {
        //PlayExplode();
        StartCoroutine(Death());
        //AudioManager.instance.Play("Explosion");
    }

    IEnumerator Death()
    {
        //Debug.Log("oof");
        enemyCollider.enabled = false;
        animator.SetTrigger("Explode");
        PlayExplode();
        yield return new WaitForSeconds(.4f);
        Destroy(gameObject);
    }

    public void PlayExplode()
    {
        if (hasExploded == false)
        {
            AudioManager.instance.Play("Explosion");
            Debug.Log("Boom");
            hasExploded = true;
        }
        else
        {
            return;
        }

    }
}
