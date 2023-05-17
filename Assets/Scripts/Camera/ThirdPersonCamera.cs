using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;                // Target object to follow
    public float distance = 5f;             // Distance from the target
    public float heightOffset = 2f;         // Height offset from the target
    public float rotationSpeed = 5f;        // Camera rotation speed
    public float minVerticalAngle = -60f;   // Minimum vertical angle limit
    public float maxVerticalAngle = 60f;    // Maximum vertical angle limit

    private float currentHorizontalAngle = 0f;
    private float currentVerticalAngle = 0f;

    private void FixedUpdate()
    {
        // Check if the target is assigned
        if (target == null)
            return;

        // Get the rotation inputs from the mouse movement
        float rotationInputX = Input.GetAxis("Mouse X");
        float rotationInputY = Input.GetAxis("Mouse Y");

        // Update the camera angles based on the mouse inputs
        currentHorizontalAngle += rotationSpeed * rotationInputX;
        currentVerticalAngle -= rotationSpeed * rotationInputY;
        currentVerticalAngle = Mathf.Clamp(currentVerticalAngle, minVerticalAngle, maxVerticalAngle);

        // Calculate the rotation based on the current angles
        Quaternion rotation = Quaternion.Euler(currentVerticalAngle, currentHorizontalAngle, 0f);

        // Calculate the desired position of the camera with offset
        Vector3 desiredPosition = target.position + rotation * new Vector3(0f, heightOffset, -distance);

        // Smoothly move the camera towards the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, rotationSpeed * Time.deltaTime);

        // Update the camera's look at target
        transform.LookAt(target.position + Vector3.up * heightOffset);
    }
}
