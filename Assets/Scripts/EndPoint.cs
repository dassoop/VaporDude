using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public PauseMenu pauseMenu;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            pauseMenu.GameOver();
        }
    }
}
