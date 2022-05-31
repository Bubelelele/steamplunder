using UnityEngine;
using UnityEngine.Events;

public class AnimationEvent : MonoBehaviour
{
    public UnityEvent calledFromAnimation;

    private void CalledFromAnimation()
    {
        calledFromAnimation.Invoke();
    }
}
