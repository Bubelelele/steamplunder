using UnityEngine;

public class OOBNet : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent<Player>(out var player)) player.DieAnimFinished();
    }
}
