using UnityEngine;

public class StoryBeatAchieve : MonoBehaviour {
    
    [SerializeField] private StoryBeat storyBeat;

    public void AchieveStoryBeat() {
        storyBeat.AddToAchievedStoryBeats();
    }

}
