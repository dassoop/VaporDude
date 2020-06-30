using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	public Animator animator;
    public Transform playerSpawnPoint;
    public GameObject player;
    public PlayerCombat playerCombat;
    public LayerMask ladderLayer;
    public CharacterController2D charController2D;
    public RaycastHit2D hitInfo;
    public Vector2 wallCheckStartPoint;
    public Transform wallCheckSpot;
    public Rigidbody2D rb;
    public Transform attackPoint;
    public float knockBackDirection;

	public float runSpeed = 40f;
    private float wallCheckLength = .5f;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;
    public bool holdingWall = false;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update ()
    {

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
			animator.SetBool("IsJumping", jump);
		}

		if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
		}

        else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}

        if (transform.position.y < -7)
        {
            Die();
        }


        if (controller.m_FacingRight)
        {
            knockBackDirection = -1f;
        }

        else
        {
            knockBackDirection = 1f;
        }

	}

	public void OnLanding ()
	{
		animator.SetBool("IsJumping", false);
        controller.hasDoubleJumped = false;
        controller.hasJumped = false;
        controller.extraJumpValue = controller.extraJump;
	}

	public void OnCrouching (bool isCrouching)
	{
		animator.SetBool("IsCrouching", isCrouching);
	}

	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;

        wallCheckStartPoint = new Vector2(wallCheckSpot.transform.position.x, wallCheckSpot.transform.position.y);

        if (charController2D.m_FacingRight == true)
        {
            hitInfo = Physics2D.Raycast(wallCheckStartPoint, Vector2.right, wallCheckLength, ladderLayer);
            //Debug.DrawRay();
            //Debug.Log(hitInfo.transform.gameObject.layer);
        }
        
        else
        {
            hitInfo = Physics2D.Raycast(wallCheckStartPoint, Vector2.right * -1, wallCheckLength, ladderLayer);
            //Debug.DrawRay(transform.position, Vector2.right * -1, Color.green);
            //Debug.Log(hitInfo.collider);
            //Debug.Log(hitInfo.transform.gameObject.layer);
        }

        if (hitInfo.collider != null)
        {
            //Debug.Log(hitInfo);
            //if (hitInfo.transform.gameObject.layer == 11)
            if (hitInfo.collider.gameObject.layer == 11 && horizontalMove != 0f)
            {
                holdingWall = true;
                controller.extraJumpValue = controller.extraJump;
                animator.SetBool("HoldingWall", holdingWall);
            }

            else 
            {
                holdingWall = false;
                attackPoint.transform.position = new Vector2(attackPoint.transform.position.x * 1, attackPoint.transform.position.y);
                animator.SetBool("HoldingWall", holdingWall);
            }
        }

        else
        {
            holdingWall = false;
            animator.SetBool("HoldingWall", holdingWall);
        }

        //Debug.Log(hitInfo.collider.gameObject.layer);

    }

    public void Die()
    {
        //Debug.Log("Die");
        StartCoroutine(DieAnim());
    }

    IEnumerator DieAnim()
    {
        animator.SetTrigger("Hit");
        yield return new WaitForSeconds(.2f);
        playerCombat.playerHealth = 100f;
        animator.SetTrigger("Respawn");
        player.transform.position = playerSpawnPoint.transform.position;
    }
}
