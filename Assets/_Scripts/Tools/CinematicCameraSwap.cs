using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CinematicCameraSwap : MonoBehaviour {

    private CinemachineVirtualCamera _vcam;

    private void Awake() {
        _vcam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Tab))
            _vcam.Priority = 99;
        else if (Input.GetKeyUp(KeyCode.Tab))
            _vcam.Priority = 9;
    }
}
