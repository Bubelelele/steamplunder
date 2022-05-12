using System;
using UnityEngine;

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

    public void Damage(int amount) {
        PlayerData.SetHealth(PlayerData.Health - amount);
        if (PlayerData.Health <= 0) Die();
    }

    public void Heal(int amount) {
        int healthToSet = PlayerData.Health + amount;
        if (healthToSet > maxHealth) healthToSet = maxHealth;
        PlayerData.SetHealth(healthToSet);
    }

    private void Die() {
        
    }
}
