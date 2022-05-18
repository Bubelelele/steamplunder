using UnityEngine;

public class HammerHitbox : HitboxBase {

    private void OnTriggerStay(Collider other) {
        if (!_triggerEnabled) return;
        if (_artifact == null) {
            Debug.LogWarning($"Artifact connection missing on: {gameObject.name}");
            return;
        }
        if (other.TryGetComponent<EntityBase>(out _)) {
            CheckCollider(other);
        }
    }

    public override void EnableTrigger(ArtifactWeaponBase requester) {
        base.EnableTrigger(requester);
        Invoke(nameof(DisableTrigger), .05f);
    }
}
