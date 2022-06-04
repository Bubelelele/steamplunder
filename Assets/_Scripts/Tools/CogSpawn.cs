using UnityEngine;

public class CogSpawn : MonoBehaviour {
    
    [SerializeField] private int amountOfCogs;

    public void SpawnCogs() {
        for (int i = 0; i < amountOfCogs; i++) {
            EffectSpawner.SpawnDroppedCog(transform.position);
        }
    }

}
