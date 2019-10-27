using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float radius;
    [Range(0f,1f)]
    public float probability;
    public GameObject[] enemies;
    float cooldown = 1;
    float lastSpawnTime = 0;
    // Update is called once per frame

    void FixedUpdate()
    {
        if(Time.time - lastSpawnTime > cooldown)
        {
            if(Random.Range(0f,1f) < probability)
            {
                float angle = Random.Range(0,2*Mathf.PI);
                float dist = Random.Range(0f, radius);
                int enem = Random.Range(0,enemies.Length);
                GameObject.Instantiate(enemies[enem], new Vector3(Mathf.Sin(angle)*dist, Mathf.Cos(angle) * dist, 0f) ,Quaternion.identity);
                lastSpawnTime = Time.time;
            }
        }
    }
}
