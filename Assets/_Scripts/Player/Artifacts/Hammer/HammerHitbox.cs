using UnityEngine;

public class HammerHitbox : HitboxBase {

    private void OnTriggerStay(Collider other) {
        if (!_triggerEnabled) return;
        if (other.TryGetComponent<EntityBase>(out _)) {
            if (_colliders.Contains(other)) return;
            
            _colliders.Add(other);
            if (_artifact != null) _artifact.ProcessHitboxData(other);
        }
    }

    public override void EnableTrigger(ArtifactWeaponBase requester) {
        base.EnableTrigger(requester);
        Invoke(nameof(DisableTrigger), .05f);
    }
}
