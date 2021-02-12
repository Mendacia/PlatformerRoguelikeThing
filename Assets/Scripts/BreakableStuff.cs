using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableStuff : MonoBehaviour
{
    [SerializeField] private bool isFrail = false;
    [SerializeField] private int requiredHits = 1;
    private Vector3 initialLocation;
    [SerializeField] private float maxShakeDistance;
    [SerializeField] private GameObject crumbleParticles = null;
    [SerializeField] private Transform approximateCenterpoint = null;
    private float shakeDuration = 0;

    void Awake()
    {
        initialLocation = gameObject.transform.position;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isFrail && collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            gameObject.transform.localPosition = initialLocation + new Vector3(Random.Range(-maxShakeDistance / 2, maxShakeDistance / 2), Random.Range(-maxShakeDistance / 2, maxShakeDistance / 2), 0);
            shakeDuration -= Time.deltaTime;
        }
        else
        {
            gameObject.transform.localPosition = initialLocation;
        }
    }

    public void BreakMe()
    {
        if (requiredHits > 1)
        {
            requiredHits--;
            shakeDuration += 0.3f;
        }
        else
        {
            Instantiate(crumbleParticles, approximateCenterpoint.position, Quaternion.identity);
            Destroy(gameObject);
            Debug.Log("I fucking died");
        }
    }
}
