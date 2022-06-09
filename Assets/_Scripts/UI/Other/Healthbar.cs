using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {
    
    [SerializeField] private Image healthbarImage;
    [SerializeField] private float reduceSpeed = 2f;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private UnityEvent onHealthbarEmpty;
    
    private float _target = 1;
    
    public void UpdateHealthbar(int currentHealth, int maxHealth) {
        int currentHealthToDisplay = (currentHealth < 0) ? 0 : currentHealth;
        _target = (float) currentHealthToDisplay / maxHealth;
        if (healthText != null) healthText.text = currentHealthToDisplay.ToString();
    }

    private void Update() {
        healthbarImage.fillAmount = Mathf.MoveTowards(healthbarImage.fillAmount, _target, reduceSpeed * Time.deltaTime);
        if (healthbarImage.fillAmount == 0f) {
            onHealthbarEmpty.Invoke();
        }
    }
}