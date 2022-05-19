using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

    public static event Action<int, IInteractable, Vector3, string> OnSetIndicator;
    public static event Action<int, bool> OnIndicatorSetActive;
    
    [SerializeField] private KeyCode defaultInteractionKey = KeyCode.E;
    [SerializeField] private float interactionDistance = 2f;

    private Camera _cam;

    private void Awake() {
        _cam = Camera.main;
    }

    private void LateUpdate() {
        InteractionRay();
    }

    private void InteractionRay() {
        var rayOrigin = transform.position + transform.up * .5f;
        Ray ray = new Ray(rayOrigin, transform.forward);
        bool[] hitSomething = {false, false};

        if (Physics.Raycast(ray, out var hit, interactionDistance)) {
            var interactables = hit.collider.GetComponents<IInteractable>();
            if (interactables.Length > 2) Debug.LogWarning($"{hit.collider.gameObject.name} has more than 2 interactables!");
            
            for (var i = 0; i < interactables.Length; i++) {
                var interactable = interactables[i];
                
                hitSomething[i] = true;
                OnSetIndicator?.Invoke(i, interactable, _cam.WorldToScreenPoint(hit.collider.transform.position),
                    defaultInteractionKey.ToString());

                if (Input.GetKeyDown(defaultInteractionKey)) {
                    interactable.Interact();
                    OnIndicatorSetActive?.Invoke(i, false);
                    return;
                }

                if (interactable.HoldToInteract && Input.GetKey(defaultInteractionKey)) {
                    interactable.Interact();
                }
            }
        }

        for (int i = 0; i < 2; i++) OnIndicatorSetActive?.Invoke(i, hitSomething[i]);
    }
}