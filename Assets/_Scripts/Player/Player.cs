using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    [SerializeField] private int maxHealth = 100;
    
    private static Player _currentPlayer;
    private Animator _animator;

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
        _animator = GetComponent<Animator>();
    }

    public void Die() {
        Debug.Log("Player dead");
        _animator.SetTrigger("Die");
    }

    public void DieAnimFinished() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
