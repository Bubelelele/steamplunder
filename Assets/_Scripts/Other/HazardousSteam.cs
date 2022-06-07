using System;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class HazardousSteam : Campfire {
    
    [SerializeField] private float activeTime = 4f;
    [SerializeField] private float cooldownTime = 4f;

    private VisualEffect _vfx;

    private void Start() {
        _vfx = GetComponent<VisualEffect>();
        if (_vfx == null) return;
        StartCoroutine(HandleSteamFlow());
    }

    private IEnumerator HandleSteamFlow() {
        _vfx.Play();
        yield return new WaitForSeconds(activeTime);
        _vfx.Stop();
        yield return new WaitForSeconds(cooldownTime);
        StartCoroutine(HandleSteamFlow());
    }
}
