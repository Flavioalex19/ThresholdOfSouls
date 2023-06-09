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
    bool _isSprinting = false;
    #endregion

    /*
    #region Dash Variables
    public bool _canDash = true;
    public bool _isDashing = false;//if the player is in dash motion
    #endregion
    */

    #region Interaction Variables
    bool _canInteract = false;//If the player is allow to interact
    bool _isInteracting = false;//Verify if thge player is interacting with something
    #endregion

    public bool _isUsingSearchDevice = false;//If the player can use the search Device/Vision
    

    //Components
    Stats _stats;
    Movement cc_movement;
    PlayerManager pm_playerManager;
    Inventory inv_inventory;
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

        _stats = GetComponent<Stats>();
        cc_movement = GetComponent<Movement>();
        pm_playerManager = GetComponent<PlayerManager>();
        inv_inventory = GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cc_movement.GetCanDash() && Input.GetKeyDown(KeyCode.Space))
        {
            cc_movement.SetDashRequested(true);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            //pm_playerManager.SetIsFighting(true);
            pm_playerManager.SetIsWithdrawing(true);
            pm_playerManager.MyState = CharacterStates.Withdrawing;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //NOTE: Maybe put a waiting to use, to not spam
            _isUsingSearchDevice = !_isUsingSearchDevice;
            if ( _isUsingSearchDevice)
            {
                pm_playerManager.MyState = CharacterStates.Searching;
            }
            else pm_playerManager.MyState = CharacterStates.Neutral;
            //pm_playerManager.MyState = CharacterStates.Searching;
        }

        #region Combat

        if (Input.GetMouseButtonDown(0) && pm_playerManager.MyState == CharacterStates.Fighting)
        {
            //pm_playerManager.SetIsAttacking(true);
        }

        //if is Fighting - TODO!!!!!!!!!!!!!!!!!!!!!!!!!!
        if (Input.GetMouseButtonDown(1))
        {
            if (inv_inventory.GetSkillSlot0())
            {
                if (_stats.CheckManaToCast((inv_inventory.GetSkillSlot0())))
                {
                    _stats.SetMana(_stats.GetMana() - inv_inventory.GetSkillSlot0().GetSkillManaCost());
                    pm_playerManager.SetIsCastingSpell(true);
                    //pm_playerManager.MyState = CharacterStates.CastingSpell;
                }
                
            }
            else print("Not");
                
        }
        #endregion

        if (Input.GetKey(KeyCode.LeftShift)) _isSprinting = true;
        else _isSprinting = false;

        PlayerInteraction();
        
    }
    private void FixedUpdate()
    {

        if(pm_playerManager.MyState == CharacterStates.Neutral || pm_playerManager.MyState == CharacterStates.Fighting)
        {
            MyMovement();
        }
        

        

    }

    #region Get & Set
    public bool GetCanInteract() { return _canInteract; }
    public void SetCanInteract(bool canInteract) { _canInteract = canInteract;}
    public bool GetIsInteracting() { return _isInteracting; }
    public void SetIsInteracting(bool isInteracting) { _isInteracting=isInteracting; }
    public bool GetIsSprinting() { return _isSprinting; }
    #endregion
    void MyMovement()
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
        cc_movement.Move(cc_move, _isMoving, _isSprinting);
    }

    void PlayerInteraction()
    {
        //If the player can Interact
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
}
