using System;
using System.Collections.Generic;
using UnityEngine;

public class HammerHitbox : MonoBehaviour {
    
    private List<Transform> _colliders;
    private bool _triggerEnabled;
    private Hammer _hammer;
    
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

    public void EnableTrigger(Hammer requester) {
        _hammer = requester;
        _triggerEnabled = true;
        Invoke(nameof(SendColliders), .05f);
    }

    private void SendColliders() {
        var collider = GetComponent<SphereCollider>();
        if (_hammer != null) _hammer.ProcessHitboxData(_colliders, transform.position + collider.center, collider.radius); 
    }
}
