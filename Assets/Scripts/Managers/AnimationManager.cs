using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    PlayerInput input;
    PlayerManager pm_manager;
    Movement movement;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
        pm_manager = GetComponent<PlayerManager>();
        movement = GetComponent<Movement>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Forward", movement.GetForwardAmount());
        animator.SetBool("isRunning", input.GetIsSprinting());
        animator.SetBool("isWithdrawing", pm_manager.GetIsWithdrawing());
        animator.SetBool("isFighting", pm_manager.GetIsFighting());
        animator.SetBool("isCasting", pm_manager.GetIsCastingSpell());
    }
}
