using UnityEngine;

public class HudState : MonoBehaviour {

    public void SetActive(bool state) {
        GameCanvas.SetHudActive(state);
    }

}
