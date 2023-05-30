using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Stats
{
    bool _isTarget = false;
    [SerializeField]GameObject ui_enemy_canvas;
    [SerializeField]Image ui_enemyHP_Bar;

    [SerializeField]Animator animator_ui_enemyHP;

    [SerializeField] GameObject cc_player;
    int cc_combo_playerCurrentCombo;

    // Start is called before the first frame update
    void Start()
    {
        _hp = _maxHp;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (_isTarget) animator_ui_enemyHP.SetBool("isOn", _isTarget);
        else animator_ui_enemyHP.SetBool("isOn", _isTarget);

        ui_enemyHP_Bar.fillAmount = _hp/_maxHp;
    }

    public void SetIsTarget(bool isTarget)
    {
        _isTarget = isTarget;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon" && cc_player.GetComponent<PlayerManager>().MyState == CharacterStates.Attacking)
        {
            if(_hasBeenHit == false || cc_player.GetComponent<Combo>().currentComboCount != cc_combo_playerCurrentCombo)
            {
                _particleSystem_Hit.Play();
                cc_combo_playerCurrentCombo = cc_player.GetComponent<Combo>().currentComboCount;
                _hp -= 20;
                _hasBeenHit = true;
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _hasBeenHit = false;
    }
}
