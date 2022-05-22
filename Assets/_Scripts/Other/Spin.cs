using UnityEngine;

public class Spin : MonoBehaviour {
    
    [SerializeField] private Vector3 rotationAxis = Vector3.up;
    [SerializeField] private float speed = 1f;
    [SerializeField] private bool clockwise = true;

    private void FixedUpdate() {
        float dir = clockwise ? -1 : 1;
        transform.Rotate(rotationAxis, speed * dir);
    }
}