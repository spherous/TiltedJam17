using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class PlayerChoiceText : MonoBehaviour, IPointerDownHandler
{
    public InkScript ink;

    public TextMeshProUGUI playerChoiceText;

    public Color32 orangeColor;

    public float flashDuration;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FlashWhite());
    }

    IEnumerator FlashWhite()
    {
        float t = 0;
        bool running = true;
        while(running)
        {
            if(playerChoiceText.color == Color.white)
            {
                StartCoroutine(FlashOrange());
                running = false;
            }
            playerChoiceText.color = Color.Lerp(orangeColor, Color.white, t/flashDuration);
            
            t += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FlashOrange()
    {
        float t = 0;
        bool running = true;
        while(running)
        {
            if(playerChoiceText.color == orangeColor)
            {
                StartCoroutine(FlashWhite());
                running = false;
            }
            playerChoiceText.color = Color.Lerp(Color.white, orangeColor, t/flashDuration);
            
            t += Time.deltaTime;
            yield return null;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ink.Next();
    }
}
