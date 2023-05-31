using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public enum EnemyStates
{
    None,
    Searching,
    Patrolling,
    Pursuing,
    Attacking,
    RecoverFromAttack,
    Waiting
}

public class AiBehavior : MonoBehaviour
{
    [SerializeField]EnemyStates enemyStates;

    [SerializeField] List<Transform> list_PatrollWaypoints = new List<Transform>();

    [SerializeField]Transform _wp_target;
    bool _hasArrived = false;

    //Pursuing/Attacaking Variables
    [SerializeField]Transform _myTarget;

    //Waiting/searching varoables
    [SerializeField]float _waitingTime = 2f;

    //Components
    Perception _perception;
    NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
        _perception = GetComponent<Perception>();
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        _perception.DetectTarget();
        if (_perception.DetectTarget()) enemyStates = EnemyStates.Pursuing;//If the enemy detected a target go to pursuing state

        switch (enemyStates)
        {
            case EnemyStates.None:
                Waiting();
                break;
            case EnemyStates.Searching:
                break;
            case EnemyStates.Patrolling:
                GoToWaypoint();
                break;
            case EnemyStates.Pursuing:
                Pursuing();
                break;
            case EnemyStates.Attacking:
                //Attack
                break;
            case EnemyStates.RecoverFromAttack:
                break;
            case EnemyStates.Waiting:
                Waiting();
                break;
            default:
                break;

        }

        if (enemyStates == EnemyStates.Pursuing) _agent.speed = 4.5f;
        else _agent.speed = 2f;
    }
    void ChooseWp()
    {
        int dice;
        dice = Random.Range(0, list_PatrollWaypoints.Count);
        if(list_PatrollWaypoints[dice] == _wp_target) dice = Random.Range(0, list_PatrollWaypoints.Count);//If the roll dice is the same as the current waypoint target, re-roll
        _wp_target = list_PatrollWaypoints[dice];
        //transform.LookAt( _wp_target );
        RotateTowardsTarget(_wp_target, 20f);
    }
    void GoToWaypoint()
    {
        if(_wp_target != null && !_hasArrived)
        {
            _agent.SetDestination(_wp_target.position);
            if(_agent.remainingDistance <= _agent.stoppingDistance + 1)
            {
                _hasArrived = true;
                enemyStates = EnemyStates.Waiting;
            }

        }
    }
    void Pursuing()
    {
        
        _myTarget = _perception.DetectTarget();
        if(_myTarget != null)
        {
            _agent.SetDestination(_myTarget.position);
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                _hasArrived = true;
                _agent.isStopped = true;
                enemyStates = EnemyStates.Attacking;


            }
            
        }
        else enemyStates = EnemyStates.Waiting;
        
    }
    void Waiting()
    {
        Wait();
    }
    void Wait()
    {
        _hasArrived = false;
        float patrollingTime = _waitingTime;
       while(patrollingTime > 0)
        {
            patrollingTime-= Time.deltaTime;
        }
        ChooseWp();
        enemyStates = EnemyStates.Patrolling;
    }
    public void RotateTowardsTarget(Transform target, float rotationSpeed)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    
}
