using UnityEngine;
using UnityEngine.Events;

public class SpinPedestal : MonoBehaviour, IInteractable {

    [SerializeField] private bool canOnlyActivateOnce;
    [SerializeField] private bool canHold;
    [SerializeField] private UnityEvent onActivate;
    [SerializeField] private Animator spinAnim;

    private bool _activated;
    private PlayerMovement _playerMovement;

    private void Start() {
        _playerMovement = Player.GetPlayer().GetComponent<PlayerMovement>();
    }

    public bool HoldToInteract => canHold;

    public void Interact() {
        if (!CheckSpinActive() || _activated) return;
        
        spinAnim.SetTrigger("Press");
        onActivate?.Invoke();
        _playerMovement.FreezeLook();
        if (canOnlyActivateOnce) _activated = true;
    }

    public string GetDescription() {
        string msg = "(Spinning Axe required)";
        if (_activated) msg = "(Cannot be activated again)";
        else if (CheckSpinActive()) msg = "Spin with Axe";

        return msg;
    }

    public string GetKeyText() => null;

    private bool CheckSpinActive() => PlayerData.ArtifactStatus[Artifact.Spin];
}