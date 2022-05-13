using System.Collections.Generic;
using UnityEngine;

public abstract class HitboxBase : MonoBehaviour {
    
    private List<Transform> _colliders;
    private bool _triggerEnabled;
    private WeaponBase _weapon;
    
    private void OnEnable() {
        _colliders = new List<Transform>();
        _triggerEnabled = false;
    }

    private void OnTriggerStay(Collider other) {
        if (!_triggerEnabled) return;
        if (other.TryGetComponent<EntityBase>(out _)) {
            if (_colliders.Contains(other.transform)) return;
            
            _colliders.Add(other.transform);
        }
    }

    public void EnableTrigger(WeaponBase requester) {
        _weapon = requester;
        _triggerEnabled = true;
        Invoke(nameof(SendColliders), .05f);
    }

    private void SendColliders() {
        var collider = GetComponent<SphereCollider>();
        //if (_weapon != null) _weapon.ProcessHitboxData(_colliders, transform.position + collider.center, collider.radius); 
    }

}
