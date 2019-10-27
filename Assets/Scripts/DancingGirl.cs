using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Cinemachine;

public class DancingGirl : MonoBehaviour
{
    GameManager gm => GameManager.Instance;

    public Light2D girlLight;
    public float maxLightOuterRadius;
    public float maxLightInnerRadius;

    public float stoppingDistanceOffset;

    public float currentFuel;
    public float maxFuel;
    public float fuelConsumptionRatePerFrame;
    
    public float maxSpeed;
    public CinemachineDollyCart dolly;

    public enum GirlStates {stopped, stopping, moving, accelerating}
    public GirlStates state;

    float acceleratingTimer = 0;

    public SpriteRenderer spriteRenderer; 
    
    private void Start() {
        StartCoroutine(AnimateGirl());
    }

    private void Update()
    {
        if(!gm.gamePlaying)
            return;
            
        // Get the distance to the player
        float distance = Vector2.Distance(transform.position, gm.player.transform.position);
        
        switch(state)
        {
            case GirlStates.stopped:
                if(distance <= girlLight.pointLightOuterRadius)
                    state = GirlStates.accelerating;
                break;
            case GirlStates.stopping:
                if(dolly.m_Speed == 0)
                {
                    acceleratingTimer = 0;
                    state = GirlStates.stopped;
                }
                dolly.m_Speed = Mathf.Lerp(0, dolly.m_Speed, acceleratingTimer);
                acceleratingTimer += Time.deltaTime;
                break;
            case GirlStates.accelerating:
                if(dolly.m_Speed == maxSpeed)
                {
                    acceleratingTimer = 0;
                    state = GirlStates.moving;
                }
                dolly.m_Speed = Mathf.Lerp(maxSpeed, dolly.m_Speed, acceleratingTimer);
                acceleratingTimer += Time.deltaTime;
                break;
            case GirlStates.moving:
                if(distance > girlLight.pointLightOuterRadius * stoppingDistanceOffset)
                    state = GirlStates.stopping;
                break;
        }

        // Drain Fuel
        DrainFuel();
    }

    public void DrainFuel()
    {
        currentFuel = currentFuel - fuelConsumptionRatePerFrame <= 0 ? 0 : currentFuel - fuelConsumptionRatePerFrame;

        girlLight.pointLightOuterRadius = maxLightOuterRadius * (currentFuel/maxFuel);
        girlLight.pointLightInnerRadius = maxLightInnerRadius * (currentFuel/maxFuel);

        if(currentFuel <= .2f)
            gm.Lose(); 
    }

    public void RefuelLight(float amountOfFuel)
    {
        currentFuel = currentFuel + amountOfFuel < maxFuel ? currentFuel + amountOfFuel : maxFuel;
    }

    public Sprite[] sprites;
    public float animationTime;

    IEnumerator AnimateGirl()
    {
        float t = 0;
        int index = 0;
        while(Application.isPlaying)
        {
            t += Time.deltaTime;
            if(t >= animationTime)
            {
                index++;
                if(index == sprites.Length - 1)
                    index = 0;
                spriteRenderer.sprite = sprites[index];
                t = 0;
            }
            yield return null;
        }
    }

    public InkScript ink;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("DialogueTrigger"))
        {
            dolly.m_Speed = 0;
            state = GirlStates.stopped;
            gm.PauseGame();
            ink.NewDialogue();
        }
        else if(other.gameObject.CompareTag("EndGame"))
        {
            gm.EndGame();
        }
    }
}
