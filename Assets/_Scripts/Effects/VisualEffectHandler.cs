using System;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(VisualEffect))]
public class VisualEffectHandler : MonoBehaviour {
    public Transform follow;
    
    private void Start() => Destroy(gameObject, 1f);

    private void Update() {
        if (follow != null) {
            transform.position = follow.position;
            transform.rotation = follow.rotation;
        }
    }
}