using UnityEngine;
using Random = UnityEngine.Random;

public class WalkingAudio : MonoBehaviour {

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] walkingAudioClips;

    private PlayerMovement _playerMovement;

    private void Awake() {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    public void Step(AnimationEvent animationEvent) {
        var canDoStepSound = _playerMovement.IsSleeping() || animationEvent.animatorClipInfo.weight < .5f;
        if (canDoStepSound) return;

        var clipToPlay = walkingAudioClips[Random.Range(0, walkingAudioClips.Length-1)];
        audioSource.clip = clipToPlay;
        audioSource.Play();
    }

}
