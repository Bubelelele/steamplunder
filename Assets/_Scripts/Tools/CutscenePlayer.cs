using UnityEngine;
using UnityEngine.Playables;

public class CutscenePlayer : MonoBehaviour {
    
    [SerializeField] private PlayableDirector playableDirector;

    public void Play() {
        CutsceneManager.PlayCutscene(playableDirector);
    }

}
