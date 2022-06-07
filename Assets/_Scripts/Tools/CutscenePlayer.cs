using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class CutscenePlayer : MonoBehaviour {
    
    [SerializeField] private bool freezeTime;
    [SerializeField] private bool canPlayMultipleTimes;
    [SerializeField] private PlayableDirector playableDirector;
    [SerializeField] private UnityEvent onCutsceneFinished;

    private bool _played;

    private void Awake() {
        if (freezeTime) playableDirector.timeUpdateMode = DirectorUpdateMode.UnscaledGameTime;
    }

    public void Play() {
        if (!canPlayMultipleTimes && _played) return;

        _played = true;
        CutsceneManager.OnCutscenePlaying += OnCutscenePlaying;
        CutsceneManager.PlayCutscene(playableDirector);
    }
    
    private void OnCutscenePlaying(bool isPlaying) {
        if (isPlaying) return;
        CutsceneManager.OnCutscenePlaying -= OnCutscenePlaying;
        onCutsceneFinished.Invoke();
    }

}
