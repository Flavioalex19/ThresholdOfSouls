using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    PlayerInput input;
    PlayerManager pm_manager;
    Movement movement;
    Combo combo;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
        pm_manager = GetComponent<PlayerManager>();
        movement = GetComponent<Movement>();
        combo = GetComponent<Combo>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Movement Animations
        animator.SetFloat("Forward", movement.GetForwardAmount());
        animator.SetBool("isRunning", input.GetIsSprinting());
        
        #endregion
        #region Combat Animations
        animator.SetBool("isWithdrawing", pm_manager.GetIsWithdrawing());
        animator.SetBool("isFighting", pm_manager.GetIsFighting());
        animator.SetBool("canAttack", pm_manager.GetIsAttacking());
        animator.SetInteger("comboCount", combo.currentComboCount);
        animator.SetBool("isCasting", pm_manager.GetIsCastingSpell());
        #endregion

    }
}
