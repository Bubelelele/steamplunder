using UnityEngine;

public class HealthUI : MonoBehaviour {
    
    [SerializeField] private Healthbar healthbar;
    
    private void Awake() => PlayerData.OnHealthChanged += OnHealthChanged;

    private void OnDestroy() => PlayerData.OnHealthChanged -= OnHealthChanged;

    private void OnHealthChanged(int health, int maxHealth) => healthbar.UpdateHealthbar(health, maxHealth);
    
}
