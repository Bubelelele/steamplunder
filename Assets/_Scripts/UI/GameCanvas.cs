using UnityEngine;

public class GameCanvas : MonoBehaviour {

    [SerializeField] private GameObject hud;

    private static GameCanvas _currentGameCanvas;

    private void Awake() {
        _currentGameCanvas = this;
    }

    public static void SetHudActive(bool state) {
        if (_currentGameCanvas.hud == null) {
            Debug.LogWarning("init fail");
            return;
        }
        _currentGameCanvas.hud.SetActive(state);
    }

}
