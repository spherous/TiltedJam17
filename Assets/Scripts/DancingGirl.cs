using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancingGirl : MonoBehaviour
{
    GameManager gm => GameManager.Instance;

    private void Update()
    {
        // Get the distance to the player
        float distance = Vector2.Distance(transform.position, gm.player.transform.position);
        // Move
    }
}
