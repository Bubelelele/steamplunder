using UnityEngine;
using UnityEngine.Events;

public class ArtifactCollider : MonoBehaviour, IInteractable, IHittable {

    [SerializeField] private Artifact artifact;
    [SerializeField] private UnityEvent onArtifactHit;
    [SerializeField] private string descriptionOverride;

    public bool HoldToInteract { get; }
    public void Interact() { }

    public string GetDescription() {
        string hasArtifact = descriptionOverride != "" ? descriptionOverride : $"Use {artifact}";
        string hasNotArtifact = $"(Requires {artifact})";
        return PlayerData.ArtifactStatus[artifact] ? hasArtifact : hasNotArtifact;
    }

    public string GetKeyText() => PlayerArtifacts.ArtifactKeyCodes[artifact].ToString();

    public void Hit(int damage, Artifact source) {
        if (source == artifact) onArtifactHit.Invoke();
    }
}
