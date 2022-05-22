using System;
using UnityEngine;

public class VentTile : Tile {
    
    public event Action OnPressStateChanged;
    
    private bool _playerOn;

    public bool IsPressed => _playerOn || TileOccupied;

    public override Tile TakeTile(Box box) {
        _currentBox = box;
        if (_currentBox.GetType() != typeof(WoodenBox)) OnPressStateChanged?.Invoke();
        return this;
    }

}
