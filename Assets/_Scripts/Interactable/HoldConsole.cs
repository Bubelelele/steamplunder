using UnityEngine;
using UnityEngine.Events;

public class HoldConsole : MonoBehaviour, IInteractable {
    
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private Transform objectToRotate;
    [SerializeField] private UnityEvent onRotationTick;

    public bool HoldToInteract => true;

    public void Interact() {
        objectToRotate.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        onRotationTick.Invoke();
    }

    public string GetDescription() => "Rotate thing";

    public string GetKeyText() => null;
}
