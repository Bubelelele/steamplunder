using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(VisualEffect))]
public class VisualEffectDestroyer : MonoBehaviour {
    private void Start() => Destroy(gameObject, 1f);
}