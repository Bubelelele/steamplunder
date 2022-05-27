using System;
using UnityEngine;

public class DroppedCog : MonoBehaviour {

    [SerializeField] private Collider triggerCollider;
    [SerializeField] private Rigidbody rb;

    private bool _activated;
    
    private void Update() {
        if (!_activated && rb.IsSleeping()) {
            _activated = triggerCollider.enabled = true;
            rb.gameObject.layer = 0; //Set to default layer in order to allow for collisions with the player again
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && !PlayerData.AllSlotsFull) {
            EffectSpawner.SpawnPickupFX(transform.position);
            //sound
            PlayerData.CogPickedUp();
            Destroy(rb.gameObject);
        }
    }
}
