using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSwing : MonoBehaviour
{
    private float yIntent;
    private float xIntent;
    private Vector2 wantedDirection;
    private Rigidbody2D myRigidbody;
    private bool isSwinging = false;

    [SerializeField] private float dashForce = 50;

    void Start()
    {
        myRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        xIntent = 0;
        yIntent = 0;

        if (Input.GetKey(KeyCode.W))
        {
            yIntent += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            yIntent -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            xIntent += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            xIntent -= 1;
        }

        wantedDirection = new Vector2(xIntent, yIntent);

        //taking inputs
        if (Input.GetKeyDown(KeyCode.LeftShift) && isSwinging == false)
        {
            isSwinging = true;
        }
    }

    void FixedUpdate()
    {
        if (isSwinging)
        {
            Debug.Log(xIntent + ", " + yIntent);
            myRigidbody.velocity = Vector2.zero;
            myRigidbody.velocity = (wantedDirection * dashForce);
            isSwinging = false;
            Debug.Log("Swung");
        }
    }
}
