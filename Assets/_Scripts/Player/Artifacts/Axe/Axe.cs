using UnityEngine;
using Random = UnityEngine.Random;

public class Axe : ArtifactWeaponBase {
    
    [SerializeField] private GameObject lvl2Object;
    [SerializeField] private GameObject lvl3Object;
    [SerializeField] private int damageLvl2 = 30;
    [SerializeField] private float comboTimingWindow = .3f;
    //add support for different damage multipliers based which attack

    private float _queuedTime;
    private AxeHitbox _hitbox;
    private float _currentDamageMultiplier;

    public override void Use() {
        base.Use();
        _animator.SetTrigger("Attack 1");
        _currentDamageMultiplier = 1f;
        if (artifactObject.TryGetComponent<AxeHitbox>(out var axeHitbox)) {
            _hitbox = axeHitbox;
        }
    }

    private int GetDamageValue() => PlayerData.ArtifactStatus[Artifact.Spin] ? damageLvl2 : damage;

    protected override void Awake() {
        base.Awake();
        PlayerData.OnArtifactUnlocked += OnArtifactUnlocked;
    }

    private void Update() {
        if (_ready) return;

        _queuedTime -= Time.deltaTime;
        if (Input.GetKeyDown(InputKey)) _queuedTime = comboTimingWindow;
    }

    private void Attack1Ended(bool interrupt = false) {
        if (_queuedTime > 0f) {
            _animator.SetTrigger("Attack 2");
            _currentDamageMultiplier = 1.2f;
        } else if (!interrupt)
            ActionEnded();
    }

    private void Attack2Ended() {
        if (_queuedTime > 0f) {
            _currentDamageMultiplier = 1.5f;
            if (PlayerData.ArtifactStatus[Artifact.Spin])
                // 50/50 for spin or bash attack
                if (Random.value > .5f) {
                    _animator.SetTrigger("Spin Attack");
                    return;
                }
            _animator.SetTrigger("Bash Attack");
        } else
            ActionEnded();
    }
    
    private void OnArtifactUnlocked(Artifact type) {
        if (type == Artifact.Spin) lvl2Object.SetActive(true); 
        else if (type == Artifact.Gun) lvl3Object.SetActive(true); 
    }
    
    private void OnDestroy() {
        PlayerData.OnArtifactUnlocked -= OnArtifactUnlocked;
    }

    public void HitboxOn() {
        _hitbox.EnableTrigger(this);
    }

    public void HitboxOff() {
        _hitbox.DisableTrigger();
    }
    
    public override void ProcessHitboxData(Collider collider) {
        if (collider.TryGetComponent<IHittable>(out var hittable)) {
            int damageToDeal = Mathf.RoundToInt(GetDamageValue() * _currentDamageMultiplier);
            hittable.Hit(damageToDeal, ArtifactType);
            if (collider.TryGetComponent<EnemyBase>(out _)) {
                var hitPoint = collider.ClosestPointOnBounds(lvl2Object.transform.position);
                EffectSpawner.SpawnBloodFX(hitPoint);
            }
        }
    }
    
}
