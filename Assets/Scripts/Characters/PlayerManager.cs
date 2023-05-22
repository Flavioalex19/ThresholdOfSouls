using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterStates
{
    None,
    Interacting,
    Searching,
    Fighting,
    Attacking,
    Dashing

}

public class PlayerManager : MonoBehaviour
{
    public CharacterStates MyState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
