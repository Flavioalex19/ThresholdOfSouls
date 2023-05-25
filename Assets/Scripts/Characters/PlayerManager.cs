using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterStates
{
    Neutral,
    Interacting,
    Searching,
    Fighting,
    Attacking,
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

    bool _isFighting = false;//if the player is in a fighting zone

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isFighting)
        {
            MyState = CharacterStates.Fighting;
        }
        

        FSM();
    }

    #region Get & Set
    public Skills GetSkillSlot0() { return _skillSlot0; }
    public Skills GetSkillSlot1() { return _skillSlot1; }
    public bool GetIsFighting(){ return _isFighting;}
    public void SetIsFighting(bool isFighting){ _isFighting = isFighting; }

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
            case CharacterStates.Fighting:
                break;
            case CharacterStates.Attacking:
                break;
            case CharacterStates.Dashing:
                break;
            case CharacterStates.Waiting: 
                break;
            default:
                break;
        }
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
