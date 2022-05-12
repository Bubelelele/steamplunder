using System;
using UnityEngine;

public class InteractionUI : MonoBehaviour {

    [SerializeField] private InteractionIndicator[] interactionIndicators;

    private void Awake() {
        PlayerInteraction.OnSetIndicator += SetIndicator;
        PlayerInteraction.OnIndicatorSetActive += IndicatorSetActive;
    }

    private void IndicatorSetActive(int index, bool state) {
        interactionIndicators[index].gameObject.SetActive(state);
    }

    private void SetIndicator(int index, IInteractable interactable, Vector3 pos, string defaultKeyText) {
        interactionIndicators[index].SetIndicator(interactable, defaultKeyText);
        transform.position = pos;
    }

    private void OnDestroy() {
        PlayerInteraction.OnSetIndicator -= SetIndicator;
        PlayerInteraction.OnIndicatorSetActive -= IndicatorSetActive;
    }
}
