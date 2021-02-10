using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordItemCollect : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerWeaponSwing>().enabled = true;
            Destroy(gameObject);
        }
    }
}
