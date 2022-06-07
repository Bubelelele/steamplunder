using System;
using UnityEngine;

public class Campfire : MonoBehaviour {

    [SerializeField] private int tickDamage = 1;
    [SerializeField] private float tickSpeed = .5f;

    private bool _canTickDamage;
    
    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player") && !_canTickDamage) {
            PlayerData.Damage(tickDamage);
            _canTickDamage = true;
            Invoke(nameof(ResetCanTickDamage), tickSpeed);
        }
    }

    private void ResetCanTickDamage() => _canTickDamage = false;
}
