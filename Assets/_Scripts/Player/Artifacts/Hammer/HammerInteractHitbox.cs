using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerInteractHitbox : MonoBehaviour {
    
    private List<Transform> _colliders;
    private bool _triggerEnabled;
    
    private void OnEnable() {
        _colliders = new List<Transform>();
        _triggerEnabled = false;
        StartCoroutine(ToggleTrigger());
    }

    private IEnumerator ToggleTrigger() {
        yield return new WaitForSeconds(.3f);
        _triggerEnabled = true;
        yield return new WaitForSeconds(.3f);
        _triggerEnabled = false;
    }

    private void OnTriggerStay(Collider other) {
        if (!_triggerEnabled) return;
        if (other.TryGetComponent<IHittable>(out var hittable)) {
            if (_colliders.Contains(other.transform)) return;
            
            hittable.Hit(0, Artifact.Hammer);
            _colliders.Add(other.transform);
        }
    }
}