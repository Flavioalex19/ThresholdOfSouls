using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    #region Private Variables
    //Variables
    Transform _cam;// A reference to the main camera in the scenes transform
    Vector3 _camForward;// The current forward direction of the camera
    Vector3 cc_move;
    bool _isMoving;

   

    //Components
    Movement cc_movement;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        // get the transform of the main camera
        if (Camera.main != null)
        {
            _cam = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
            // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
        }

        cc_movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        // read inputs
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // calculate move direction to pass to character
        if (_cam != null)
        {
            // calculate camera relative direction to move:
            _camForward = Vector3.Scale(_cam.forward, new Vector3(1, 0, 1)).normalized;
            cc_move = v * _camForward + h * _cam.right;
        }
        else
        {
            // we use world-relative directions in the case of no main camera
            cc_move = v * Vector3.forward + h * Vector3.right;
        }
        cc_movement.Move(cc_move, _isMoving);
    }
}
