using System;
using UnityEngine;
using UnityEngine.Events;

public class StoryBeatCheck : MonoBehaviour {

    [SerializeField] private StoryBeat storyBeat;
    [SerializeField] private UnityEvent onIfStoryBeatAchieved;

    private void Awake() {
        if (storyBeat.ToString().GetSavedBool()) onIfStoryBeatAchieved.Invoke();
    }
}
