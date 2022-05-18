using System.Collections.Generic;
using UnityEngine;

public class AxeHitbox : MonoBehaviour {

    private List<IHittable> _entities;
    private bool _triggerEnabled;
    private Axe _artifact;

    public void SetArtifact(Axe artifact) => _artifact = artifact;
    
    private void OnEnable() {
        _entities = new List<IHittable>();
        _triggerEnabled = false;
        Invoke(nameof(EnableTrigger), .2f);
    }

    private void EnableTrigger() => _triggerEnabled = true;

    private void OnTriggerStay(Collider other) {
        if (_artifact == null) {
            Debug.LogWarning($"Artifact connection missing on: {gameObject.name}");
            return;
        }
        if (!_triggerEnabled) return;
        if (other.TryGetComponent<IHittable>(out var hittable)) {
            if (_entities.Contains(hittable)) return;
            
            hittable.Hit(_artifact.GetDamageValue(), Artifact.Axe);
            _entities.Add(hittable);
        }
    }
}
