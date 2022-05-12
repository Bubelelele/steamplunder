using System.Collections.Generic;
using UnityEngine;

public class Hammer : ArtifactBase {
    
    /*
     * 1. Write out hammer functionality *
     * 2. Add support for double interaction indicator *
     * 3. No cooldown on puzzle element hit ?
     * 4. Find similarities to axe and abstract them
     * 5. Test slowing down player turn and move speed when using artifact
     */
    
    [SerializeField] private float knockbackStength = 10f;

    public override void Use() {
        base.Use();
        _animator.SetTrigger("Hammer");
    }

    private void OnGroundHit() {
        if (artifactObject.TryGetComponent<HammerHitbox>(out var hitbox)) {
            hitbox.EnableTrigger(this);
        }
    }

    public void ProcessHitboxData(List<Transform> colliders, Vector3 colliderCenter, float colliderRadius) {
        foreach (var collider in colliders) {
            float distance = Vector3.Distance(colliderCenter, collider.position);
            float fraction = 1f - Mathf.Clamp01(distance / colliderRadius);

            if (collider.TryGetComponent<IHittable>(out var hittable)) {
                hittable.Hit(Mathf.RoundToInt(damage * fraction), Artifact.Hammer);
            }

            if (collider.TryGetComponent<Rigidbody>(out var rb)) {
                Vector3 knockbackDir = (collider.position - colliderCenter).normalized;
                Vector3 knockbackVector = knockbackDir * knockbackStength * fraction;
                rb.AddForce(knockbackVector, ForceMode.Impulse);
            }
            
            if (collider.TryGetComponent<EnemyBase>(out var enemy)) {
                enemy.Stun();
            }
        }

    }
    
}
