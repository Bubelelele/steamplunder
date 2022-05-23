using UnityEngine;

public class BillboardFX : MonoBehaviour {
    
    private Transform _camTransform;
    private Quaternion _originalRotation;

    void Start() {
        var cam = Camera.main;
        _camTransform = cam.transform;
        GetComponent<Canvas>().worldCamera = cam;
        _originalRotation = transform.rotation;
    }

    void Update() {
        transform.rotation = _camTransform.rotation * _originalRotation;
    }
}