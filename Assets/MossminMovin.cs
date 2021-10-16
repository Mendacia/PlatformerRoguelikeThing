using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossminMovin : MonoBehaviour
{
    public enum aIState
    {
        IDLE,
        RUN,
        JUMP,
        JUMPRUN
    }
    public aIState currentState;

    [SerializeField] private Transform thresholdWest = null, thresholdEast = null;
    [SerializeField] private Animator anim = null;
    [SerializeField] private Rigidbody2D rb = null;

    private bool moveAllowed = false;
    private int moveDir = 1; //1 Right, 0 Left
    private bool jumpAllowed = false;
    private float wantedSpeed;

    private float xIntent;
    private float yIntent;
    private Vector2 wantedDirection;

    private void Start()
    {
        StartCoroutine(CommitAction(Random.Range(0, 10)));
    }

    IEnumerator CommitAction(float waitTimer)
    {
        yield return new WaitForSeconds(waitTimer);
        var ts = Random.Range(0, 4);
        switch (ts)
        {
            default:
                currentState = aIState.IDLE;
                Debug.Log("Mossmin Is IDLE");
                break;
            case 1:
                currentState = aIState.RUN;
                Debug.Log("Mossmin Is RUNNING");
                break;
            case 2:
                currentState = aIState.JUMPRUN;
                Debug.Log("Mossmin Is JUMPING (Cancelled for now, so just Shmoving)");
                break;
            case 3:
                currentState = aIState.JUMPRUN;
                Debug.Log("Mossmin Is SHMOVING!!!!!!");
                break;
        }
        moveAllowed = true;
        jumpAllowed = true;
        wantedSpeed = Random.Range(2f, 5f);
        moveDir = Random.Range(0, 2);
        yield return new WaitForSeconds(Random.Range(0, 3));
        moveAllowed = false;
        StartCoroutine(CommitAction(Random.Range(0, 3)));
    }

    private void FixedUpdate()
    {
        xIntent = rb.velocity.x;
        yIntent = rb.velocity.y;
        switch (currentState)
        {
            default:
                xIntent = 0;
                break;
            case aIState.RUN:
                RunFixedUpdate();
                break;
            //case aIState.JUMP:
                //JumpFixedUpdate();
                //break;
            case aIState.JUMPRUN:
                RunFixedUpdate();
                JumpFixedUpdate();
                break;
        }

        if (xIntent > wantedSpeed)
        {
            xIntent = 5;
        }
        if (xIntent < -wantedSpeed)
        {
            xIntent = -5;
        }

        wantedDirection = new Vector2(xIntent, yIntent);
        rb.velocity = wantedDirection;
    }

    private void RunFixedUpdate()
    {
        if (moveAllowed)
        {
            if (moveDir == 1)//Right
            {
                if (rb.position.x > thresholdEast.position.x)
                {
                    xIntent = 0;
                    moveAllowed = false;
                }
                else
                {
                    xIntent += 0.5f;
                }
            }
            else//Left
            {
                if (rb.position.x < thresholdWest.position.x)
                {
                    xIntent = 0;
                    moveAllowed = false;
                }
                else
                {
                    xIntent -= 0.5f;
                }
            }
        }
    }

    private void JumpFixedUpdate()
    {
        if (jumpAllowed)
        {
            yIntent = 5;
            jumpAllowed = false;
        }
    }
}
