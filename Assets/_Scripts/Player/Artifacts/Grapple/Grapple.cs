using System;
using UnityEngine;

public class Grapple : ArtifactBase {

    [SerializeField] private float grappleRange = 8f;
    [SerializeField] private float maxHeightDifference = 3.5f;
    [SerializeField] private float extendSpeed = 10f;
    [SerializeField] private float retractSpeed = 8f;
    [SerializeField] private float flyingSpeed = 5f;
    [SerializeField] private float howCloseBeforeDetach = 1f;

    [SerializeField] private Transform clawTransform;
    [SerializeField] private Transform clawGearTransform;
    [SerializeField] private Transform tipTransform;
    [SerializeField] private LineRenderer lineRenderer;
    
    public static bool CanGrappleToPoint { get; private set; }
    public static event Action<GrappleState> OnGrappleStateChanged;

    private float _sqrGrappleRange;
    private PlayerMovement _playerMovement;
    private GrappleState _currentGrappleState;
    private GrapplePoint _whichPointToGrapple;
    private Transform _lerpTo;
    private Vector3 _lerpToLocal;
    private Vector3 _clawLocalOrigin;

    private bool IsGrappling => _currentGrappleState != GrappleState.Idle;
    
    protected override void Awake() {
        base.Awake();
        _sqrGrappleRange = Mathf.Pow(grappleRange, 2);
        _playerMovement = GetComponent<PlayerMovement>();
        _clawLocalOrigin = clawTransform.localPosition;
    }

    private void Update() {
        CanGrappleToPoint = CheckCanGrappleToPoint();

        if (_currentGrappleState == GrappleState.Extending) {
            clawTransform.position =
                Vector3.MoveTowards(clawTransform.position, _lerpTo.position, extendSpeed * Time.deltaTime);
            if (clawTransform.position == _lerpTo.position) Extended();
        } else if (_currentGrappleState == GrappleState.Flying) {
            _playerMovement.LookAt(_lerpTo.position);
            transform.position =
                Vector3.MoveTowards(transform.position, _lerpTo.position, flyingSpeed * Time.deltaTime);
            clawTransform.position = _lerpTo.position;
            if (Vector3.Distance(_lerpTo.position, transform.position) < howCloseBeforeDetach) StopFlying();
        } else if (_currentGrappleState == GrappleState.Retracting) {
            clawTransform.localPosition =
                Vector3.MoveTowards(clawTransform.localPosition, _lerpToLocal, retractSpeed * Time.deltaTime);
            if (clawTransform.localPosition == _lerpToLocal) Retracted();
        }
    }

    private void LateUpdate() {
        if (IsGrappling) DrawRope();
    }

    public override void Use() {
        if (GrapplePoint.Current == null || !CanGrappleToPoint) {
            InvokeOnActionFinished();
            return;
        }
        
        StartGrapple();
        base.Use();
    }
    
    private void StartGrapple() {
        _whichPointToGrapple = GrapplePoint.Current;
        _lerpTo = _whichPointToGrapple.transform;
        lineRenderer.positionCount = 0; //Reset LineRenderer
        _playerMovement.SetFreeze(true);
        _playerMovement.LookAt(_lerpTo.position);
        UpdateState(GrappleState.WaitingToShoot);
        _animator.SetTrigger("Grapple");
    }

    private bool CheckCanGrappleToPoint() {
        if (GrapplePoint.Current == null || !PlayerData.ArtifactStatus[Artifact.Grapple]) return false;
        
        var grapplePointPos = GrapplePoint.Current.transform.position;
        var vectorToGrapplePoint = grapplePointPos - transform.position;
        
        //Use squared values to avoid sqrt calculation
        var sqrDistance = vectorToGrapplePoint.sqrMagnitude;
        bool inRange = sqrDistance < _sqrGrappleRange;
        bool inHeight = vectorToGrapplePoint.y <= maxHeightDifference;
        return inRange && inHeight && InClearPath(vectorToGrapplePoint);
    }

    private bool InClearPath(Vector3 vectorToGrapplePoint) {
        Ray ray = new Ray(transform.position, vectorToGrapplePoint);
        int layerMask = 1 | 1 << 6; //bit shifting. It will look at layers 1 (default) and 6 (ground)
        
        Debug.DrawLine(transform.position, GrapplePoint.Current.transform.position);
        if (Physics.Raycast(ray, out var hit, grappleRange, layerMask)) {
            if (hit.collider.TryGetComponent<GrapplePoint>(out _)) return true;
        }
        return false;
    }

    private void DrawRope() {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, tipTransform.position);
        lineRenderer.SetPosition(1, clawGearTransform.position);
    }
    
    private void Shoot() {
        if (_currentGrappleState != GrappleState.WaitingToShoot) return;
        Debug.Log("SHOOT");
        AudioManager.PlayAudio(AudioType.GrappleShoot_Player);
        UpdateState(GrappleState.Extending);
    }

    private void Extended() {
        Debug.Log("HIT POINT");

        //evaluate which type of grapple point we hit
        if (_whichPointToGrapple.TryGetComponent<GrappleLever>(out var grappleLever)) {
            //Grapple lever
            Debug.Log("GRAPPLING LEVER");
            UpdateState(GrappleState.Retracting);
            _lerpToLocal = _clawLocalOrigin;
            grappleLever.PullLever();
        } else {
            //Normal grapple to point
            Debug.Log("GRAPPLE FLY");
            _animator.SetBool("Flying", true);
            UpdateState(GrappleState.Flying);
        }
        
    }

    private void StopFlying() {
        Debug.Log("GRAPPLE FLY STOP");
        _animator.SetBool("Flying", false);
        _playerMovement.SetFreeze(false);
        ActionEnded();
    }

    private void Retracted() {
        Debug.Log("GRAPPLE RETRACTED");
        _animator.SetTrigger("Detach");
        _playerMovement.SetFreeze(false);
        ActionEnded();
    }

    protected override void ActionEnded() {
        base.ActionEnded();
        clawTransform.localPosition = _clawLocalOrigin; //Reset claw position
        UpdateState(GrappleState.Idle);
        if (_whichPointToGrapple != null && _whichPointToGrapple.TryGetComponent<EnemyBase>(out _)) {
            //temp code for grapple and hit enemy
            GetComponent<PlayerArtifacts>().UseArtifact(GetComponent<Axe>());
        }
        _whichPointToGrapple = null;
    }

    private void UpdateState(GrappleState state) {
        _currentGrappleState = state;
        OnGrappleStateChanged?.Invoke(state);
    }

}

public enum GrappleState {
    Idle,
    WaitingToShoot,
    Extending,
    Retracting,
    Flying
}
