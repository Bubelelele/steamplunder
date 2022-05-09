using UnityEngine;

public class Tile : MonoBehaviour {

    private WoodenBox _currentBox = null;

    public bool TileOccupied => _currentBox != null;

    public Tile TakeTile(WoodenBox box) {
        _currentBox = box;
        return this;
    }

    public void ClearTile() => _currentBox = null;

}
