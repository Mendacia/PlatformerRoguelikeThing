using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerper : MonoBehaviour
{
    [SerializeField] private GameObject trackingTarget;
    [SerializeField] private float lerpDistance;
    [SerializeField] private bool timeDriven;

    public void SetMyScreen(GameObject currentScreen)
    {
        trackingTarget = currentScreen;
    }
    void Update()
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(trackingTarget.transform.position.x, trackingTarget.transform.position.y, -10), lerpDistance * (timeDriven ? Time.deltaTime : 1));
    }
}
