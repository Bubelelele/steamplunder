using System;
using UnityEngine;
using UnityEngine.Events;

public class DroppedCog : MonoBehaviour {

    [SerializeField] private Collider triggerCollider;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private UnityEvent onPickedUp;

    private bool _activated;
    
    private void Update() {
        if (!_activated && rb.velocity == Vector3.zero) {
            _activated = triggerCollider.enabled = true;
            rb.gameObject.layer = 0; //Set to default layer in order to allow for collisions with the player again
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player") && !PlayerData.AllSlotsFull) {
            EffectSpawner.SpawnPickupFX(transform.position);
            PlayerData.CogPickedUp();
            onPickedUp.Invoke();
            Destroy(rb.gameObject);
        }
    }
}
