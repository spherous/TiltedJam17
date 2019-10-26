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

    public Vector2 interactionBoxOffset;
    public Vector2 interactionBoxSize;
    Collider2D[] colliders;
    int collidersSize;
    LayerMask itemsLayers;
    GameObject item;

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
        colliders = new Collider2D[8];
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
        if(Input.GetKeyDown(KeyCode.E))
            TryInteract();
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

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position + (Vector3)interactionBoxOffset , interactionBoxSize);
    }

    void TryInteract()
    {
        if(item == null) //are we already holding something? if not...
        {
            if( CheckBoxOverlap((Vector2)transform.position + interactionBoxOffset , interactionBoxSize , 0f, itemsLayers )) //try picking something up
            {
                ItemBase ib ;
                for(int i = 0 ; i < collidersSize;i++)
                {
                    colliders[i].gameObject.TryGetComponent(out ib);
                    if(ib != null)
                    {
                        if (ib.Pickup(gameObject))
                        {
                            item = colliders[i].gameObject;
                            break;
                        }
                    }
                }
            }
        }
        else //we have something, try using our item on it. 
        {
            collidersSize = Physics2D.OverlapBoxNonAlloc( (Vector2) transform.position + interactionBoxOffset , interactionBoxSize , 0f , colliders );//dont care about layers
            for( int i = 0 ; i < collidersSize ; i++)
            {
                if( item.GetComponent<ItemBase>().Use(colliders[i].gameObject) ) //when "Use" returns true, quit out;
                {
                    item = null;
                    return;
                }  
            }
        }
    }

    bool CheckBoxOverlap( Vector2 center, Vector2 size, float rotation, LayerMask checkLayers)
    {
        ContactFilter2D c = new ContactFilter2D();
        c.layerMask = checkLayers;
        c.useLayerMask = true;
        collidersSize = Physics2D.OverlapBox(center,size, rotation,c, colliders) ;
        return collidersSize > 0;
    }




}