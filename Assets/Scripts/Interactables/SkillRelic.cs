using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRelic : Item
{
    [SerializeField]Skills relic_Skill;

    #region Floating Variables
    [SerializeField] private float _floatSpeed = 1f;// Speed of the floating movement
    [SerializeField] private float _floatAmplitude = 1f;// Maximum height of the float
    private Vector3 _startPosition;
    private float _timeOffset;
    #endregion

    UiManager uim_uiManager;
    Inventory inv_inventory;

    private void Start()
    {
        uim_uiManager = GameObject.Find("UI Manager").GetComponent<UiManager>();
        inv_inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        _startPosition = transform.position;
        _timeOffset = Random.Range(0f, 2f * Mathf.PI); // Generate a random time offset to create variation in the floating motion
    }

    // Update is called once per frame
    void Update()
    {

        ItemInteraction();
    }
    private void FixedUpdate()
    {
        ItemFloating();
    }
    protected override void ItemInteraction()
    {
        if (go_Player != null)
        {
            if (go_Player.GetComponent<PlayerInput>().GetIsInteracting())
            {
                go_Player.GetComponent<PlayerInput>().SetIsInteracting(false);
                if (VerifyToAddSkillSlot())
                {
                    //go_Player.GetComponent<PlayerManager>().MyState = CharacterStates.Neutral;
                    //TODO-Update the ui manager
                    Destroy(gameObject);
                }
                go_Player.GetComponent<PlayerManager>().MyState = CharacterStates.Neutral;
            }
        }
    }
    //Verify if the slot if full to add the skill
    bool VerifyToAddSkillSlot()
    {
        if(go_Player.GetComponent<Inventory>().GetSkillSlot0() == null)
        {
            go_Player.GetComponent<Inventory>().AddSkillToSkillSlot0(relic_Skill);
            uim_uiManager.UpdateSkillSlot0UI(relic_Skill);
            return true;
        }
        else if(go_Player.GetComponent<Inventory>().GetSkillSlot1() == null)
        {
            go_Player.GetComponent<Inventory>().AddSkillToSkillSlot1(relic_Skill);
            uim_uiManager.UpdateSkillSlot1UI(relic_Skill);
            return true;
        }
        else return false;
    }

    void ItemFloating()
    {
        // Calculate the vertical position using a sine wave
        float newY = _startPosition.y + Mathf.Sin((Time.time + _timeOffset) * _floatSpeed) * _floatAmplitude;

        // Update the object's position
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Rotate the object slowly around the Y-axis
        transform.Rotate(Vector3.up, Time.deltaTime * 20f);
    }
   

}
