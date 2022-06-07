using UnityEngine;

public class Roundabout : MonoBehaviour {
    
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Transform bridge;
    [SerializeField] private Transform backCog;
    [SerializeField] private AudioSource audioSource;
    
    public void RotateTick() {
        var rotationAmount = rotationSpeed * Time.deltaTime;
        bridge.Rotate(-rotationAmount * Vector3.up);
        backCog.Rotate(rotationAmount * Vector3.forward);
        
        if (!audioSource.isPlaying) audioSource.Play();
    }
    
}
