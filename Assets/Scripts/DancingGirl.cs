using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DancingGirl : MonoBehaviour
{
    GameManager gm => GameManager.Instance;

    public Light2D light;

    public float currentFuel;
    public float maxFuel;
    public float fuelConsumptionRatePerFrame;

    private void Update()
    {
        // Get the distance to the player
        float distance = Vector2.Distance(transform.position, gm.player.transform.position);
        // Move
        // Drain Fuel
        DrainFuel();
    }

    public void DrainFuel()
    {
        currentFuel = currentFuel - fuelConsumptionRatePerFrame <= 0 ? 0 : currentFuel - fuelConsumptionRatePerFrame;

        if(currentFuel == 0)
            gm.Lose();
    }

    public void RefuelLight(float amountOfFuel)
    {
        currentFuel = currentFuel + amountOfFuel < maxFuel ? currentFuel + amountOfFuel : maxFuel;
    }
}
