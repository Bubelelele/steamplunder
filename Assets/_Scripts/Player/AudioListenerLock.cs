using UnityEngine;

public class AudioListenerLock : MonoBehaviour {

    private Camera _cam;

    private void Awake() {
        _cam = Camera.main;
    }

    private void LateUpdate() {
        //Follow rotation of camera, but only the Y-axis
        transform.rotation = Quaternion.Euler(0, _cam.transform.eulerAngles.y, 0);
    }
}
