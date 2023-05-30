using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{

    [SerializeField] protected int _lvl = 1;
    [SerializeField] protected float _hp;
    [SerializeField] protected float _maxHp;
    [SerializeField] float _mana;
    [SerializeField] float _maxMana;
    [SerializeField] float _xp;
    [SerializeField] float _maxXp;
    [SerializeField]public  float _attack;

    [SerializeField]protected bool _hasBeenHit = false;
    protected bool _isInvinsible = false;

    [SerializeField]protected ParticleSystem _particleSystem_Hit;


    private void Start()
    {
        _hp = _maxHp;
        _mana = _maxMana;
    }

    #region Get & Set
    public int GetLvl()
    {
        return _lvl;
    }
    public void SetLvl(int lvl)
    {
        _lvl = lvl;
    }
    public float GetHp()
    {
        return _hp;
    }
    public void SetHP(float hp) 
    {
        _hp = hp;
    }
    public float GetMaxHp()
    {
        return _maxHp;
    }
    public void SetMaxHp(float maxHp)
    {
        _maxHp = maxHp;
    }
    public float GetMana()
    {
        return _mana;
    }
    public void SetMana(float mana)
    {
        _mana = mana;
    }
    public float GetMaxMana()
    {
        return _maxMana;
    }
    public void SetMaxMana(float maxMana)
    {
        _maxMana = maxMana;
    }
    public float GetXP()
    {
        return _xp;
    }
    public void SetXP(float xp)
    {
        _xp = xp;
    }
    public float GetMaxXp()
    {
        return _maxXp;
    }
    public void SetMaxXP(float maxXp)
    {
        _maxXp = maxXp;
    }
    
    #endregion

    public bool CheckManaToCast(Skills skills)
    {
        if(skills.GetSkillManaCost() < _mana)
        {
            return true;
        }
        else return false;
    }

    public void AttackStart()
    {

    }
    public void AttackEnd()
    {

    }

    public void Hit()
    {
        _particleSystem_Hit.Play();
    }

}
