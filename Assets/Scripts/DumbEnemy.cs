using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbEnemy : MonoBehaviour
{
    [Range(1f, 100f)]
    public float health = 10;

    [Range(0f, 50f)]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private GameObject _target;
    

    private Rigidbody2D _rb;

    private Dictionary<string, Action> States;
    private string _currentState;
    private Vector2 moveVec;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        States = new Dictionary<string, Action>();
        _currentState = "DefaultState";
        States["DefaultState"] = DefaultState;
    }

    private void Start()
    {
        moveVec = Vector2.zero;
    }

    void Update()
    {
        States[_currentState]();
    }

    void FixedUpdate()
    {
        if (moveVec != Vector2.zero)
            GetComponent<Rigidbody2D>().MovePosition((Vector2)(transform.position) + moveVec * _speed * Time.fixedDeltaTime);
    }

    //STATES

    //      DEFAULT
    void EnterDefaultState()
    {
        _currentState = "DefaultState";
        DefaultState();
    }

    void DefaultState()
    {
        SetMoveDir();
    }

    void ExitDefaultState()
    {
        
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
}
