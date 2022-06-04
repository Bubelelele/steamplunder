using UnityEngine;
using Random = UnityEngine.Random;

public class Barricade : MonoBehaviour {

    [SerializeField] private int hitsToBreakBarricade = 3;
    [SerializeField] private GameObject[] planks;

    private int _hitsTaken;
    private int _normalBreakAmount;
    private int _lastBreakAmount;

    private void Awake() {
        Setup(hitsToBreakBarricade);
    }

    private void Setup(int hitsToBreak) {
        float fraction = (float)planks.Length / hitsToBreak;
        _normalBreakAmount = Mathf.RoundToInt(fraction);
        _lastBreakAmount = planks.Length - _normalBreakAmount * (hitsToBreak-1);
    }

    public void OnAxeHit() {
        _hitsTaken++;
        BreakPlanks(_hitsTaken < hitsToBreakBarricade ? _normalBreakAmount : _lastBreakAmount);

        if (_hitsTaken >= hitsToBreakBarricade) {
            gameObject.SetActive(false);
        }
    }

    private void BreakPlanks(int amount) {
        for (int i = 0; i < amount; i++) {
            while (true) {
                var randomPlank = planks[Random.Range(0, planks.Length)];
                if (randomPlank.activeSelf) {
                    randomPlank.SetActive(false);
                    EffectSpawner.SpawnBarricadeFX(randomPlank.transform.position);
                    break;
                }
            }
            
        }
    }

}
