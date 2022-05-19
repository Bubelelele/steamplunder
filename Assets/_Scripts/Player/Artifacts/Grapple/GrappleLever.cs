using UnityEngine;
using UnityEngine.Events;

public class GrappleLever : GrapplePoint {
    
    [SerializeField] private UnityEvent onLeverPulled;
    [SerializeField] private Animator animator;

    public void PullLever() {
        onLeverPulled.Invoke();
        OnMouseExit();
        animator?.SetTrigger("Pull");
        Destroy(this);
    }

}
