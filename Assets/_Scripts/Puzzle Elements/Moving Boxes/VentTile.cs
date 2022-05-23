using System;
using UnityEngine;

public class VentTile : PressableTile {
    
    public override Tile TakeTile(Box box) {
        _currentBox = box;
        if (_currentBox.GetType() != typeof(WoodenBox)) StateChanged();
        return this;
    }

}
