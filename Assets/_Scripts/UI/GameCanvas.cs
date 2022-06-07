using System;
using UnityEngine;

public class GameCanvas : MonoBehaviour {

    [SerializeField] private GameObject hud;

    private static GameCanvas _currentGameCanvas;

    private void Awake() {
        _currentGameCanvas = this;
    }

    public static void SetHudActive(bool state) {
        _currentGameCanvas.hud.SetActive(state);
    }

}
