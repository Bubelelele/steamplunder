using System;
using UnityEngine;

public class ResetCogIndicator : MonoBehaviour {

    private Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    public void Reset() {
        _animator.SetTrigger("Reset");
        PlayerData.ResetCogCount();
    }
}
