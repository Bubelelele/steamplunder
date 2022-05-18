using UnityEngine;

public abstract class EnemyBase : EntityBase {
    
    [SerializeField] private Healthbar healthbar;

    protected override void Awake() {
        base.Awake();
        healthbar?.UpdateHealthbar(_health, maxHealth);
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

    //attack?
    //navmesh?
    //ai?

}
