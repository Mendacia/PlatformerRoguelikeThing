using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectableKiller : MonoBehaviour
{
    private ParticleSystem myEmitter;
    [System.NonSerialized] public bool collected = false;

    void Awake()
    {
        myEmitter = gameObject.GetComponent<ParticleSystem>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            myEmitter.emissionRate = 0;
            collected = true;
        }
    }
}
