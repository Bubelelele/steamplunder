using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour {
    
    [SerializeField] private PlayableDirector playableDirector;
    [SerializeField] private bool freezeTime;
    [SerializeField] private bool playOnStart;
    [SerializeField] private bool inactiveOnStart;
    [SerializeField] private bool isStoryCutscene;
    [SerializeField] private string storyCutsceneId;
    [SerializeField] private UnityEvent onCutsceneFinished;

    private bool _played;
    private bool _disabled;

    private void Awake() {
        _disabled = inactiveOnStart;
        if (storyCutsceneId.GetSavedBool()) storyCutsceneId.AddToWatchedStoryCutscenes();
        if (storyCutsceneId.CheckWatchedStoryCutscenes())
            _played = true;
        if (playableDirector.playableAsset == null)
            Debug.LogWarning($"{gameObject.name} needs a timeline asset!");
        if (freezeTime) playableDirector.timeUpdateMode = DirectorUpdateMode.UnscaledGameTime;
        if (isStoryCutscene && storyCutsceneId == "") 
            Debug.LogWarning($"{gameObject.name} needs a story cutscene ID to function correctly!");
        if (playOnStart)
            CutsceneManager.PlayCutscene(playableDirector);
    }
    
    private void OnDestroy() => CutsceneManager.OnCutscenePlaying -= OnCutscenePlaying;

    private void OnTriggerEnter(Collider other) {
        if (_played || _disabled || !other.CompareTag("Player")) return;
        CutsceneManager.OnCutscenePlaying += OnCutscenePlaying;
        CutsceneManager.PlayCutscene(playableDirector);
        _played = true;
        
        if (isStoryCutscene) {
            //Add to list of watched cutscenes, will then get saved as watched on actual shrine save
            storyCutsceneId.AddToWatchedStoryCutscenes();
        }
    }

    private void OnCutscenePlaying(bool isPlaying) {
        if (isPlaying) return;
        CutsceneManager.OnCutscenePlaying -= OnCutscenePlaying;
        onCutsceneFinished.Invoke();
    }

    public void EnableTrigger() {
        _disabled = false;
    }
}