using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ropeMoveWithPlayer : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            var colRB = collision.GetComponent<Rigidbody2D>();
            rb.AddForce(colRB.velocity.normalized * 0.75f, ForceMode2D.Impulse);
        }
    }
}
