using UnityEngine;

public class HoldConsole : MonoBehaviour, IInteractable {

    [SerializeField] private bool holdToInteract;
    [SerializeField] private Transform objectToRotate;

    public bool HoldToInteract => holdToInteract;

    public void Interact() {
        objectToRotate.Rotate(Vector3.up, 1f);
    }

    public string GetDescription() => "Rotate thing";

    public string GetKeyText() => null;
}
