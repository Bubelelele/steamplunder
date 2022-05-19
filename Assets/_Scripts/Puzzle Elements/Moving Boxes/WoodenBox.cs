using UnityEngine;

public class WoodenBox : Box, IInteractable {
    
    [SerializeField] private string description = "Push";

    public bool HoldToInteract => false;

    public void Interact() {
        AttemptMove();
    }

    public string GetDescription() {
        return description;
    }

    public string GetKeyText() {
        return null;
    }
}