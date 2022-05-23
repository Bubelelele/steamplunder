using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleEffect : MonoBehaviour {
    private void Start() => Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);
}