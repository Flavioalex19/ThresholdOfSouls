using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    
    // Update is called once per frame
    void Update()
    {
        ItemInteraction();
    }

    void ItemInteraction()
    {
        if (go_Player != null)
        {
            if (go_Player.GetComponent<PlayerInput>().GetIsInteracting())
            {
                go_Player.GetComponent<PlayerInput>().SetIsInteracting(false);
                go_Player.GetComponent<PlayerManager>().MyState = CharacterStates.Neutral;
                //go_Player = null;
                Destroy(gameObject);
            }
        }
    }
}
