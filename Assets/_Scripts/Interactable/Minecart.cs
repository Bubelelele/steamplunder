using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class Minecart : MonoBehaviour, IInteractable {
    
    [SerializeField] private Transform playerSeat;
    [SerializeField] private PlayableDirector[] cutscenes;
    [SerializeField] private UnityEvent[] onCutsceneFinished;

    private int _nextCheckpoint = 1;
    private int _currentCheckpoint;
    private Transform _playerTransform;
    private PlayerMovement _playerMovement;

    private void Start() {
        _playerMovement = Player.GetPlayerMovement();
        _playerTransform = _playerMovement.transform;
    }

    public bool HoldToInteract { get; }

    public void Interact() {
        if (StandStill()) return;

        if (cutscenes.Length >= _nextCheckpoint) 
            PlayNextCutscene();
    }

    public string GetDescription() {
        return StandStill() ? "(Path ahead seems to be blocked)" : "Get into mine cart";
    }

    public string GetKeyText() => null;

    private void PlayNextCutscene() {
        var nextCutscene = cutscenes[_currentCheckpoint];
        nextCutscene.stopped += OnCutsceneEnded;
        _playerTransform.SetParent(playerSeat);
        _playerTransform.localPosition = Vector3.zero;
        nextCutscene.Play();
    }

    private void OnCutsceneEnded(PlayableDirector playableDirector) {
        playableDirector.stopped -= OnCutsceneEnded;
        _playerTransform.SetParent(null);
        onCutsceneFinished[_currentCheckpoint].Invoke();
        _currentCheckpoint = _nextCheckpoint;
    }
    
    private bool StandStill() => _nextCheckpoint == _currentCheckpoint;

    [ContextMenu("Next Checkpoint")]
    public void Proceed() => _nextCheckpoint++;
    
    [ContextMenu("Previous Checkpoint")]
    public void PreviousCheckpoint() => _nextCheckpoint--;

}