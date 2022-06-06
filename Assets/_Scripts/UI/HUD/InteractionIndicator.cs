using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InteractionIndicator : MonoBehaviour {

    [SerializeField] private TMP_Text interactionText;
    [SerializeField] private Image keyImage;
    [SerializeField] private GameObject holdIndicator;
    [SerializeField] private Sprite[] indicatorSprites;

    public void SetIndicator(IInteractable interactable, string key) {
        interactionText.text = interactable.GetDescription();
        keyImage.sprite = GetKeySprite(interactable.GetKeyText() ?? key);
        holdIndicator.SetActive(interactable.HoldToInteract);
    }
    
    private Sprite GetKeySprite(string key) {
        switch (key) {
            case "Mouse0":
                return indicatorSprites[2];
            case "Mouse1":
                return indicatorSprites[5];
            case "E":
                return indicatorSprites[0];
            case "F":
                return indicatorSprites[1];
            case "Q":
                return indicatorSprites[3];
            default:
                Debug.LogWarning($"Key {key} does not have a sprite set up!");
                return indicatorSprites[0]; //Pass default to not break stuff
        }
    }
    
}