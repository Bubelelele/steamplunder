using UnityEngine;

public class Tile : MonoBehaviour {

    protected Box _currentBox;

    public bool TileOccupied => _currentBox != null;

    public virtual Tile TakeTile(Box box) {
        _currentBox = box;
        return this;
    }

    public virtual void ClearTile() => _currentBox = null;

    public Vector3 GetPosition() => transform.position;

}
