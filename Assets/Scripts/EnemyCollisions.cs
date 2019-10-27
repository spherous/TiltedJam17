using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisions : MonoBehaviour
{
    private EnemyBase _dumbEnemy;

    private void Awake()
    {
        _dumbEnemy = GetComponent<EnemyBase>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<TopDownMove>())
        {
            // Collided with Player
            //GameManager.Instance.Lose();
        }

        if (collision.gameObject.GetComponent<EnemyBase>())
        {
            if (_dumbEnemy.CurrentState == "Scared" &&
            collision.gameObject.GetComponent<EnemyBase>().CurrentState != "Scared")
            {
                collision.gameObject.GetComponent<EnemyBase>().EnterScaredState(4f);
                //Debug.Log("Scare spread too another ghoul");
            }
        }
    }
}
