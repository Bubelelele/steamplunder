using UnityEngine;
using UnityEngine.Events;

public class AnimatorRelay : MonoBehaviour {

    [SerializeField] private string parameter;
    [SerializeField] private Animator animator;

    public void SetBool(bool value) {
        if (animator == null) return;
        animator.SetBool(parameter, value);
    }
    
    public void SetInt(int value) {
        if (animator == null) return;
        animator.SetInteger(parameter, value);
    }
    
    public void SetFloat(float value) {
        if (animator == null) return;
        animator.SetFloat(parameter, value);
    }
}
