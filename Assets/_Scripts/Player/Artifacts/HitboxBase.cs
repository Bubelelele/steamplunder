using System.Collections.Generic;
using UnityEngine;

public abstract class HitboxBase : MonoBehaviour {
    
    protected List<Collider> _colliders;
    protected bool _triggerEnabled;
    protected ArtifactWeaponBase _artifact;
    
    private void OnEnable() => ClearTrigger();
    
    public virtual void EnableTrigger(ArtifactWeaponBase requester) {
        _artifact = requester;
        _triggerEnabled = true;
    }
    
    public void DisableTrigger() {
        ClearTrigger();
    }

    protected void CheckCollider(Collider other) {
        if (_colliders.Contains(other)) return;
            
        _colliders.Add(other);
        if (_artifact != null) _artifact.ProcessHitboxData(other);
    }

    private void ClearTrigger() {
        _triggerEnabled = false;
        _colliders = new List<Collider>();
    }

}
