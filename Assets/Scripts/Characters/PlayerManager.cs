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
    Dashing

}

public class PlayerManager : MonoBehaviour
{
    public CharacterStates MyState;

    bool _isFighting = false;

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

    public bool GetIsFighting()
    {
        return _isFighting;
    }
    public void SetIsFighting(bool isFighting)
    {
        _isFighting = isFighting;
    }

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
            default:
                break;
        }
    }
    public void ReturnToNeutral()
    {
        MyState = CharacterStates.Neutral;
    }
}
