using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public enum CharacterStates
{
    Neutral,
    Interacting,
    Searching,
    Withdrawing,
    Fighting,
    Attacking,
    CastingSpell,
    Dashing,
    Waiting

}

public class PlayerManager : MonoBehaviour
{
    public CharacterStates MyState;
    #region Skill Variables
    public Skills _skillSlot0;
    public Skills _skillSlot1;
    #endregion

    bool _isWithdrawing = false;
    bool _isFighting = false;//if the player is in a fighting zone
    public bool _isAttacking = false;
    bool _isCastingSpell = false;

    public GameObject weapon;
    public Transform weaponSocket;
    public Transform weaponHolder;//when pick up a new weapon add to the holder


    public GameObject vfx_spell_lumenStrike;

    public AudioSource sfx_weaponSoundSource;
    public AudioSource sfx_step;

    Stats cc_Stats;
    Combo combo;


    private void Awake()
    {
        foreach (Transform child in vfx_spell_lumenStrike.transform)
        {
            // Get the ParticleSystem or VisualEffect component of each child
            ParticleSystem particleSystem = child.GetComponent<ParticleSystem>();
            VisualEffect visualEffect = child.GetComponent<VisualEffect>();

            // Play the VFX if a component is found
            if (particleSystem != null)
            {
                particleSystem.Stop();
            }
            else if (visualEffect != null)
            {
                visualEffect.Stop();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cc_Stats = GetComponent<Stats>();
        combo = GetComponent<Combo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isFighting)
        {
            
            MyState = CharacterStates.Fighting;
        }
        if (_isAttacking)
        {
            MyState = CharacterStates.Attacking;
        }
        if (_isCastingSpell)
        {
            MyState = CharacterStates.CastingSpell;
        }
        

        FSM();
    }

    #region Get & Set
    public Skills GetSkillSlot0() { return _skillSlot0; }
    public Skills GetSkillSlot1() { return _skillSlot1; }
    public bool GetIsWithdrawing() { return  _isWithdrawing; }
    public void SetIsWithdrawing(bool isWithdrawing) { _isWithdrawing = isWithdrawing; }
    public bool GetIsFighting(){ return _isFighting;}
    public void SetIsFighting(bool isFighting){ _isFighting = isFighting; }
    public bool GetIsAttacking() { return _isAttacking;}
    public void SetIsAttacking(bool isAttacking) { _isAttacking = isAttacking; }
    public bool GetIsCastingSpell() { return _isCastingSpell;}
    public void SetIsCastingSpell(bool isCastingSpell) { _isCastingSpell= isCastingSpell; }

    #endregion

    void FSM()
    {
        switch (MyState)
        {
            case CharacterStates.Neutral:
                
                break;
            case CharacterStates.Interacting:
                break;
            case CharacterStates.Searching:
                break;
            case CharacterStates.Withdrawing:
                WithdrawingAnimationEvent();
                break;
            case CharacterStates.Fighting:
                break;
            case CharacterStates.Attacking:
                StartCoroutine(Attacking());
                break;
            case CharacterStates.CastingSpell: 
                CastSpellAnimationEvent();
                break;
            case CharacterStates.Dashing:
                break;
            case CharacterStates.Waiting: 
                StartCoroutine (Waiting());
                break;
            default:
                break;
        }
    }

    void WithdrawingAnimationEvent()
    {
        StartCoroutine(Withdrawing());
    }
    IEnumerator Withdrawing()
    {
       
        
        yield return new WaitForSeconds(1f);
        _isWithdrawing = false;
        //MyState = CharacterStates.Fighting;
        _isFighting = true;
    }
    public void AttachWeaponToHand()
    {
        weapon.transform.SetParent(weaponSocket);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
    }
    IEnumerator Attacking()
    {
        /*
        yield return new WaitForSeconds(.5f);
        _isAttacking = false;
        combo.currentComboCount = 0;
        MyState = CharacterStates.Waiting;
        */
        // Play the attack animation here
        // For example: animator.Play("Attack");

        // Wait for the animation to complete
        yield return new WaitForSeconds(.85f);

        // Reset the combo count and transition to the appropriate state
        _isAttacking = false;
        combo.currentComboCount = 0;
        MyState = CharacterStates.Fighting;

    }
    void CastSpellAnimationEvent()
    {
        StartCoroutine(CastingSpell());
    }
    IEnumerator CastingSpell()
    {
        _isCastingSpell = false;
        yield return new WaitForSeconds(1f);
        _isFighting = true;
        MyState = CharacterStates.Fighting;
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(.3f);
        MyState = CharacterStates.Fighting;
    }

    //Return to neutral state
    public void ReturnToNeutral() { MyState = CharacterStates.Neutral;}

    #region Skill Slots 
    public void AddSkillToSkillSlot0(Skills skills){ _skillSlot0 = skills;}
    public void AddSkillToSkillSlot1(Skills skills) { _skillSlot1 = skills; }
    public void RemoveSkillOnSkillSlot0() { _skillSlot0 = null;}
    public void RemoveSkillOnSkillSlot1() { _skillSlot1 = null;}
    #endregion

    public void  PlayVFXAnimation()
    {
        foreach (Transform child in vfx_spell_lumenStrike.transform)
        {
            // Get the ParticleSystem or VisualEffect component of each child
            ParticleSystem particleSystem = child.GetComponent<ParticleSystem>();
            VisualEffect visualEffect = child.GetComponent<VisualEffect>();

            // Play the VFX if a component is found
            if (particleSystem != null)
            {
                particleSystem.Play();
            }
            else if (visualEffect != null)
            {
                visualEffect.Play();
            }
        }
    }

    public void SFXSetp()
    {
        sfx_step.Play();
    }
    public void SFXSwordAttack()
    {
        sfx_weaponSoundSource.Play();
    }
}
