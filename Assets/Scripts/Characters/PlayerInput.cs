using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    #region Private Variables
    //Variables

    Transform _cam;// A reference to the main camera in the scenes transform
    Vector3 _camForward;// The current forward direction of the camera

    #region Movement Variables
    Vector3 cc_move;//Vector For the movement
    bool _isMoving;//Verify if the player is in movement
    #endregion

    #region Interaction Variables
    bool _canInteract = false;//If the player is allow to interact
    bool _isInteracting = false;//Verify if thge player is interacting with something
    #endregion


    //Components
    Movement cc_movement;
    PlayerManager pm_playerManager;
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
        pm_playerManager = GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_canInteract)
        {
            //Input
            if (Input.GetKeyDown(KeyCode.E))
            {
                _isInteracting = true;
                pm_playerManager.MyState = CharacterStates.Interacting;
            }
        }
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

    #region Get & Set
    public bool GetCanInteract()
    {
        return _canInteract;
    }
    public void SetCanInteract(bool canInteract)
    {
        _canInteract = canInteract;
    }
    public bool GetIsInteracting()
    {
        return _isInteracting;
    }
    public void SetIsInteracting(bool isInteracting)
    {
        _isInteracting=isInteracting;
    }

    #endregion
}
