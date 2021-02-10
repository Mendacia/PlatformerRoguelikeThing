using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private enum playerState
    {
        GROUNDED,
        AIRBORNE
    }
    private playerState currentState = playerState.AIRBORNE;

    private Rigidbody2D myRigidbody;
    private float xIntent;
    private float yIntent;
    private Vector2 wantedDirection;
    private bool jumpBool = false;
    private bool hasDoubleJumped = false;
    private List<GameObject> screens = new List<GameObject>();

    [SerializeField] private CameraLerper screenTransitionScript;

    [Header("Movement Values")]
    [SerializeField] private float playerWalkMaxSpeed = 5;
    [SerializeField] private float playerWalkAcceleration = 0.5f;
    [SerializeField] private float playerJumpHeight = 10;
    [SerializeField] private float playerJumpLRAcceleration = 0.3f;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Screen")
        {
            screens.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Screen")
        {
            screens.Remove(other.gameObject);
        }
    }

    void Update()
    {
        xIntent = myRigidbody.velocity.x;
        yIntent = myRigidbody.velocity.y;

        switch (currentState)
        {
            case playerState.GROUNDED:
                {
                    GroundedUpdate();
                    break;
                }
            case playerState.AIRBORNE:
                {
                    AirborneUpdate();
                    break;
                }
        }

        //Player Speed Cap

        if (screens.Count == 2)
        {
            screenTransitionScript.SetMyScreen(screens[0]);
        }
    }

    public void SetGrounded(bool shouldBeGrounded)
    {
        if (shouldBeGrounded)
        {
            currentState = playerState.GROUNDED;
            hasDoubleJumped = false;
        }
        else
        {
            currentState = playerState.AIRBORNE;
        }
    }

    private void GroundedUpdate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            xIntent += playerWalkAcceleration;
        }
        if (Input.GetKey(KeyCode.A))
        {
            xIntent -= playerWalkAcceleration;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBool = true;
        }

        if (xIntent > playerWalkMaxSpeed)
        {
            xIntent = playerWalkMaxSpeed;
        }
        if (xIntent < -playerWalkMaxSpeed)
        {
            xIntent = -playerWalkMaxSpeed;
        }

        if (!(Input.GetKey(KeyCode.D)) && !(Input.GetKey(KeyCode.A)))
        {
            xIntent = 0;
        }
    }

    private void AirborneUpdate()
    {
        if (Input.GetKey(KeyCode.D) && !(xIntent > playerWalkMaxSpeed))
        {
            xIntent += playerJumpLRAcceleration;
        }
        if (Input.GetKey(KeyCode.A) && !(xIntent < -playerWalkMaxSpeed))
        {
            xIntent -= playerJumpLRAcceleration;
        }
        if (Input.GetKeyDown(KeyCode.Space) && hasDoubleJumped == false)
        {
            jumpBool = true;
            hasDoubleJumped = true;
        }
    }

    private void FixedUpdate()
    {
        if (jumpBool)
        {
            yIntent = playerJumpHeight;
            jumpBool = false;
        }
        wantedDirection = new Vector2(xIntent, yIntent);
        myRigidbody.velocity = wantedDirection;
    }
}
