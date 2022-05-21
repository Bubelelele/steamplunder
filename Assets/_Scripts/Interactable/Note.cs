using System;
using UnityEngine;

public class Note : MonoBehaviour, IInteractable {
    
    [SerializeField] [TextArea] private string textToDisplay;
    
    public static event Action<string> OnDisplayNote;
    
    public bool HoldToInteract { get; }
    public void Interact() {
        OnDisplayNote?.Invoke(textToDisplay);
    }

    public string GetDescription() => "Read";

    public string GetKeyText() => null;
}
