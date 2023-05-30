using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script made by Flavio Alexandre(FA)
/// This Script is responsible for the canvas, in the object(NPC or item), to look at the main camera
/// </summary>

public class UILookAt : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }
}
