using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDeathCheck : MonoBehaviour {

    [SerializeField] private EnemyBase[] enemiesToCheck;
    [SerializeField] private UnityEvent onEnemiesKilled;

    private void Start() {
        foreach (var enemyBase in enemiesToCheck) {
            enemyBase.onDeath.AddListener(CallOnEnemiesKilled);
        }
    }

    private void CallOnEnemiesKilled() => onEnemiesKilled.Invoke();
}
