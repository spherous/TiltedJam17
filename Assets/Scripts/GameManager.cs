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
    public bool gamePlaying;// {get; private set;} = false;

    public void Lose()
    {
        gamePlaying = false;
        Debug.Log("You lose.");
    }

    public void StartGame()
    {
        gamePlaying = true;
    }
    public void PauseGame()
    {
        gamePlaying = false;
    }

    public void ApplyCurse(float howMuch)
    {
        foreach(EnemySpawner enemySpawner in spawners)
        {
            enemySpawner.probability *= howMuch;
        }
    }

    public void EndGame()
    {
        // End the game.
    }
}
