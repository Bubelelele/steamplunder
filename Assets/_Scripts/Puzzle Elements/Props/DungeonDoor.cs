using System;
using UnityEngine;

public class DungeonDoor : MonoBehaviour {

    private Animator _animator;
    private BoxCollider _collider;

    private void Awake() {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider>();
    }

    public void Open() {
        _animator.SetTrigger("Open");
        _collider.enabled = false;
    }
}
