using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    public float comboTimeThreshold = 0.8f; // Time threshold between attacks to register as a combo
    public int maxComboCount = 3; // Maximum number of attacks in a combo

    public int currentComboCount = 0; // Current number of attacks in the combo
    private float lastAttackTime = 0f; // Time of the last attack

    PlayerManager pm_playerManager;

    private void Start()
    {
        pm_playerManager = GetComponent<PlayerManager>();
    }

    private void Update()
    {
        // Check for attack input (mouse button)
        if (Input.GetMouseButtonDown(0))
        {
            // Calculate the time since the last attack
            float timeSinceLastAttack = Time.time - lastAttackTime;

            // If enough time has passed since the last attack, reset the combo count
            if (timeSinceLastAttack > comboTimeThreshold)
            {
                print("Here");
                currentComboCount = 0;
                pm_playerManager.SetIsAttacking(false);
                pm_playerManager.MyState = CharacterStates.Fighting;
            }

            // Perform the attack
            PerformAttack();

            // Update the last attack time and increment the combo count
            lastAttackTime = Time.time;
            currentComboCount++;

            // If the combo count exceeds the maximum, reset it
            if (currentComboCount > maxComboCount)
            {
                print("Heeeee");
                currentComboCount = 1;
                //pm_playerManager.SetIsAttacking(false);
            }
        }
    }

    private void PerformAttack()
    {
        pm_playerManager.SetIsAttacking(true);
        Debug.Log("Performing Attack " + currentComboCount);
    }
}

