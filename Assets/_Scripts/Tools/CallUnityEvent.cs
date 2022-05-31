using UnityEngine;
using UnityEngine.Events;

public class CallUnityEvent : MonoBehaviour {
    [SerializeField] private UnityEvent onCall;

    public void Call() {
        onCall.Invoke();
    }
}