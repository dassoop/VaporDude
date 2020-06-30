using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingEnemyAI : MonoBehaviour
{
    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float detectionRange = 10f;
    public LayerMask playerLayer;
    public Transform graphics;
    public bool isFacingRight = true;
    public Animator animator;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();


        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    private void FixedUpdate()
    {


        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }

        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        Collider2D detectedPlayer = Physics2D.OverlapCircle(transform.position, detectionRange, playerLayer);

        if (detectedPlayer != null)
        {
            rb.AddForce(force);
        }

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }


        //Flip Character Graphics
        if (rb.velocity.x >= 0 && !isFacingRight)
        {
            isFacingRight = true;
            animator.Play("Enemy_TurnRight");
            FlipRight();
        }

        else if (rb.velocity.x < 0 && isFacingRight)
        {
            isFacingRight = false;
            animator.Play("Enemy_TurnLeft");
            FlipLeft();
        }

        //Debug.Log("VELOCITY" + rb.velocity.x);
        //Debug.Log(isFacingRight);
    }

    void FlipRight()
    {
        transform.Rotate(0f, 180f, 0f);
    }

    void FlipLeft()
    {
        transform.Rotate(0f, 180f, 0f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
