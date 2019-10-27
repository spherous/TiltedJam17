using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AreaBoundEnemy : EnemyBase
{
    [Range(1f, 100f)]
    public float health = 10;
    GameManager gm => GameManager.Instance;

    //[Range(0f, 50f)] [SerializeField] private float _speed = 5f;
    [Range(.1f, 10)] [SerializeField] private float _spawnTimerInSeconds = 2;
    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject _girlDancer;
    [Range(.00001f, .005f)]
    public float aggressiveness ;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;

    Dictionary<string, Action> States;
    Vector2 moveVec;
    public float baseSpeed;
    public float chaseSpeed;
    /// <summary>
    /// Either use movement between points or random movement in an area.
    /// </summary>
    public bool UseTargetPoints;
    public float aggroRadius;

    public Vector2 focusAreaCenter;
    public float focusAreaRadius;

    public Vector3[] positions;
    int positionIndex;
    Vector3 currentTargetPosition;

    /// <summary>
    /// How fast the enemy spawns. Only affects enemy during spawn state
    /// </summary>
    public float SpawnTimerInSeconds
    {
        get => _spawnTimerInSeconds;
        set { _spawnTimerInSeconds = value; }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        States = new Dictionary<string, Action>();
        States["DefaultState"] = DefaultState; // Chase player
        States["SpawnState"] = SpawnState;
        States["ScaredState"] = ScaredState;
        States["AggroState"] = AggroState;
    }

    private void Start()
    {
        moveVec = Vector2.zero;
        _target = gm.player.gameObject;
        _girlDancer = gm.dancer.gameObject;
        EnterSpawnState();
    }

    void Update()
    {
        if (!GameManager.Instance.gamePlaying)
            return;

        States[CurrentState]();
        
    }

    void FixedUpdate()
    {
        if (!GameManager.Instance.gamePlaying)
            return;

        if (moveVec != Vector2.zero)
            _rb.MovePosition((Vector2)(transform.position) + moveVec * Time.fixedDeltaTime);
    }

    //STATES

    private void EnterSpawnState()
    {
        // Enemy starts invisible
        var color = _spriteRenderer.color;
        color.a = 0;
        _spriteRenderer.color = color;

        CurrentState = "SpawnState";
        SpawnState();
    }

        // The enemy will fade in over time
    private float _spawnCounter = 0;
    private void SpawnState()
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

    private void ExitSpawnState()
    {
        EnterDefaultState();
    }


    //      DEFAULT
    void EnterDefaultState()
    {
        CurrentState = "DefaultState";
        DefaultState();
    }

    // Default state is to chase the player
    void DefaultState()
    {

        if(UseTargetPoints)
            MoveBetweenPoints();
        else
            MoveInArea();

        if(( transform.position - _target.transform.position ).magnitude < aggroRadius)
            EnterAggroState();

        if(UnityEngine.Random.Range(0,1f) < (aggressiveness * (_target.transform.position - _girlDancer.transform.position).magnitude / _girlDancer.GetComponent<DancingGirl>().girlLight.pointLightInnerRadius  ))
            EnterAggroState();

        float distFromGirl = Mathf.Abs(Vector3.Distance(transform.position, _girlDancer.transform.position));
        if(distFromGirl < _girlDancer.GetComponent<DancingGirl>().girlLight.pointLightInnerRadius &&
            CurrentState != "ScaredState")
        {
            ExitDefaultState();
            EnterScaredState(6f);
        }
    }

    void ExitDefaultState()
    {
        
    }

    void EnterAggroState()
    {
        CurrentState = "AggroState";
        AggroState();
    }

    void AggroState()
    {
        MoveTowardsPlayer();
        float distFromGirl = Mathf.Abs(Vector3.Distance(transform.position, _girlDancer.transform.position));
        if(distFromGirl < _girlDancer.GetComponent<DancingGirl>().girlLight.pointLightInnerRadius &&
            CurrentState != "ScaredState")
        {
            ExitAggroState();
            EnterScaredState(6f);
        }
    }

    void ExitAggroState()
    {

    }

    public override void EnterScaredState(float scaredTimer)
    {
        CurrentState = "ScaredState";
        MoveAwayFromGirl();
        Invoke("EnterDefaultState", scaredTimer);
    }

    private void ScaredState()
    {
        MoveAwayFromGirl();
    }



    //FUNCTIONS

    void ZeroMoveDir()
    {
        moveVec = Vector2.zero;
    }

    void MoveTowardsPlayer()
    {
        moveVec = (Vector2)(_target.transform.position - transform.position).normalized*chaseSpeed;
    }

    void MoveAwayFromGirl()
    {
        moveVec = (Vector2)(transform.position - _girlDancer.transform.position).normalized * baseSpeed;
    }

    void MoveInArea()
    {
        if((currentTargetPosition- transform.position ).sqrMagnitude < .008f)
        {
            float angle = UnityEngine.Random.Range(0,2*Mathf.PI);
            float dist = UnityEngine.Random.Range(0f, focusAreaRadius);
            currentTargetPosition = focusAreaCenter + new Vector2( Mathf.Sin(angle) * dist, Mathf.Cos(angle) * dist);
        }
        moveVec = baseSpeed * (currentTargetPosition- transform.position ) .normalized;
    }

    void MoveBetweenPoints()
    {
        if(  (positions[positionIndex] - transform.position).sqrMagnitude < .008f  )  
        {
            positionIndex++;
            if(positionIndex>=positions.Length)
                positionIndex = 0;
        }
        moveVec = baseSpeed * (positions[positionIndex] - transform.position ) .normalized;  
    }




}
