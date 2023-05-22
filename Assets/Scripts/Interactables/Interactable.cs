using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //Variables
    protected GameObject go_Player;


    private void Update()
    {
        
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
