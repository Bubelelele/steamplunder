using UnityEngine;

public class WalkingAudio : MonoBehaviour {

    [SerializeField] private bool active;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] walkingAudioClips;

    public void Step() {
        if (!active) return;
        var clipToPlay = walkingAudioClips[Random.Range(0, walkingAudioClips.Length-1)];
        audioSource.clip = clipToPlay;
        audioSource.Play();
    }

}
