using UnityEngine;

public class ArtifactUnlock : MonoBehaviour {

    [SerializeField] private Artifact artifactToUnlock;

    public void UnlockArtifact() => PlayerData.UnlockArtifact(artifactToUnlock);
}
