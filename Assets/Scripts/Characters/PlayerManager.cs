using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    bool _isCastingSpell = false;

    public GameObject weapon;
    public Transform weaponSocket;
    public Transform weaponHolder;//when pick up a new weapon add to the holder

    Stats cc_Stats;

    // Start is called before the first frame update
    void Start()
    {
        cc_Stats = GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isFighting)
        {
            MyState = CharacterStates.Fighting;
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
                break;
            case CharacterStates.CastingSpell: 
                CastSpellAnimationEvent();
                break;
            case CharacterStates.Dashing:
                break;
            case CharacterStates.Waiting: 
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
        print("Here");
        weapon.transform.SetParent(weaponSocket);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
        yield return new WaitForSeconds(1f);
        _isWithdrawing = false;
        //MyState = CharacterStates.Fighting;
        _isFighting = true;
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

    //Return to neutral state
    public void ReturnToNeutral() { MyState = CharacterStates.Neutral;}

    #region Skill Slots 
    public void AddSkillToSkillSlot0(Skills skills){ _skillSlot0 = skills;}
    public void AddSkillToSkillSlot1(Skills skills) { _skillSlot1 = skills; }
    public void RemoveSkillOnSkillSlot0() { _skillSlot0 = null;}
    public void RemoveSkillOnSkillSlot1() { _skillSlot1 = null;}
    #endregion

}
