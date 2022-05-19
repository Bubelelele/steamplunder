using UnityEngine;
using TMPro;

public class InteractionIndicator : MonoBehaviour {

    [SerializeField] private TMP_Text interactionText;
    [SerializeField] private TMP_Text keyText;
    [SerializeField] private GameObject holdIndicator;

    public void SetIndicator(IInteractable interactable, string key) {
        interactionText.text = interactable.GetDescription();
        keyText.text = KeyTextConversion(interactable.GetKeyText() ?? key);
        holdIndicator.SetActive(interactable.HoldToInteract);
    }
    
    private string KeyTextConversion(string key) {
        if (key == "Mouse0") return "LM";
        if (key == "Mouse1") return "RM";
        return key;
    }
    
}