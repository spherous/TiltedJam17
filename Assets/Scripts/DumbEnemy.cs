using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbEnemy : MonoBehaviour
{
    [Range(1f, 100f)]
    public float health = 10;

    [Range(0f, 50f)] [SerializeField] private float _speed = 5f;
    [Range(.1f, 10)] [SerializeField] private float _spawnTimerInSeconds = 2;
    [SerializeField] private GameObject _target;
    

    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;

    private Dictionary<string, Action> States;
    private string _currentState;
    private Vector2 moveVec;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        States = new Dictionary<string, Action>();
        States["DefaultState"] = DefaultState;
        States["Spawn"] = Spawn;
    }

    private void Start()
    {
        moveVec = Vector2.zero;
        EnterSpawnState();
    }

    void Update()
    {
        States[_currentState]();
    }

    void FixedUpdate()
    {
        if (moveVec != Vector2.zero)
            _rb.MovePosition((Vector2)(transform.position) + moveVec * _speed * Time.fixedDeltaTime);
    }

    //STATES

    //      DEFAULT
    void EnterDefaultState()
    {
        _currentState = "DefaultState";
        DefaultState();
    }

    // Default state is to chase the player
    void DefaultState()
    {
        SetMoveDir();
    }

    void ExitDefaultState()
    {
        
    }

    private void EnterSpawnState()
    {
        // Make Invisible
        var color = _spriteRenderer.color;
        color.a = 0;
        _spriteRenderer.color = color;

        _currentState = "Spawn";
        Spawn();
    }

    private void ExitSpawnState()
    {
        EnterDefaultState();
    }

    //FUNCTIONS

    void ZeroMoveDir()
    {
        moveVec = Vector2.zero;
    }

    void SetMoveDir()
    {
        moveVec = (Vector2)(_target.transform.position - transform.position).normalized;
    }

    // The enemy will fade in over time
    private float _spawnCounter = 0;
    private void Spawn()
    {
        _spawnCounter += Time.deltaTime/_spawnTimerInSeconds;
        var color = _spriteRenderer.color;

        color.a = Mathf.Lerp(0, 1, _spawnCounter);

        _spriteRenderer.color = color;

        if(_spriteRenderer.color.a >= 1f)
        {
            EnterDefaultState();
        }
    }
}
