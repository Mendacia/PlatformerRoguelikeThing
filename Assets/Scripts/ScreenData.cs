using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenData : MonoBehaviour
{
    //This holds public bools for CameraLerper to interperate

    public bool halfSizeScreen = false;
    public bool doubleSizeScreen = false;
    public bool scrollingScreenWE = false;
    public bool scrollingScreenNS = false;

    //Anchors for scrolling screens, will be null if the screen does not scroll
    [System.NonSerialized] public GameObject westmostAnchor = null;
    [System.NonSerialized] public GameObject eastmostAnchor = null;
    [System.NonSerialized] public GameObject northmostAnchor = null;
    [System.NonSerialized] public GameObject southmostAnchor = null;

    void Start()
    {
        if (scrollingScreenWE)
        {
            westmostAnchor = transform.Find("Westmost Anchor").gameObject;
            eastmostAnchor = transform.Find("Eastmost Anchor").gameObject;
        }

        if (scrollingScreenNS)
        {
            northmostAnchor = transform.Find("Northmost Anchor").gameObject;
            southmostAnchor = transform.Find("Southmost Anchor").gameObject;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerControls>().addScreenToTheList(gameObject, true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerControls>().addScreenToTheList(gameObject, false);
        }
    }
}
