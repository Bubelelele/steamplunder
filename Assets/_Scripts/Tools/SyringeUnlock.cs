using System;
using UnityEngine;

public class SyringeUnlock : MonoBehaviour {

    [SerializeField] private bool unlockOnEnable;

    private bool _used;

    private void OnEnable() {
        if (unlockOnEnable) UnlockSlot();
    }

    public void UnlockSlot() {
        if (_used) return;
        _used = true;
        PlayerData.UnlockSyringeSlot();
    }

}
