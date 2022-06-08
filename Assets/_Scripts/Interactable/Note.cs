using System;
using UnityEngine;

public class Note : MonoBehaviour, IInteractable {
    
    [SerializeField] [TextArea] private string textToDisplay;
    [SerializeField] private NoteType noteType = NoteType.Mystery;
    
    public static event Action<string, NoteType> OnDisplayNote;
    
    public bool HoldToInteract { get; }
    public void Interact() {
        OnDisplayNote?.Invoke(textToDisplay, noteType);
    }

    public string GetDescription() => "Read";

    public string GetKeyText() => null;
}

public enum NoteType {
    Mystery,
    Paper
}
