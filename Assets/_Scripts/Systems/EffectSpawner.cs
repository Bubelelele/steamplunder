using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EffectSpawner : MonoBehaviour {
    
    [Header("Prefabs")]
    [SerializeField] private GameObject droppedCog;
    [SerializeField] private GameObject bloodFX;
    [SerializeField] private GameObject pickupFX;

    private static EffectSpawner _instance;
    private void Awake() => _instance = this;

    public static void SpawnDroppedCog(Vector3 position) => _instance.DroppedCog(position);
    public static void SpawnBloodFX(Vector3 position) => _instance.BloodFX(position);
    public static void SpawnPickupFX(Vector3 position) => _instance.PickupFX(position);

    private void SpawnFX(GameObject prefab, Vector3 position) => Instantiate(prefab, position, Quaternion.identity);

    private void DroppedCog(Vector3 position) {
        var cog = Instantiate(droppedCog, position + Vector3.up, Quaternion.identity);
        if (cog.TryGetComponent<Rigidbody>(out var rb)) {
            rb.AddForce(Random.Range(-3f, 3f), 4f, Random.Range(-3f, 3f), ForceMode.Impulse);
        }
    }
    
    private void BloodFX(Vector3 position) => SpawnFX(bloodFX, position);
    private void PickupFX(Vector3 position) => SpawnFX(pickupFX, position);
}
