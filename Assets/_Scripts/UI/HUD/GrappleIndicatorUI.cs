using UnityEngine;
using UnityEngine.UI;

public class GrappleIndicatorUI : MonoBehaviour {

    [SerializeField] private Image grappleIndicator;
    [SerializeField] private Color greenColor;
    [SerializeField] private Color redColor;

    private Camera _cam;
    private bool _mouseHovering;
    private bool _canGrapple;

    private void Awake() {
        _cam = Camera.main;
        GrapplePoint.OnStateChanged += OnGrapplePointStateChanged;
        Grapple.OnGrappleStateChanged += OnGrappleStateChanged;
        OnGrappleStateChanged(GrappleState.Idle);
    }

    private void OnDestroy() {
        GrapplePoint.OnStateChanged -= OnGrapplePointStateChanged;
        Grapple.OnGrappleStateChanged -= OnGrappleStateChanged;
    }

    private void Update() {
        if (GrapplePoint.Current != null) {
            grappleIndicator.transform.position = 
                _cam.WorldToScreenPoint(GrapplePoint.Current.transform.position);
            grappleIndicator.color = Grapple.CanGrappleToPoint ? greenColor : redColor;
        }
    }
    
    private void OnGrapplePointStateChanged(bool state) {
        _mouseHovering = state;
        SetActive();
    }
    
    private void OnGrappleStateChanged(GrappleState state) {
        _canGrapple = state is GrappleState.Idle;
        SetActive();
    }
    
    private void SetActive() => grappleIndicator.gameObject.SetActive(_mouseHovering && _canGrapple);

}
