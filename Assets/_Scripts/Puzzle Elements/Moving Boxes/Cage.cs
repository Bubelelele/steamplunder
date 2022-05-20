using System;
using UnityEngine;

public class Cage : MonoBehaviour {

    private Animator _animator;
    
    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    public void SetOpen(bool state) {
        _animator.SetBool("Open", state);
    }
}
