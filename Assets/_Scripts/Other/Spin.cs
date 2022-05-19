using UnityEngine;

public class Spin : MonoBehaviour {
    public float speed = 1f;
    
    [SerializeField] private bool clockwise = true;

    private float _dir;

    private void Awake() {
        _dir = clockwise ? -1 : 1;
    }

    private void FixedUpdate() {
        transform.Rotate(Vector3.up, speed * _dir);
    }
}