using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class EnemyBase : EntityBase {
    
    [SerializeField] private Healthbar healthbar;
    
    protected NavMeshAgent _navMeshAgent;

    protected override void Awake() {
        base.Awake();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        healthbar?.UpdateHealthbar(_health, maxHealth);
        EnterState(State.Idle);
    }

    public override void Hit(int damage, Artifact source) {
        base.Hit(damage, source);
        healthbar?.UpdateHealthbar(_health, maxHealth);
    }

    protected override void Die() {
        Destroy(gameObject);
    }

    public virtual void Stun() {
        //default stun
    }

    #region AI State Machine

    public enum State {
        Idle,
        Chase,
        Attack,
        Stun
    }

    public State CurrentState { get; private set; }

    protected void EnterState(State newState) {
        if (CurrentState == newState) return;
        CurrentState = newState;
        
        if (CurrentState == State.Idle)
            EnterIdle();
        else if (CurrentState == State.Chase)
            EnterChase();
        else if (CurrentState == State.Attack)
            EnterAttack();
        else if (CurrentState == State.Stun) 
            EnterStun();
    }

    private void Update() {
        if (CurrentState == State.Idle)
            UpdateIdle();
        else if (CurrentState == State.Chase)
            UpdateChase();
        else if (CurrentState == State.Attack)
            UpdateAttack();
        else if (CurrentState == State.Stun) 
            UpdateStun();
    }

    protected virtual void UpdateIdle() { }
    protected virtual void UpdateChase() { }
    protected virtual void UpdateAttack() { }
    protected virtual void UpdateStun() { }
    
    protected virtual void EnterIdle() { }
    protected virtual void EnterChase() { }
    protected virtual void EnterAttack() { }
    protected virtual void EnterStun() { }

    #endregion

}


