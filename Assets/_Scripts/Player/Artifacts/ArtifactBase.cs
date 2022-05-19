using System;
using System.Collections;
using UnityEngine;

public abstract class ArtifactBase : MonoBehaviour {

    [field: SerializeField] public Artifact ArtifactType { get; private set; }
    [field: SerializeField] public KeyCode InputKey { get; private set; }
    [SerializeField] protected int damage;
    [SerializeField] protected float cooldown;
    [SerializeField] protected GameObject artifactObject;
    
    protected bool _ready = true;
    protected Animator _animator;
    
    public event Action OnActionFinished;

    protected virtual void Awake() => _animator = GetComponent<Animator>();

    public bool CheckInput() {
        bool canUse = PlayerData.ArtifactStatus[ArtifactType] && _ready;
        return Input.GetKeyDown(InputKey) && canUse;
    }
    
    private IEnumerator AttackCooldown() {
        //CooldownBar.OnCooldownStarted.Invoke(cooldown);
        yield return new WaitForSeconds(cooldown);
        _ready = true;
    }

    public virtual void Use() {
        _ready = false;
        artifactObject.SetActive(true);
    }

    protected virtual void ActionEnded() {
        artifactObject.SetActive(false);
        OnActionFinished?.Invoke();
        StartCoroutine(AttackCooldown());
        _animator.SetTrigger("Action Ended");
    }
    
    protected void InvokeOnActionFinished() => OnActionFinished?.Invoke();
}
