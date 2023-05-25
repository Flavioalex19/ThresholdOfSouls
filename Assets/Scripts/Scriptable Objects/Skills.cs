using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill")]
public class Skills : ScriptableObject
{

    #region Visible Variables

    [SerializeField] string skill_name;
    [SerializeField] string skill_description;
    [SerializeField] int skill_index;
    [SerializeField] int skill_lvl;
    [SerializeField] float skill_manaCost;
    [SerializeField] float skill_totalXP;
    [SerializeField] float skill_damage;
    [SerializeField] Sprite skill_emblemImage;

    #endregion

    public int GetSkillIndex() { return skill_index; }
    public Sprite GetSkillEmblemSprite() { return skill_emblemImage; }

}
