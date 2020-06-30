using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float hoverSpeed;
    public Rigidbody2D rb;
    private bool movingUp;
    private Vector3 originalPosition;
    private Vector3 currentPosition;

    private void Start()
    {
        originalPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        movingUp = true;
        hoverSpeed = Random.Range(.015f, .020f);
    }

    public void Update()
    {
        currentPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        //Debug.Log(movingUp);

        if (movingUp == true)
        {
            MoveUp();
            if (currentPosition.y >= originalPosition.y + 1)
            {
                movingUp = false;
                MoveDown();
            }
        }

        else
        {
            MoveDown();
            if (currentPosition.y <= originalPosition.y)
            {
                movingUp = true;
                MoveUp();
            }
        }
    }

    public void MoveUp()
    {
        //Debug.Log("MoveUp");
        //originalPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        rb.transform.position += rb.transform.up * hoverSpeed;
    }

    public void MoveDown()
    {
        //Debug.Log("MoveDown");
        //originalPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        rb.transform.position -= rb.transform.up * hoverSpeed;
        //Debug.Log(rb.transform.position);
    }

}
