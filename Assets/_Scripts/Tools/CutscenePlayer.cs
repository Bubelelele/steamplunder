using UnityEngine;
using UnityEngine.Playables;

public class CutscenePlayer : MonoBehaviour {
    
    [SerializeField] private bool canPlayMultipleTimes;
    [SerializeField] private PlayableDirector playableDirector;

    private bool _played;

    public void Play() {
        if (!canPlayMultipleTimes && _played) return;

        _played = true;
        CutsceneManager.PlayCutscene(playableDirector);
    }

}
