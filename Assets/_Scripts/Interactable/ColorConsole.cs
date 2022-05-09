using UnityEngine;

public class ColorConsole : MonoBehaviour, IInteractable {
    
    private bool hit;
    public bool HoldToInteract { get; }

    public void Interact() {
        GetComponent<MeshRenderer>().material.color = hit ? Color.red : Color.green;

        hit = !hit;
    }

    public string GetDescription() {
        return hit ? "Make cube red" :"Make cube green";
    }

    public string GetKeyText() => null;
}