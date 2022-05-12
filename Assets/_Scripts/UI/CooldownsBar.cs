using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownsBar : MonoBehaviour {

    [SerializeField] private Image axeCooldownUI;
    [SerializeField] private Image hammerCooldownUI;
    [SerializeField] private Image grappleCooldownUI;
    [SerializeField] private Image gunCooldownUI;
    [SerializeField] private Image steamerCooldownUI;

    private float _axeDelta;
    private float _hammerDelta;
    private float _grappleDelta;
    private float _gunDelta;
    private float _steamerDelta;
    
    private void Update() {
        axeCooldownUI.fillAmount = Mathf.MoveTowards(axeCooldownUI.fillAmount, 0, _axeDelta * Time.deltaTime);
        hammerCooldownUI.fillAmount = Mathf.MoveTowards(hammerCooldownUI.fillAmount, 0, _hammerDelta * Time.deltaTime);
        grappleCooldownUI.fillAmount = Mathf.MoveTowards(grappleCooldownUI.fillAmount, 0, _grappleDelta * Time.deltaTime);
        gunCooldownUI.fillAmount = Mathf.MoveTowards(gunCooldownUI.fillAmount, 0, _gunDelta * Time.deltaTime);
        steamerCooldownUI.fillAmount = Mathf.MoveTowards(steamerCooldownUI.fillAmount, 0, _steamerDelta * Time.deltaTime);
    }

    public void AxeCooldown(float time) {
        _axeDelta = 1 / time;
        axeCooldownUI.fillAmount = 1f;
    }
    
    public void HammerCooldown(float time) {
        _hammerDelta = 1 / time;
        hammerCooldownUI.fillAmount = 1f;
    }
    
    public void GrappleCooldown(float time) {
        _grappleDelta = 1 / time;
        grappleCooldownUI.fillAmount = 1f;
    }
    
    public void GunCooldown(float time) {
        _gunDelta = 1 / time;
        gunCooldownUI.fillAmount = 1f;
    }
    
    public void SteamerCooldown(float time) {
        _steamerDelta = 1 / time;
        steamerCooldownUI.fillAmount = 1f;
    }

    public void ToggleAxeUI(bool active) => axeCooldownUI.transform.parent.gameObject.SetActive(active);
    public void ToggleHammerUI(bool active) => hammerCooldownUI.transform.parent.gameObject.SetActive(active);
    public void ToggleGunUI(bool active) => gunCooldownUI.transform.parent.gameObject.SetActive(active);
    public void ToggleSteamerUI(bool active) => steamerCooldownUI.transform.parent.gameObject.SetActive(active);
    public void ToggleGrappleUI(bool active) => grappleCooldownUI.transform.parent.gameObject.SetActive(active);
    
}
