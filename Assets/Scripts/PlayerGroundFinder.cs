using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundFinder : MonoBehaviour
{
    private bool foundGround;
    [SerializeField] private PlayerControls player;

    private void Update()
    {
        foundGround = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            player.SetGrounded(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            player.SetGrounded(false);
        }
    }
}
