using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : ItemBase
{
    public Vector2 offset;
    bool pickedUp = false;
    public int amount;

    public override bool Pickup(GameObject player)
    {
        if(!pickedUp)
        {
            pickedUp = true;
            transform.parent =player.transform;
            transform.localPosition = offset;
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

    // Start is called before the first frame update
    void Start()
    {
    }

}
