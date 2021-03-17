using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallFinder : MonoBehaviour
{
    [SerializeField] private PlayerControls player = null;
    [SerializeField] private LayerMask wallFinderMask = 0;

    void Update()
    {
        var leftWallDirectionFinderRaycast = Physics2D.Raycast(transform.position, Vector2.left, 0.25f, wallFinderMask);
        var rightWallDirectionFinderRaycast = Physics2D.Raycast(transform.position, Vector2.right, 0.25f, wallFinderMask);

        Debug.DrawRay(transform.position, Vector2.left * 0.25f, Color.green);
        Debug.DrawRay(transform.position, Vector2.right * 0.25f, Color.red);


        if (leftWallDirectionFinderRaycast.collider == null)
        {
            //Beep boop I'm useless but necessary
        }
        else if(leftWallDirectionFinderRaycast.collider.tag == "Ground")
        {
            player.wallOnRight = false;
        }

        if (rightWallDirectionFinderRaycast.collider == null)
        {
            //Beep boop I'm useless but necessary
        }
        else if (rightWallDirectionFinderRaycast.collider.tag == "Ground")
        {
            player.wallOnRight = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            player.canWallJump = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            player.canWallJump = false;
        }
    }
}