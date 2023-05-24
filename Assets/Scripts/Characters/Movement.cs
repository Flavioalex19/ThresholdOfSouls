using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] float stationaryTurnSpeed;
    [SerializeField] float movingTurnSpeed;
    [SerializeField] float characterSpeed;

    [Header("Dash Variables")]
    [SerializeField] float dashDistance;
    [SerializeField] float dashDuration;
    [SerializeField] float dashCooldown;
    [SerializeField] float stopDuration;

    float forwardAmount;
    float turnAmount;
    Vector3 groundNormal;

    bool _canDash = true;
    [SerializeField]bool _isDashing = false;
    bool _dashRequested = false;
    Vector3 _dashDirection;
    float _dashStartTime;
    float _stopStartTime;
    Vector3 _previousVelocity;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_canDash && Input.GetKeyDown(KeyCode.Space))
        {
            //_dashRequested = true;
        }
    }

    private void FixedUpdate()
    {
        if (_dashRequested)
        {
            Dash();
            _dashRequested = false;
        }

        if (_isDashing)
        {
            DashProgress();
        }
        else if (!_canDash)
        {
            DashStopProgress();
        }
    }

    #region Get & Set
    public float GetForwardAmount()
    {
        return forwardAmount;
    }
    public bool GetCanDash()
    {
        return _canDash;
    }
    public void SetCanDash(bool canDash)
    {
        _canDash = canDash;
    }
    public bool GetDashRequest()
    {
        return _dashRequested;
    }
    public void SetDashRequested(bool dashRequested)
    {
        _dashRequested = dashRequested;
    }
    #endregion

    public void Move(Vector3 move, bool isMoving, bool isSprinting)
    {
        isMoving = true;
        if (!_isDashing)
        {
            // convert the world relative moveInput vector into a local-relative
            // turn amount and forward amount required to head in the desired
            // direction.
            if (move.magnitude > 1f) move.Normalize();
            move = transform.InverseTransformDirection(move);
            move = Vector3.ProjectOnPlane(move, groundNormal);
            turnAmount = Mathf.Atan2(move.x, move.z);
            forwardAmount = move.z;

            ApplyExtraTurnRotation();

            if (isSprinting) forwardAmount = forwardAmount * 2;

            transform.position += transform.forward * Time.deltaTime * forwardAmount * characterSpeed;
        }
        
    }

    void ApplyExtraTurnRotation()
    {
        // help the character turn faster (this is in addition to root rotation in the animation)
        float turnSpeed = Mathf.Lerp(stationaryTurnSpeed, movingTurnSpeed, forwardAmount);
        transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
    }

    private void Dash()
    {
        if (!_isDashing)
        {
            _isDashing = true;
            _canDash = false;

            _dashDirection = transform.forward;
            _dashStartTime = Time.fixedTime;

            Invoke(nameof(EnableDash), dashCooldown);
        }
    }

    void DashProgress()
    {
        /*
        float dashProgress = (Time.fixedTime - _dashStartTime) / dashDuration;
        Vector3 dashVelocity = _dashDirection * (dashDistance / dashDuration) ;
        rb.velocity = dashVelocity;

        if (dashProgress >= 1f)
        {
            _isDashing = false;
            _previousVelocity = rb.velocity;
            _stopStartTime = Time.fixedTime;
            Invoke(nameof(DashStopProgress), stopDuration);
        }
        */
        float dashProgress = (Time.fixedTime - _dashStartTime) / dashDuration;
        float remainingDistance = dashDistance * (1f - dashProgress);
        float modifiedSpeed = remainingDistance / Time.deltaTime;

        rb.velocity = _dashDirection * modifiedSpeed;

        if (dashProgress >= 1f)
        {
            _isDashing = false;
            _previousVelocity = rb.velocity;
            _stopStartTime = Time.fixedTime;
            Invoke(nameof(DashStopProgress), stopDuration);
        }

    }

    void DashStopProgress()
    {
        float stopProgress = (Time.fixedTime - _stopStartTime) / stopDuration;
        rb.velocity = Vector3.Lerp(_previousVelocity, Vector3.zero, stopProgress);

        if (stopProgress >= 1f)
        {
            print("Here");
            rb.velocity = Vector3.zero; // Stop the character completely
            _canDash = true;
        }
    }

    private void EnableDash()
    {
        _canDash = true;
    }
}




