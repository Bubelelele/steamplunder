using System;
using TMPro;
using UnityEngine;

public class ArtifactUnlockUI : MonoBehaviour {

    [SerializeField] private TMP_Text unlockText;
    [SerializeField] private Animator animator;

    private void Start() {
        PlayerData.OnArtifactUnlocked += OnArtifactUnlocked;
        //if bugs arise, let it wait a bit before allowing the unlock animation to play
    }

    private void OnDestroy() {
        PlayerData.OnArtifactUnlocked -= OnArtifactUnlocked;
    }

    private void OnArtifactUnlocked(Artifact type) {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1) return;
        unlockText.text = type.ToString();
        animator.SetTrigger("play");
    }
    
    
}
