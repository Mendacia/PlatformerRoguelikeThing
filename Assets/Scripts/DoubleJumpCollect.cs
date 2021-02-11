using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpCollect : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerControls>().doubleJumpUnlocked = true;
            Destroy(gameObject);
        }
    }
}
