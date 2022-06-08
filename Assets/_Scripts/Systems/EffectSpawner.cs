using UnityEngine;
using Random = UnityEngine.Random;

public class EffectSpawner : MonoBehaviour {

    [SerializeField] private EffectCollectionSO effectCollection;

    private static EffectSpawner _instance;
    private void Awake() => _instance = this;

    //Public static method call relayers
    public static void SpawnDroppedCog(Vector3 position) => _instance.DroppedCog(position);
    public static void SpawnBloodFX(Vector3 position) => _instance.BloodFX(position);
    public static void SpawnPickupFX(Vector3 position) => _instance.PickupFX(position);
    public static void SpawnSlashFX(int index, Transform followTransform) => _instance.SlashFX(index, followTransform);
    public static void SpawnBarricadeFX(Vector3 position) => _instance.BarricadeFX(position);
    public static void SpawnSparksFX(Vector3 position) => _instance.SparksFX(position);
    public static void SpawnHammerShockFX(Vector3 position) => _instance.HammerShockFX(position);
    
    //Local instantiates
    private void SpawnFX(GameObject prefab, Vector3 position) => Instantiate(prefab, position, Quaternion.identity);

    private void DroppedCog(Vector3 position) {
        var cog = Instantiate(effectCollection.droppedCog, position + Vector3.up, Quaternion.identity);
        if (cog.TryGetComponent<Rigidbody>(out var rb)) {
            rb.AddForce(Random.Range(-3f, 3f), 6f, Random.Range(-3f, 3f), ForceMode.Impulse);
        }
    }
    
    private void BloodFX(Vector3 position) => SpawnFX(effectCollection.bloodFX, position);
    private void PickupFX(Vector3 position) => SpawnFX(effectCollection.pickupFX, position);
    private void BarricadeFX(Vector3 position) => SpawnFX(effectCollection.barricadeFX, position);
    private void SparksFX(Vector3 position) => SpawnFX(effectCollection.sparksFX, position);
    private void HammerShockFX(Vector3 position) => SpawnFX(effectCollection.hammerShockFX, position);

    private void SlashFX(int index, Transform followTransform) {
        var handler = Instantiate(effectCollection.slashFX[index], followTransform.position, followTransform.rotation).GetComponent<VisualEffectHandler>();
        handler.follow = followTransform;
    }
}
