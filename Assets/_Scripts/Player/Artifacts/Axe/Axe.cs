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

    public override void Use() {
        base.Use();
        _animator.SetTrigger("Attack 1");
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

    private void Attack1Ended() {
        if (_queuedTime > 0f) {
            _animator.SetTrigger("Attack 2");
        } else
            ActionEnded();
    }
    
    private void Attack1InterruptCheck() {
        if (_queuedTime > 0f) {
            _animator.SetTrigger("Attack 2");
        }
    }

    private void Attack2Ended() {
        if (_queuedTime > 0f) {
            if (PlayerData.ArtifactStatus[Artifact.Spin])
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
            hittable.Hit(GetDamageValue(), ArtifactType);
        }
    }
    
}
