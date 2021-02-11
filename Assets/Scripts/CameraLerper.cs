using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerper : MonoBehaviour
{
    [SerializeField] private GameObject trackingTarget;
    private ScreenData currentScreenData;
    [SerializeField] private float lerpDistance;
    [SerializeField] private bool timeDriven;

    private void Start()
    {
        currentScreenData = trackingTarget.GetComponent<ScreenData>();
    }

    public void SetMyScreen(GameObject currentScreen)
    {
        trackingTarget = currentScreen;
        currentScreenData = trackingTarget.GetComponent<ScreenData>();
    }
    void Update()
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(trackingTarget.transform.position.x, trackingTarget.transform.position.y, -10), lerpDistance * (timeDriven ? Time.deltaTime : 1));
        if (currentScreenData.halfSizeScreen)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 2.5f, lerpDistance * (timeDriven ? Time.deltaTime : 1));
        }
        else if (currentScreenData.doubleSizeScreen)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 10f, lerpDistance * (timeDriven ? Time.deltaTime : 1));
        }
        else
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 5f, lerpDistance * (timeDriven ? Time.deltaTime : 1));
        }
    }
}
