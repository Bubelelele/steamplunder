using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private GameObject fadeDeathPanel;

    private static Player _currentPlayer;
    private static PlayerMovement _playerMovement;

    public static Player GetPlayer() {
        if (_currentPlayer == null) Debug.LogWarning($"No player assigned!");
        return _currentPlayer;
    }

    public static PlayerMovement GetPlayerMovement() {
        if (_playerMovement == null) Debug.LogWarning($"No player assigned!");
        return _playerMovement;
    }

    public static Vector3 GetPosition() {
        if (_currentPlayer == null) Debug.LogWarning($"No player assigned!");
        return _currentPlayer.transform.position;
    }

    private void Awake() {
        _currentPlayer = this;
        _playerMovement = GetComponent<PlayerMovement>();
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
        
        AudioManager.PlayAudio(AudioType.Death_Player);
    }

    public void DieAnimFinished() {
        PlayerData.SetHealth(maxHealth);
        PlayerData.ReloadScene();
    }
}
