using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerper : MonoBehaviour
{
    [SerializeField] private GameObject trackingTarget;
    private ScreenData currentScreenData;
    [SerializeField] private float lerpDistance;
    [SerializeField] private bool timeDriven;
    [SerializeField] private Transform player = null;

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
        if (!currentScreenData.scrollingScreenWE && !currentScreenData.scrollingScreenNS)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(trackingTarget.transform.position.x, trackingTarget.transform.position.y, -10), lerpDistance * (timeDriven ? Time.deltaTime : 1));
            if (currentScreenData.halfSizeScreen) //Used in a small screen
            {
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 2.5f, lerpDistance * (timeDriven ? Time.deltaTime : 1));
            }
            else if (currentScreenData.doubleSizeScreen) //Used in a large screen
            {
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 10f, lerpDistance * (timeDriven ? Time.deltaTime : 1));
            }
            else //Used in a regular screen
            {
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 5f, lerpDistance * (timeDriven ? Time.deltaTime : 1));
            }
        }
        else if (currentScreenData.scrollingScreenWE) //Used if the screen should scroll Left and Right
        {
            //Sets the camera to normal size
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 5f, lerpDistance * (timeDriven ? Time.deltaTime : 1));

            var transformLimitWest = currentScreenData.westmostAnchor.transform.position.x;
            var transformLimitEast = currentScreenData.eastmostAnchor.transform.position.x;

            if (player.position.x > transformLimitWest && player.position.x < transformLimitEast)
            {
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(player.position.x, trackingTarget.transform.position.y, -10), lerpDistance * (timeDriven ? Time.deltaTime : 1));
            }
            else if (player.position.x < transformLimitWest)
            {
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(transformLimitWest, trackingTarget.transform.position.y, -10), lerpDistance * (timeDriven ? Time.deltaTime : 1));
            }
            else if (player.position.x > transformLimitEast)
            {
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(transformLimitEast, trackingTarget.transform.position.y, -10), lerpDistance * (timeDriven ? Time.deltaTime : 1));
            }

        }
    }
}
