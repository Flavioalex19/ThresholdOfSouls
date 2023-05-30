using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] Transform _playerTransform;                // Target object to follow
    [SerializeField] float _distance = 5f;             // Distance from the target
    [SerializeField] float _heightOffset = 2f;         // Height offset from the target
    [SerializeField] float _lockOnHeightIncrease = 0.5f; // Height increase when locked on
    [SerializeField] float _lockOnDistanceIncrease = 1f; // Distance increase when locked on
    [SerializeField] float _rotationSpeed = 5f;        // Camera rotation speed
    [SerializeField] float _minVerticalAngle = -60f;   // Minimum vertical angle limit
    [SerializeField] float _maxVerticalAngle = 60f;    // Maximum vertical angle limit

    private float _currentHorizontalAngle = 0f;
    private float _currentVerticalAngle = 0f;

    public Transform _enemyTarget;
    private bool _lockOnTarget = false;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        // Toggle lock-on target
        if (Input.GetKeyDown(KeyCode.L))
        {
            _lockOnTarget = !_lockOnTarget;
            _enemyTarget.GetComponent<Enemy>().SetIsTarget(_lockOnTarget);//me00000
        }
    }
    private void FixedUpdate()
    {
        // Check if the target is assigned
        if (_playerTransform == null)
            return;

        // Get the rotation inputs from the mouse movement
        float rotationInputX = Input.GetAxis("Mouse X");
        float rotationInputY = Input.GetAxis("Mouse Y");

        // Update the camera angles based on the mouse inputs
        _currentHorizontalAngle += _rotationSpeed * rotationInputX;
        _currentVerticalAngle -= _rotationSpeed * rotationInputY;
        _currentVerticalAngle = Mathf.Clamp(_currentVerticalAngle, _minVerticalAngle, _maxVerticalAngle);

        // Calculate the rotation based on the current angles
        Quaternion rotation = Quaternion.Euler(_currentVerticalAngle, _currentHorizontalAngle, 0f);

        // Calculate the desired position of the camera with offset
        Vector3 desiredPosition;
        Vector3 lookAtTarget;

        if (_lockOnTarget && _enemyTarget != null)
        {
            // Calculate the position offset from the locked-on enemy target
            Vector3 targetOffset = _enemyTarget.position - _playerTransform.position;
            desiredPosition = _playerTransform.position - targetOffset.normalized * (_distance + _lockOnDistanceIncrease);
            lookAtTarget = _enemyTarget.position;
            
            // Increase the height when locked on
            desiredPosition += Vector3.up * (_heightOffset + _lockOnHeightIncrease);
        }
        else
        {
            desiredPosition = _playerTransform.position + rotation * new Vector3(0f, _heightOffset, -_distance);
            lookAtTarget = _playerTransform.position;
        }

        // Smoothly move the camera towards the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, _rotationSpeed * Time.deltaTime);

        // Update the camera's look at target
        transform.LookAt(lookAtTarget + Vector3.up * _heightOffset);

        // Check if the camera has reached the maximum or minimum vertical angle
        if (_currentVerticalAngle <= _minVerticalAngle || _currentVerticalAngle >= _maxVerticalAngle)
        {
            // Reset the camera angles to prevent flipping
            _currentVerticalAngle = Mathf.Clamp(_currentVerticalAngle, _minVerticalAngle, _maxVerticalAngle);
            _currentHorizontalAngle -= _rotationSpeed * rotationInputX;
        }

        
    }
}






