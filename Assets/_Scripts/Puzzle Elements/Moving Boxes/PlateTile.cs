using System;
using UnityEngine;
using UnityEngine.Events;

public class PlateTile : Tile {
    
    public event Action OnPressStateChanged;
    
    private bool _playerOn;

    public bool IsPressed => _playerOn || TileOccupied;

    public override Tile TakeTile(Box box) {
        _currentBox = box;
        StateChanged();
        return this;
    }

    public override void ClearTile() {
        base.ClearTile();
        StateChanged();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            _playerOn = true;
            StateChanged();
        }
    }
    
    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            _playerOn = false;
            StateChanged();
        }
    }
    
    private void StateChanged() => OnPressStateChanged?.Invoke();
    
}
