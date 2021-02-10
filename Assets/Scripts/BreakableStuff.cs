using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableStuff : MonoBehaviour
{
    [SerializeField] private bool isFrail = false;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isFrail && collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    public void BreakMe()
    {
         Destroy(gameObject);
         Debug.Log("I fucking died");
    }
}
