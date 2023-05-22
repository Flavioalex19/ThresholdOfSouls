using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //Variables
    GameObject go_Player;


    private void Update()
    {
        if (go_Player != null)
        {
            if (go_Player.GetComponent<PlayerInput>().GetIsInteracting())
            {
                //Change this!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                go_Player.GetComponent<PlayerInput>().SetIsInteracting(false);
                go_Player.GetComponent<PlayerManager>().MyState = CharacterStates.None;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            go_Player = other.gameObject;
            other.GetComponent<PlayerInput>().SetCanInteract(true);
        }
        

    }
    private void OnTriggerStay(Collider other)
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            go_Player = null;
            other.GetComponent<PlayerInput>().SetCanInteract(false);
        }




    }
}
