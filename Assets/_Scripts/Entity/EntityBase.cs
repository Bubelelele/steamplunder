using UnityEngine;

public abstract class EntityBase : MonoBehaviour, IHittable {

    [SerializeField] protected int maxHealth = 1;
    
    protected int _health;

    protected virtual void Awake() {
        _health = maxHealth;
    }

    public virtual void Hit(int damage, Artifact source) {
        _health -= damage;
        if (_health <= 0) Die();
    }

    protected abstract void Die();
}
