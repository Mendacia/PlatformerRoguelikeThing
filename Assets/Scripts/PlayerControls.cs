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
    private List<GameObject> screens = new List<GameObject>();

    //Weapon Handling
    [System.NonSerialized] public bool canSwingWeapon = true;

    [SerializeField] private CameraLerper screenTransitionScript;

    //Wall Jump Handling
    public bool wallJumpUnlocked = false;
    [System.NonSerialized] public bool canWallJump = false;
    [System.NonSerialized] public bool wallOnRight = true;
    private bool wallJumpBool = false;

    //Double Jump Handling
    public bool doubleJumpUnlocked = false;
    private bool doubleJumpBool = false;
    private bool canDoubleJump = true;

    [Header("Movement Values")]
    [SerializeField] private float playerWalkMaxSpeed = 5;
    [SerializeField] private float playerWalkAcceleration = 0.5f;
    [SerializeField] private float playerJumpHeight = 10;
    [SerializeField] private float playerJumpLRAcceleration = 0.3f;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    public void addScreenToTheList(GameObject screen, bool toAdd)
    {
        if (toAdd)
        {
            screens.Add(screen);
        }
        else
        {
            screens.Remove(screen);
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

        if (screens.Count == 1)
        {
            screenTransitionScript.SetMyScreen(screens[0]);
        }
    }

    public void SetGrounded(bool shouldBeGrounded)
    {
        if (shouldBeGrounded)
        {
            currentState = playerState.GROUNDED;
        }
        else
        {
            currentState = playerState.AIRBORNE;
        }
    }

    private void GroundedUpdate()
    {
        canSwingWeapon = true;
        canDoubleJump = true;

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

        //Wall Jump Segment
        if (wallJumpUnlocked)
        {
            if(canWallJump)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    wallJumpBool = true;
                }
            }
        }

        if (doubleJumpUnlocked)
        {
            if (canDoubleJump && !canWallJump)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    doubleJumpBool = true;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (jumpBool)
        {
            yIntent = playerJumpHeight;
            jumpBool = false;
        }

        if (wallJumpBool)
        {
            yIntent = playerJumpHeight;
            if (wallOnRight)
            {
                xIntent = -playerWalkMaxSpeed;
            }
            else
            {
                xIntent = playerWalkMaxSpeed;
            }
            wallJumpBool = false;
        }

        if (doubleJumpBool)
        {
            yIntent = playerJumpHeight;
            doubleJumpBool = false;
            canDoubleJump = false;
        }
        wantedDirection = new Vector2(xIntent, yIntent);
        myRigidbody.velocity = wantedDirection;
    }
}
