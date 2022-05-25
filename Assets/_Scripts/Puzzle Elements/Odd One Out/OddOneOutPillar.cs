using System;
using System.Collections.Generic;
using SpriteGlow;
using UnityEngine;

public class OddOneOutPillar : MonoBehaviour, IInteractable {

    [SerializeField] private SpriteRenderer[] symbolSpriteRenderers;

    private List<SpriteGlowEffect> _spriteGlows = new();

    private void Awake() {
        foreach (var sprite in symbolSpriteRenderers) {
            _spriteGlows.Add(sprite.GetComponent<SpriteGlowEffect>());
        }
    }

    private void Start() {
        Glow(false);
    }

    public bool HoldToInteract { get; }
    
    public void Interact() {
        
    }

    public string GetDescription() => "Touch";

    public string GetKeyText() => null;

    public void SetSymbolSprite(Sprite sprite, int rowIndex) {
        symbolSpriteRenderers[rowIndex].sprite = sprite;
    }

    public void Glow(bool glow) {
        foreach (var spriteGlow in _spriteGlows) {
            spriteGlow.enabled = glow;
        }
    }
}
