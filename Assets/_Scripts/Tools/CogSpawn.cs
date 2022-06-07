using System.Collections;
using UnityEngine;

public class CogSpawn : MonoBehaviour {
    
    [SerializeField] private int amountOfCogs;
    [SerializeField] private bool spawnOnAwake;

    private void Awake() {
        if (spawnOnAwake) StartCoroutine(SpawnCogs());
    }

    public IEnumerator SpawnCogs() {
        for (int i = 0; i < amountOfCogs; i++) {
            EffectSpawner.SpawnDroppedCog(transform.position);
            yield return new WaitForSeconds(.4f);
        }
    }

}
