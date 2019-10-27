using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;
    private void Awake() {
        Instance = this;
    }
    #endregion

    public Player player;
    public DancingGirl dancer;
    public EnemySpawner[] spawners;

    public void Lose()
    {
        Debug.Log("You lose.");
    }
}
