using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour {
    
    [SerializeField] private PlayableDirector playableDirector;
    [SerializeField] private bool playOnStart;
    [SerializeField] private bool isStoryCutscene;
    [SerializeField] private string storyCutsceneId;
    [SerializeField] private UnityEvent onCutsceneFinished;

    private bool _played;
    
    private void Start() {
        if (storyCutsceneId.GetSavedBool())
            _played = true;
        if (playableDirector.playableAsset == null)
            Debug.LogWarning($"{gameObject.name} needs a timeline asset!");
        if (isStoryCutscene && storyCutsceneId == "") 
            Debug.LogWarning($"{gameObject.name} needs a story cutscene ID to function correctly!");
        if (playOnStart)
            CutsceneManager.PlayCutscene(playableDirector);
    }

    private void OnEnable() => CutsceneManager.OnCutscenePlaying += OnCutscenePlaying;

    private void OnDestroy() => CutsceneManager.OnCutscenePlaying -= OnCutscenePlaying;

    private void OnTriggerEnter(Collider other) {
        if (_played || !other.CompareTag("Player")) return;
        CutsceneManager.PlayCutscene(playableDirector);
        _played = true;
        
        if (isStoryCutscene) {
            //Add to list of watched cutscenes, will then get saved as watched on actual shrine save
            PlayerData.AddToWatchedStoryCutscenes(storyCutsceneId);
        }
    }

    private void OnCutscenePlaying(bool isPlaying) {
        if (isPlaying) return;
        onCutsceneFinished.Invoke();
    }
}