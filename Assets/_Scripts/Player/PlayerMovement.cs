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
    private bool _frozenLook;
    private Vector3 _moveVector;

    private void Awake() {
        _cam = Camera.main;
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        CutsceneManager.OnCutscenePlaying += SetFreeze;
        PlayerData.OnDie += Die;

        if (_cam == null) {
            Debug.LogWarning($"{nameof(PlayerMovement)} cannot find a camera!");
            SetFreeze(true);
        }
    }

    private void OnDestroy() {
        CutsceneManager.OnCutscenePlaying -= SetFreeze;
        PlayerData.OnDie -= Die;
    }

    private void FixedUpdate() {
        Vector3 inputVector = PlayerInput.Dir3;
        Vector3 movementVector = GetMovementVector(inputVector);
        _moveVector = movementVector;

        if (_animator != null) {
            var normalizedMovementVector = _frozen ? Vector3.zero : movementVector.normalized;
            float directionX = Vector3.Dot(normalizedMovementVector, transform.right);
            float directionZ = Vector3.Dot(normalizedMovementVector, transform.forward);
            _animator.SetFloat("MoveX", directionX, 0.1f, Time.fixedDeltaTime);
            _animator.SetFloat("MoveZ", directionZ, 0.1f, Time.fixedDeltaTime);
        }

        if (_frozen) return;
        Move(movementVector);

        if (_frozenLook) return;
        if (lookAtMouse) RotateToMouse();
        else RotateToMovement(movementVector);
    }

#if UNITY_EDITOR
    private void Update() {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            var ray = _cam.ScreenPointToRay(Input.mousePosition);
            int layerMask = 1 << 6;
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, layerMask)) {
                transform.position = hit.point;
            }
        }
    }
#endif

    //Rotate to always look at the mouse
    private void RotateToMouse() {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit, maxDistance: 300f, mouseAreaLayer)) {
            LookAt(hit.point);
        }
    }

    public void LookAt(Vector3 lookPos) {
        lookPos.y = transform.position.y;
        transform.LookAt(lookPos);
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

        //Thaw look if playing resumes moving
        if (movementVector.magnitude > .1f) _frozenLook = false;
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

    public void FreezeLook() => _frozenLook = true;

    public bool IsSleeping() => _moveVector == Vector3.zero;

    private void Die(Transform source) {
        SetFreeze(true);
        _animator.SetTrigger("Die");
        GetComponent<PlayerArtifacts>().enabled = false; //Prevent player from zombie attacking
        if (source != null) LookAt(source.position);
    }
    
}