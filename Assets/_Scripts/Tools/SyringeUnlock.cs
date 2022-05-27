using UnityEngine;

public class SyringeUnlock : MonoBehaviour {

    private bool _used;

    public void UnlockSlot() {
        if (_used) return;
        _used = true;
        PlayerData.UnlockSyringeSlot();
    }

}
