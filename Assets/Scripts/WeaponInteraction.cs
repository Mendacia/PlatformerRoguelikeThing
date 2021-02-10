using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInteraction : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BreakableStuff>() != null)
        {
            collision.gameObject.GetComponent<BreakableStuff>().BreakMe();
        }
    }
}
