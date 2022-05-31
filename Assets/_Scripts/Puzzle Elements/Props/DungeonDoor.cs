using System;
using UnityEngine;

public class DungeonDoor : MonoBehaviour {

    private Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    public void Open() {
        _animator.SetTrigger("Open");
    }

    public void Close() {
        _animator.SetTrigger("Close");
    }
}
