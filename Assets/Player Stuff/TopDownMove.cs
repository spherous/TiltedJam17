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
    public LayerMask itemsLayers;
    GameObject[] items;
    public int maxItems;
    public int currentItems = 0 ;

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
        items = new GameObject[maxItems];
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
        if(!GameManager.Instance.gamePlaying)
            return;

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
        SetInteractionBoxOffset();

        //if(Input.GetKeyDown(KeyCode.E))
        TryInteract();  
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
        if(currentItems < maxItems) //are we already at capacity ?...
        {
            if( CheckBoxOverlap((Vector2)transform.position + interactionBoxOffset , interactionBoxSize , 0f, itemsLayers )) //try picking something up
            {
                ItemBase ib ;
                for(int i = 0 ; i < collidersSize;i++)
                {
                    colliders[i].gameObject.TryGetComponent(out ib);
                    if(ib != null)
                    {
                        if (ib.Pickup(gameObject,currentItems))
                        {
                            AddItem( colliders[i].gameObject );
                            break;
                        }
                    }
                }
            }
        }
        
            collidersSize = Physics2D.OverlapBoxNonAlloc( (Vector2) transform.position + interactionBoxOffset , interactionBoxSize , 0f , colliders );//dont care about layers
            for( int i = 0 ; i < collidersSize ; i++)
            {
                if(currentItems > 0) //we have something, try using our item on it. 
                if( items[currentItems-1].GetComponent<ItemBase>().Use(colliders[i].gameObject) ) //when "Use" returns true, quit out;
                {
                    PopItem();
                    return;
                }  
                if( colliders[i].gameObject.tag == "Curse")
                {
                    colliders[i].gameObject.GetComponent<CurseShrine>().Curse();
                }
            }
        
    }

    void AddItem(GameObject g) //must be called from context where currentItems< maxItems is guaranteed
    {
        items[currentItems] = g;
        currentItems ++;
    }

    void PopItem() // must be called from context where currentItems > 0 is guaranteed
    {
        items[currentItems-1]= null;
        currentItems--;
    }

Vector2[] cardinalDirections= {
            new Vector2 (-1,0),
            new Vector2 (0,1),
            new Vector2 (1,0),
            new Vector2 (0,-1)
        };

    void SetInteractionBoxOffset()
    {
        
        float[] dots = new float[4];
        for(int i = 0 ; i < 4; i++)
        {
            dots[i] = Vector2.Dot( cardinalDirections[i] , moveVec.normalized );
        }
        int maxi = 0;
        for(int i = 0 ; i < 4; i++)
        {
            if(dots[i] > dots[maxi])
                maxi = i;
        }
        if(dots[maxi] > .0001f)
            interactionBoxOffset = cardinalDirections[maxi];
        else
            interactionBoxOffset = Vector2.zero;
    }

    bool CheckBoxOverlap( Vector2 center, Vector2 size, float rotation, LayerMask checkLayers)
    {
        ContactFilter2D c = new ContactFilter2D();
        c.layerMask = checkLayers;
        c.useLayerMask = true;
        collidersSize = Physics2D.OverlapBox(center,size, rotation,c, colliders) ;
        return collidersSize > 0;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Fuel"))
            TryInteract();
    }



}