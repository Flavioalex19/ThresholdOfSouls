using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimationManager : MonoBehaviour
{
    NavMeshAgent _agent;
    Animator _animator;

    AiBehavior _aiBehavior;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _aiBehavior = GetComponent<AiBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_agent.isStopped == true) _animator.SetBool("isMoving", false);
        else _animator.SetBool("isMoving", true);
        
        _animator.SetFloat("Forward", _agent.speed);

        //Attack
        if(_aiBehavior.GetCanAttack())
        _animator.SetTrigger("Attack");
    }
}
