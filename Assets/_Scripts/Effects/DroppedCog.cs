using System;
using UnityEngine;

public class DroppedCog : MonoBehaviour {
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player")) {
            EffectSpawner.SpawnPickupFX(transform.position);
            //sound
            //Add to cog counter
            Destroy(gameObject);
        }
    }
}
