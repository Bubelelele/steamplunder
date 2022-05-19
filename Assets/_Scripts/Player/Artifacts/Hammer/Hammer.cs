using UnityEngine;

public class Hammer : ArtifactWeaponBase {
    
    /*
     * 1. Write out hammer functionality *
     * 2. Add support for double interaction indicator *
     * 3. No cooldown on puzzle element hit ?
     * 4. Find similarities to axe and grapple and abstract them
     * 5. Test slowing down player turn and move speed when using artifact
     */
    
    [SerializeField] private float knockbackStength = 10f;

    private HammerHitbox _hitbox;
    private SphereCollider _sphereCollider;

    public override void Use() {
        base.Use();
        _animator.SetTrigger("Hammer");
    }

    protected override void Awake() {
        base.Awake();
        _hitbox = artifactObject.GetComponent<HammerHitbox>();
        _sphereCollider = artifactObject.GetComponent<SphereCollider>();
    }

    private void OnGroundHit() {
        if (_hitbox != null) _hitbox.EnableTrigger(this);
    }

    public override void ProcessHitboxData(Collider collider) {
        Vector3 colliderCenter = artifactObject.transform.position + _sphereCollider.center;
        float colliderRadius = _sphereCollider.radius;
        float distance = Vector3.Distance(colliderCenter, collider.transform.position);
        float fraction = 1f - Mathf.Clamp01(distance / colliderRadius);

        if (collider.TryGetComponent<IHittable>(out var hittable)) {
            hittable.Hit(Mathf.RoundToInt(damage * fraction), ArtifactType);
        }

        if (collider.TryGetComponent<Rigidbody>(out var rb)) {
            Vector3 knockbackDir = (collider.transform.position - colliderCenter).normalized;
            Vector3 knockbackVector = knockbackDir * knockbackStength * fraction;
            rb.AddForce(knockbackVector, ForceMode.Impulse);
        }
        
        if (collider.TryGetComponent<EnemyBase>(out var enemy)) {
            enemy.Stun();
        }
    }
    
}
