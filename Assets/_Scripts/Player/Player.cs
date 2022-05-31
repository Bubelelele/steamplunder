using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private GameObject fadeDeathPanel;

    private static Player _currentPlayer;

    public static Player GetPlayer() {
        if (_currentPlayer == null) Debug.LogWarning($"No player assigned!");
        return _currentPlayer;
    }

    public static Vector3 GetPosition() {
        if (_currentPlayer == null) Debug.LogWarning($"No player assigned!");
        return _currentPlayer.transform.position;
    }

    private void Awake() {
        _currentPlayer = this;
        PlayerData.Init(maxHealth);
        PlayerData.OnDie += OnDie;
    }

    private void Start() {
        PlayerData.Start();
    }

    private void OnDestroy() {
        PlayerData.OnDie -= OnDie;
    }

    private void OnDie(Transform source) {
        if (fadeDeathPanel != null) {
            Instantiate(fadeDeathPanel, Vector3.zero, Quaternion.identity);
        }
    }

    public void DieAnimFinished() {
        PlayerData.ReloadScene();
        PlayerData.SetHealth(maxHealth);
        //AudioManager.PlayAudio(AudioType.Death_Player);
    }
}
