using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TopDownMove : MonoBehaviour
{
    /*Testing collisions here with animations : this movement behaves as I want, move and slide style. */

    /*
    
    STATES          BEHAVIORS
    DefaultState    SetMoveDirection
    DashState       DashState
    
     */
    
    Dictionary<string,Action> States; 
    public float speed;
    Vector2 moveVec;
    string state;


    /*
    animation state
    pickup item
    item base class
    die

    items : 
        lantern fuel
        pumpkins
        etc.

     */


    //monobehavior functions

    void Awake()
    {
        States = new Dictionary<string,Action>(); 
        state= "DefaultState";
        States["DefaultState"] = DefaultState;
    }
    // Start is called before the first frame update
    void Start()
    {
        moveVec = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        States[state]();
    }

    void FixedUpdate()
    {
        if(moveVec!= Vector2.zero)
            GetComponent<Rigidbody2D>().MovePosition( (Vector2)(transform.position) + moveVec*speed*Time.fixedDeltaTime);
    }

    //STATES

    //      DEFAULT
    void EnterDefaultState()
    {
        state = "DefaultState";
        DefaultState();
    }

    void DefaultState()
    {
        SetMoveDir();    
    }

    void ExitDefaultState()
    {
        ZeroMoveDir();
    }

    //FUNCTIONS

    void ZeroMoveDir()
    {
        moveVec= Vector2.zero;
    }

    void SetMoveDir()
    {
        moveVec = ((Input.GetKey(KeyCode.A) ? Vector2.left : Vector2.zero) +
         (Input.GetKey(KeyCode.D) ? Vector2.right : Vector2.zero) + 
         (Input.GetKey(KeyCode.W) ? Vector2.up : Vector2.zero) +
         (Input.GetKey(KeyCode.S) ? Vector2.down : Vector2.zero)).normalized;
    }


}