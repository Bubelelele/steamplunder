using System;
using UnityEngine;

public class OddOneOutPillar : MonoBehaviour, IInteractable {

    [SerializeField] private SpriteRenderer[] symbolSpriteRenderers;
    [SerializeField] private Animator[] symbolAnimators;
    [SerializeField] private AudioSource localAudioSource;

    public event Action<OddOneOutPillar> OnPillarTouched;

    private bool _active;

    public bool HoldToInteract { get; }
    
    public void Interact() {
        if (_active) return;
        _active = true;
        Glow(true);
        OnPillarTouched?.Invoke(this);
    }

    public string GetDescription() => _active ? "(Activated)" : "Touch";

    public string GetKeyText() => null;

    public void SetSymbolSprite(Sprite sprite, int rowIndex) {
        symbolSpriteRenderers[rowIndex].sprite = sprite;
    }

    public void Deactivate() {
        _active = false;
        Glow(false);
    }

    public void PlaySound(AudioClip clip) {
        localAudioSource.clip = clip;
        localAudioSource.Play();
    }
    
    private void Glow(bool glow) {
        foreach (var animator in symbolAnimators) {
            if (!glow) {
                animator.SetTrigger("Deactivate");
            }
            animator.SetBool("Glow", glow);
        }
    }
}
