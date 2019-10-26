using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelFlask : MonoBehaviour
{
    GameManager gm => GameManager.Instance;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PickUpFuel();
        }
    }
    
    private void PickUpFuel()
    {
        gm.player.PickupFuel();
    }
}
