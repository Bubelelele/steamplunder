using System;
using UnityEngine;

public class WeaponTrigger : MonoBehaviour {
    //refactored this
    public GameObject enemy;

    private MeleeBandit _meleeBandit;
    private HeavyBandit _heavyBandit;

    private bool _isMelee;
    private bool _isHeavy;

    private void Awake() {
        if(enemy.GetComponent<MeleeBandit>() != null)
        {
            _meleeBandit = enemy.GetComponent<MeleeBandit>();
            _isMelee = true;
        }
        else if(enemy.GetComponent<HeavyBandit>() != null)
        {
            _heavyBandit = enemy.GetComponent<HeavyBandit>();
            _isHeavy = true;
        }  
    }

    private void OnTriggerEnter(Collider other) {
        if (_isMelee && _meleeBandit.lethal && other.CompareTag("Player")) {
            PlayerData.Damage(_meleeBandit.attackDamage, transform);
        }
        if (_isHeavy && _heavyBandit.lethal && other.CompareTag("Player"))
        {
            PlayerData.Damage(_heavyBandit.attackDamage, transform);
        }
    }
}