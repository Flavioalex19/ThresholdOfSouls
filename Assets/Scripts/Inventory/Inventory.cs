using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    #region Skill Variables
    public Skills _skillSlot0;
    public Skills _skillSlot1;
    #endregion

    public Skills GetSkillSlot0() { return _skillSlot0; }
    public Skills GetSkillSlot1() { return _skillSlot1; }

    #region Skill Slots 
    public void AddSkillToSkillSlot0(Skills skills) { _skillSlot0 = skills; }
    public void AddSkillToSkillSlot1(Skills skills) { _skillSlot1 = skills; }
    public void RemoveSkillOnSkillSlot0() { _skillSlot0 = null; }
    public void RemoveSkillOnSkillSlot1() { _skillSlot1 = null; }
    #endregion
}
