using System;
using UnityEngine;
using UnityEngine.Events;

public class StoryBeatCheck : MonoBehaviour {

    [SerializeField] private StoryBeat storyBeat;
    [SerializeField] private UnityEvent onIfStoryBeatAchieved;

    private void Start() {
        if (storyBeat.ToString().GetSavedBool()) storyBeat.AddToAchievedStoryBeats(); 
        if (storyBeat.CheckAchievedStoryBeats()) onIfStoryBeatAchieved.Invoke();
    }
}
