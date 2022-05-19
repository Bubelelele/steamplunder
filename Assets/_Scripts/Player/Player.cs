using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    [SerializeField] private int maxHealth = 100;
    
    private static Player _currentPlayer;

    public static Player GetPlayer() {
        if (_currentPlayer == null) Debug.LogWarning($"No player assigned!");
        return _currentPlayer;
    }

    private void Awake() {
        _currentPlayer = this;
        PlayerData.Init(maxHealth);
    }

    public void Die() {
        Debug.Log("Player dead");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
