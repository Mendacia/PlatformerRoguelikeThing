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
    private float defaultGravityScale;
    private Transform spinner;
    private Animator weaponFX;

    [SerializeField] private float dashForce = 50;

    void Awake()
    {
        myRigidbody = gameObject.GetComponent<Rigidbody2D>();
        defaultGravityScale = myRigidbody.gravityScale;
        spinner = transform.Find("SwingSpinner");
        weaponFX = GameObject.Find("Weapon").GetComponent<Animator>();
        weaponFX.GetComponent<Collider2D>().enabled = false;
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
            StartCoroutine(AttackRunning());
            myRigidbody.velocity = Vector2.zero;
            myRigidbody.velocity = (wantedDirection * dashForce);
            rotateTheSpinnerAndRunTheAnimation();
            isSwinging = false;
        }
    }

    void rotateTheSpinnerAndRunTheAnimation()
    {
        var myAngle = 0f;
        switch (xIntent)
        {
            case 1:
                {
                    myAngle = 45 * yIntent;
                    break;
                }
            case 0:
                {
                    myAngle = 90 * yIntent;
                    break;
                }
            case -1:
                {
                    if (yIntent != 0)
                    {
                        myAngle = 135 * yIntent;
                    }
                    else
                    {
                        myAngle = 180;
                    }
                    break;
                }
        }
        spinner.rotation = Quaternion.AngleAxis(myAngle, Vector3.forward);
        weaponFX.SetTrigger("dashAttack");
    }

    IEnumerator AttackRunning()
    {
        myRigidbody.gravityScale = 0;
        myRigidbody.drag = 10f;
        weaponFX.GetComponent<Collider2D>().enabled = true;
        yield return new WaitForSecondsRealtime(1 / 6f);
        weaponFX.GetComponent<Collider2D>().enabled = false;
        myRigidbody.drag = 0;
        myRigidbody.gravityScale = defaultGravityScale;
    }
}
