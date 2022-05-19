public interface IInteractable {
    
    public bool HoldToInteract { get; }
    
    public void Interact();
    
    public string GetDescription();

    public string GetKeyText();

}