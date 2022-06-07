using UnityEngine;
using UnityEngine.Events;

public class GeneralInteractable : MonoBehaviour, IInteractable {
    
    [SerializeField] private string description;
    [SerializeField] private bool interactOneShot;
    [SerializeField] private UnityEvent onInteract;
    [SerializeField] private UnityEvent onStopInteract;

    private bool _interacting;
    
    public void Interact() {
        if (_interacting) return;
        onInteract.Invoke();
        _interacting = true;
        if (interactOneShot) Destroy(this);
    }

    public bool HoldToInteract { get; }
    public string GetDescription() => description;
    public string GetKeyText() => null;

    private void Update() {
        if (_interacting && Input.GetKeyDown(KeyCode.E)) {
            onStopInteract.Invoke();
            Invoke(nameof(ResetInteracting), .2f);
        }
    }

    private void ResetInteracting() => _interacting = false;
}
