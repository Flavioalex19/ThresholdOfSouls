using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    #region Player Bars
    [SerializeField] Image ui_hp;
    [SerializeField] Image ui_mana;
    #endregion

    #region Skills Portraits
    [SerializeField]Sprite ui_sprite_skill_emptySkill;//if the player has no skill equiped in tje corresponded slot, this will be placed
    [SerializeField] Image ui_image_skill0;
    [SerializeField] Image ui_image_skill1;

    #endregion

    GameObject cc_Player;

    // Start is called before the first frame update
    void Start()
    {
        cc_Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSkillSlot0UI(Skills skills) { ui_image_skill0.sprite = skills.GetSkillEmblemSprite();}
    public void UpdateSkillSlot1UI(Skills skills) { ui_image_skill1.sprite =  skills.GetSkillEmblemSprite();}
}
