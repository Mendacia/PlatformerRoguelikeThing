using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenData : MonoBehaviour
{
    //This holds public bools for CameraLerper to interperate

    public bool halfSizeScreen = false;
    public bool doubleSizeScreen = false;

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
