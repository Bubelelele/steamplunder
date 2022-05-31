using System;
using Cinemachine;
using UnityEngine;

public class DeathCameraZoom : MonoBehaviour {
    
    private CinemachineVirtualCamera _vcam;
    
    private void Awake() {
        _vcam = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CinemachineVirtualCamera>();
        if (_vcam != null) {
            _vcam.Priority = 12;
        }
    }
}
