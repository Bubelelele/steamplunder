using UnityEngine;

public class PlayAudioOnStart : MonoBehaviour {

    [SerializeField] private AudioType audioType;
    [SerializeField] private bool fade;
    [SerializeField] private float delay;

    private void Start() {
        AudioManager.PlayAudio(audioType, fade, delay);
    }
}
