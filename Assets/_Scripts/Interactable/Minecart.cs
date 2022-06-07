using System;
using UnityEngine;
using UnityEngine.Playables;

public class Minecart : MonoBehaviour, IInteractable {
    
    [SerializeField] private Transform playerSeat;
    [SerializeField] private PlayableDirector[] cutscenes;
    
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
        var nextCutscene = cutscenes[_nextCheckpoint-1];
        nextCutscene.stopped += OnCutsceneEnded;
        _playerMovement.SetFreeze(true);
        _playerTransform.SetParent(playerSeat);
        _playerTransform.localPosition = Vector3.zero;
        nextCutscene.Play();
        _currentCheckpoint = _nextCheckpoint;
    }

    private void OnCutsceneEnded(PlayableDirector playableDirector) {
        playableDirector.stopped -= OnCutsceneEnded;
        _playerTransform.SetParent(null);
        _playerMovement.SetFreeze(false);
    }
    
    private bool StandStill() => _nextCheckpoint == _currentCheckpoint;

    [ContextMenu("Next Checkpoint")]
    public void Proceed() => _nextCheckpoint++;

}