using System;
using UnityEngine;

public class PlateTile : PressableTile {
    
    private bool _playerOn;

    public override bool IsPressed => _playerOn || TileOccupied;

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
    
    
}
