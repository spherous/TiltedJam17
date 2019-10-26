using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancingGirl : MonoBehaviour
{
    public GameObject player;
    private void Update()
    {
        // Get the distance to the player
        float distance = Vector2.Distance(transform.position, player.transform.position);
        // Move
    }
}
