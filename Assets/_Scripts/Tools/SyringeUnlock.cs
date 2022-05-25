using UnityEngine;

public class SyringeUnlock : MonoBehaviour {
//TEMP SCRIPT
    private static bool _firstUnlocked;
    private static bool _secondUnlocked;

    [SerializeField] private bool first;
    [SerializeField] private bool second;

    public void UnlockSlot() {
        if (!first && !second) return;
        if (first && _firstUnlocked) return;
        if (second && _secondUnlocked) return;
        if (first) _firstUnlocked = true;
        if (second) _secondUnlocked = true;
        PlayerData.UnlockSyringeSlot();
    }

}
