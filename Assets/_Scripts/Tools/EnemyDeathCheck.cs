using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDeathCheck : MonoBehaviour {

    [SerializeField] private EnemyBase[] enemiesToCheck;
    [SerializeField] private UnityEvent onEnemiesKilled;

    private int _amountKilled;

    private void Start() {
        foreach (var enemyBase in enemiesToCheck) {
            enemyBase.onDeath.AddListener(OnEnemyKilled);
        }
    }

    private void OnEnemyKilled() {
        _amountKilled++;
        if (_amountKilled >= enemiesToCheck.Length) onEnemiesKilled.Invoke();
    }
}
