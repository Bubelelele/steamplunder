using System;
using UnityEngine;

public class PressableTile : Tile {
    
    public event Action OnPressStateChanged;

    public virtual bool IsPressed => TileOccupied;
    
    public override Tile TakeTile(Box box) {
        _currentBox = box;
        StateChanged();
        return this;
    }
    
    public override void ClearTile() {
        base.ClearTile();
        StateChanged();
    }
    
    protected void StateChanged() => OnPressStateChanged?.Invoke();

    

}
