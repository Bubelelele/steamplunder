using System;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour {

    [SerializeField] private KeyCode fastForwardCutsceneKey = KeyCode.Space;
    [SerializeField] private float fastForwardMultiplier = 3f;

    private static PlayableDirector _activePlayableDirector;

    public static event Action OnCutsceneFinished;
    public static bool IsCutscenePlaying => _activePlayableDirector.playableGraph.GetRootPlayable(0).GetSpeed() != 0d;

    private void Update() {
        if (_activePlayableDirector == null) return;
        
        if (Input.GetKey(fastForwardCutsceneKey)) {
            _activePlayableDirector.playableGraph.GetRootPlayable(0).SetSpeed(fastForwardMultiplier);
        } else {
            _activePlayableDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
        }
    }

    public static void PlayCutscene(PlayableDirector activePlayableDirector) {
        _activePlayableDirector = activePlayableDirector;

        Player.GetPlayer()?.GetComponent<PlayerMovement>().SetFreeze(true);
        _activePlayableDirector.Play();
        _activePlayableDirector.stopped += CutsceneEnded;
    }

    private static void CutsceneEnded(PlayableDirector director) {
        if (_activePlayableDirector != null)
            _activePlayableDirector.stopped -= CutsceneEnded;
        Player.GetPlayer()?.GetComponent<PlayerMovement>().SetFreeze(false);
        OnCutsceneFinished?.Invoke();
        _activePlayableDirector = null;
    }
}