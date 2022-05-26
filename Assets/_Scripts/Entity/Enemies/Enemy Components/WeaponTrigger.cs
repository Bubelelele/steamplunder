using System;
using UnityEngine;

public class WeaponTrigger : MonoBehaviour {
    //refactored this
    public GameObject enemy;

    private MeleeBandit _meleeBandit;
    private HeavyBandit _heavyBandit;
    private AttackScript _attackScript;

    private bool _isMelee;
    private bool _isHeavy;
    private bool _isBanditLeader;

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
        else if (enemy.GetComponent<AttackScript>() != null)
        {
            _attackScript = enemy.GetComponent<AttackScript>();
            _isBanditLeader = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (_isMelee && _meleeBandit.lethal && other.CompareTag("Player")) {
            PlayerData.Damage(_meleeBandit.attackDamage, transform);
            EffectSpawner.SpawnBloodFX(other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position));
        }
        if (_isHeavy && _heavyBandit.lethal && other.CompareTag("Player"))
        {
            PlayerData.Damage(_heavyBandit.attackDamage, transform);
            EffectSpawner.SpawnBloodFX(other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position));
        }
        if (_isBanditLeader && _attackScript.lethal && other.CompareTag("Player"))
        {
            PlayerData.Damage(_attackScript.attackDamage, transform);
            Debug.Log("hit");
            EffectSpawner.SpawnBloodFX(other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position));
        }
    }
}