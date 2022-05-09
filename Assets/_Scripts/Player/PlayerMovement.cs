using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [Header("Settings")]
    [SerializeField] private bool lookAtMouse;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnSpeed = 15f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask mouseAreaLayer;

    private Camera _cam;
    private Rigidbody _rb;
    private Animator _animator;
    private bool _frozen;
    
    private void Awake() {
        _cam = Camera.main;
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        if (_cam == null) {
            Debug.LogWarning($"{nameof(PlayerMovement)} cannot find a camera!");
            SetFreeze(true);
        }
    }

    private void FixedUpdate() {
        if (_frozen) return;
        
        Vector3 inputVector = PlayerInput.Dir3;
        Vector3 movementVector = GetMovementVector(inputVector);
        Move(movementVector);
        if (_animator != null) _animator.SetFloat("Movement", movementVector.magnitude);
        
        if (lookAtMouse || movementVector.magnitude < .1f) RotateToMouse();
        else RotateToMovement(movementVector);
    }

    //Rotate to always look at the mouse
    private void RotateToMouse() {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance: 300f, mouseAreaLayer)) {
            Vector3 target = hit.point;
            target.y = transform.position.y;
            
            transform.LookAt(target);
        }
    }

    //Rotate to the direction of the player's movement
    private void RotateToMovement(Vector3 movementVector) {
        if (movementVector.magnitude == 0) return;
        
        Quaternion rotation = Quaternion.LookRotation(movementVector);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, turnSpeed);
    }

    //Move the the player transform by movementVector
    private void Move(Vector3 movementVector) {
        float speed = moveSpeed * Time.deltaTime;

        Vector3 targetPosition = transform.position + movementVector * speed;
        transform.position = targetPosition;
    }

    //Get our movementVector by offsetting our inputVector by the camera's y rotation
    private Vector3 GetMovementVector(Vector3 inputVector) {
        Vector3 movementVector = Quaternion.Euler(0, _cam.transform.eulerAngles.y, 0) * inputVector;
        movementVector = AdjustVelocityToSlope(movementVector);
        return movementVector;
    }

    //Fixes bounces while walking down a slope. Ground beneath has to be in the ground layer
    private Vector3 AdjustVelocityToSlope(Vector3 velocity) {
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, 1.1f, groundLayer)) {
            Quaternion slopeRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            Vector3 adjustedVelocity = slopeRotation * velocity;

            if (adjustedVelocity.y < 0) return adjustedVelocity;
        }

        return velocity;
    }

    public void SetFreeze(bool freeze) {
        _frozen = freeze;
        _rb.isKinematic = freeze;
    }
    
}