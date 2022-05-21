using System;
using UnityEngine;

public class WeaponTrigger : MonoBehaviour {
    //refactored this
    public GameObject enemy;

    private MeleeBandit _meleeBandit;

    private void Awake() {
        _meleeBandit = enemy.GetComponent<MeleeBandit>();
    }

    private void OnTriggerEnter(Collider other) {
        if (_meleeBandit.lethal && other.CompareTag("Player")) {
            PlayerData.Damage(_meleeBandit.attackDamage, transform);
        }
    }
}