using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class HazardousSteam : Campfire {
    
    [SerializeField] private float activeTime = 4f;
    [SerializeField] private float cooldownTime = 4f;

    private VisualEffect _vfx;
    private Collider _triggerCollider;

    private void Start() {
        _vfx = GetComponent<VisualEffect>();
        _triggerCollider = GetComponent<Collider>();
        if (_vfx == null) return;
        StartCoroutine(HandleSteamFlow());
    }

    private IEnumerator HandleSteamFlow() {
        _vfx.Play();
        _triggerCollider.enabled = true;
        yield return new WaitForSeconds(activeTime);
        _vfx.Stop();
        _triggerCollider.enabled = false;
        yield return new WaitForSeconds(cooldownTime);
        StartCoroutine(HandleSteamFlow());
    }
}
