using UnityEngine;

public class ParentWhileInTrigger : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") || other.TryGetComponent<Box>(out _)) {
            other.transform.parent = transform;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player") || other.TryGetComponent<Box>(out _)) {
            other.transform.parent = null;
        }
    }
}
