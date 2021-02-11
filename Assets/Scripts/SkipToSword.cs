using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipToSword : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform sword;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            player.position = sword.position;
        }
    }
}
