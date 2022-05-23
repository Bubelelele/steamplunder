using System;
using UnityEngine;

public class DroppedCog : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            //Effect
            //Add to cog counter
        }
    }
}
