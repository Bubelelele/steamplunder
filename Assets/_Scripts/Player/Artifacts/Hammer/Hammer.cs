using UnityEngine;

public class Hammer : ArtifactWeaponBase {

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
        AudioManager.PlayAudio(AudioType.HammerSmash_Player);
        EffectSpawner.SpawnHammerShockFX(artifactObject.transform.position);
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
