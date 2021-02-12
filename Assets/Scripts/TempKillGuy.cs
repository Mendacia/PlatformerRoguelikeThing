using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempKillGuy : MonoBehaviour
{
    [SerializeField]private Animator myAnimator;
    private bool isAlive = true;
    [System.NonSerialized] public bool hasDiedBefore = false;

    void Start()
    {
        myAnimator = gameObject.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Enemyreforming") && isAlive == false)
        {
            Debug.Log("I'm alive again");
            isAlive = true;
        } 
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Weapon" && isAlive)
        {
            Debug.Log("I have been slain!");
            hasDiedBefore = true;
            myAnimator.SetTrigger("Died");
            isAlive = false;
            GameObject.Find("Player (Root)").GetComponent<PlayerControls>().canSwingWeapon = true;
        }
    }
}
