using UnityEngine;

public class HotbarUI : MonoBehaviour {

    [SerializeField] private GameObject hammerElement;
    [SerializeField] private GameObject grappleElement;
    [SerializeField] private GameObject steamerElement;
    [SerializeField] private GameObject axeElement;
    [SerializeField] private GameObject gunElement;

    private void Awake() {
        PlayerData.OnArtifactUnlocked += OnArtifactUnlocked;
    }

    private void OnDestroy() {
        PlayerData.OnArtifactUnlocked -= OnArtifactUnlocked;
    }

    private void OnArtifactUnlocked(Artifact artifact) {
        if (artifact == Artifact.Axe)
            axeElement.SetActive(true);
        else if (artifact == Artifact.Gun)
            gunElement.SetActive(true);
        else if (artifact == Artifact.Hammer)
            hammerElement.SetActive(true);
        else if (artifact == Artifact.Grapple)
            grappleElement.SetActive(true);
        else if (artifact == Artifact.Steamer) 
            steamerElement.SetActive(true);
    }
}
