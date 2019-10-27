using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DumbEnemy))]
public class EnemyCollisions : MonoBehaviour
{
    private DumbEnemy _dumbEnemy;

    private void Awake()
    {
        _dumbEnemy = GetComponent<DumbEnemy>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<TopDownMove>())
        {
            // Collided with Player
            Debug.Log("Hit the Player");
        }

        if (collision.gameObject.GetComponent<DumbEnemy>())
        {
            if (_dumbEnemy.CurrentState == "Scared" &&
            collision.gameObject.GetComponent<DumbEnemy>().CurrentState != "Scared")
            {
                collision.gameObject.GetComponent<DumbEnemy>().EnterScaredState();
                //Debug.Log("Scare spread too another ghoul");
            }
        }
    }
}
