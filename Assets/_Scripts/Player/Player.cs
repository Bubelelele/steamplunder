using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private int maxHealth = 100;
    
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
    }

    public void Die() {
        Debug.Log("Player dead");
    }
}
