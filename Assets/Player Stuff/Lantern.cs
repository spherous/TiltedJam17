using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : ItemBase
{
    public float offset;
    bool pickedUp = false;
    public int amount;
    int number;

    public override bool Pickup(GameObject player , int number)
    {
        if(!pickedUp)
        {
            pickedUp = true;
            transform.parent =player.transform;


            this.number = number + 1;
            float current = (transform.parent.gameObject.GetComponent<TopDownMove>().currentItems+1);
            transform.localPosition = new Vector2 ( -.5f + this.number/current/2f, offset ) ;
            gameObject.layer = default;
            GetComponent<BoxCollider2D>().enabled = false;
            return true;
        }
        return false;
    }

    public override bool Use(GameObject target)
    {
        DancingGirl d;
        if( target.TryGetComponent(out d) )
        {
            transform.parent = null;
            d.RefuelLight(amount);
            Destroy(gameObject); 
            return true;    
        }
        else
            return false;
    }

    void LateUpdate()
    {
        if(transform.parent != null)
        {
            float current = (transform.parent.gameObject.GetComponent<TopDownMove>().currentItems);
            transform.localPosition = new Vector2 ( (-1 +1/(current))/2f + ( (this.number -1)/( current) ), offset ) ;

            transform.localScale = Vector3.one / current;
        }
    }

}
