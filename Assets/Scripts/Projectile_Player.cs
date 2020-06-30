using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Player : MonoBehaviour
{
	public Rigidbody2D rb;
	public float speed = 20f;
	public Animator animator;
	public BoxCollider2D collider;
	public string playerName;
    public AudioManager audioManager;

	private void Start()
	{
        rb.velocity = transform.right * speed;
		StartCoroutine(Despawn());
	}

	void OnTriggerEnter2D(Collider2D hitInfo)
	{
		//Debug.Log(hitInfo.name);
		StartCoroutine(Hit());
		EnemyHealth enemyHealth = hitInfo.GetComponent<EnemyHealth>();
		PlayerCombat player = hitInfo.GetComponent<PlayerCombat>();

		if (hitInfo.name == (playerName))
		{
			if (player != null)
			{
				//player.playerHealth -= 100f;
			}
		}

		if (enemyHealth == null)
		{
			return;
		}

		enemyHealth.health -= 10;
	}

	IEnumerator Hit()
	{
		collider.enabled = false;
		rb.velocity = transform.right * 0;
		animator.SetTrigger("Hit");
		yield return new WaitForSeconds(.5f);
		Destroy(gameObject);
	}

	IEnumerator Despawn()
	{
		yield return new WaitForSeconds(.3f);
		Destroy(gameObject);
	}
}



