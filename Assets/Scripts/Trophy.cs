using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trophy : MonoBehaviour
{
    [SerializeField] private GameObject reward = null;

    void OnTriggerEnter2D(Collider2D collision)
    {
        reward.SetActive(true);
        Destroy(gameObject);
    }
}
