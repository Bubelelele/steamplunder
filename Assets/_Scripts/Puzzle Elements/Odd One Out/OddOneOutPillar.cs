using SpriteGlow;
using UnityEngine;

public class OddOneOutPillar : MonoBehaviour, IInteractable {

    [SerializeField] private SpriteGlowEffect[] symbolSpriteGlows;

    public bool HoldToInteract { get; }
    public void Interact() {
        
    }

    public string GetDescription() {
        return "Touch";
    }

    public string GetKeyText() => null;
}
