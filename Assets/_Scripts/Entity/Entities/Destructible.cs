using UnityEngine;
using UnityEngine.Events;

public class Destructible : EntityBase {
    [SerializeField] private UnityEvent onDestroy;
    
    protected override void Die() {
        onDestroy.Invoke();
    }
}
