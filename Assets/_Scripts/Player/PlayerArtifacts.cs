using System.Collections.Generic;
using UnityEngine;

public class PlayerArtifacts : MonoBehaviour {

    [SerializeField] private List<Artifact> startAsUnlockedOverride;

    public static Dictionary<Artifact, KeyCode> ArtifactKeyCodes { get; private set; }
    
    private ArtifactBase[] _artifacts;
    private bool _actionOngoing;

    private void Awake() {
        _artifacts = GetComponents<ArtifactBase>();

        foreach (var artifact in _artifacts) {
            artifact.OnActionFinished += ArtifactActionEnded;
            //Debug.Log($"Found {artifact.GetType()}");
        }
        
        if (ArtifactKeyCodes == null) {
            ArtifactKeyCodes = new();
            foreach (var artifact in _artifacts) {
                ArtifactKeyCodes.Add(artifact.ArtifactType, artifact.InputKey);
            }
        }
    }

    private void Start() {
        foreach (var artifact in startAsUnlockedOverride) {
            PlayerData.UnlockArtifact(artifact);
        }
    }

    private void Update() {
        if (_actionOngoing) return;
        
        foreach (var artifact in _artifacts) {
            if (artifact.CheckInput()) UseArtifact(artifact);
        }
    }

    public void UseArtifact(ArtifactBase artifact) {
        _actionOngoing = true;
        artifact.Use();
    }

    private void ArtifactActionEnded() {
        _actionOngoing = false;
    }
    
    
}
