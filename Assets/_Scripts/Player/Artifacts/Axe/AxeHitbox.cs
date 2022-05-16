using UnityEngine;

public class AxeHitbox : HitboxBase {

    private void OnTriggerStay(Collider other) {
        if (!_triggerEnabled) return;
        if (_artifact == null) {
            Debug.LogWarning($"Artifact connection missing on: {gameObject.name}");
            return;
        }
        if (other.TryGetComponent<IHittable>(out _)) {
            if (_colliders.Contains(other)) return;
            
            _colliders.Add(other);
            if (_artifact != null) _artifact.ProcessHitboxData(other);
        }
    }

}
