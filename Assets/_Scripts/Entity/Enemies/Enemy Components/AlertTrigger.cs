using UnityEngine;

public class AlertTrigger : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent<BanditBase>(out var bandit)) {
            bandit.AwareOfPlayer();
        }
    }
}