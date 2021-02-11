using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private Sprite activeStateSprite = null;
    [SerializeField] private GameObject targetGameObject = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = activeStateSprite;
            targetGameObject.SetActive(true);
        }
    }
}
