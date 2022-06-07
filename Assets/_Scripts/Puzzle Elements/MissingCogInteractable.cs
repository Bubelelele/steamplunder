using UnityEngine;
using UnityEngine.Events;

public class MissingCogInteractable : MonoBehaviour, IInteractable {
    
    [SerializeField] private UnityEvent onCogPlaced;
    [SerializeField] private GameObject cogCompleted;
    [SerializeField] private GameObject cogIndicator;

    private bool _cogPickedUp;
    private Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    public void CogPickedUp() {
        _cogPickedUp = true;
        cogIndicator.SetActive(true);
    }

    public bool HoldToInteract { get; }
    public void Interact() {
        if (!_cogPickedUp) return;
        
        _animator.SetTrigger("Completed");
        cogCompleted.SetActive(true);
        cogIndicator.SetActive(false);
        onCogPlaced.Invoke();
        Destroy(this);
    }

    public string GetDescription() => _cogPickedUp ? "Place Cog" : "(Cog Missing)";

    public string GetKeyText() => null;
}
