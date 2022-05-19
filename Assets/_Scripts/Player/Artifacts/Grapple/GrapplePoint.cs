using System;
using UnityEngine;

public class GrapplePoint : MonoBehaviour {

    public static event Action<bool> OnStateChanged;

    public static GrapplePoint Current { get; private set; }

    private void OnMouseEnter() {
        Current = this;
        OnStateChanged?.Invoke(true);
    }

    protected void OnMouseExit() {
        if (Current == this) Current = null;
        OnStateChanged?.Invoke(false);
    }

    private void OnDestroy() => OnStateChanged?.Invoke(false);
}
