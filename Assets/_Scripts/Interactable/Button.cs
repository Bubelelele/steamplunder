using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour, IInteractable {

    [SerializeField] private bool canDoMultiplePress;
    [SerializeField] private float releaseDelay = 1f;
    [SerializeField] private UnityEvent onPressed;
    [SerializeField] private Animator animator;
    
    private bool _isPressed;
    
    public bool HoldToInteract { get; }
    public void Interact() {
        if (_isPressed) return;
        
        _isPressed = true;
        animator.SetBool("IsPressed", true);
        onPressed.Invoke();
        if (canDoMultiplePress) StartCoroutine(Release());
    }

    public string GetDescription() {
        return _isPressed ? "(Pressed)" : "Press";
    }

    public string GetKeyText() => null;

    private IEnumerator Release() {
        yield return new WaitForSeconds(releaseDelay);
        animator.SetBool("IsPressed", false);
        yield return new WaitForSeconds(.2f);
        _isPressed = false;
    }
}
