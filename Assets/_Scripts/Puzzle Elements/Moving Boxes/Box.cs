using UnityEngine;

public class Box : MonoBehaviour {
    
    [SerializeField] private float slideSpeed = 10f;
    [SerializeField] private float raycastLength = 1.5f;
    [SerializeField] private float heightOffset = .1f;
    

    private bool _isMoving;
    private Vector3 _targetPos;
    private Tile _currentTile;

    private void Awake() => AlignToTile();

    private void Update() {
        if (_isMoving) {
            transform.position =
                Vector3.MoveTowards(transform.position, _targetPos, slideSpeed * Time.deltaTime);
            if (transform.position == _targetPos) {
                _isMoving = false;
            }
        }
    }

    public void AttemptMove(bool slide = false) {
        var currentPlayer = Player.GetPlayer();
        if (currentPlayer == null || _isMoving) return;

        Vector3 moveDirection = GetMoveDirection(currentPlayer.transform.position);
        if (slide) {
            Tile nextTile = FindOpenTile(moveDirection, transform.position);
            Tile destination = null;
            
            while (true) {
                if (nextTile == null) break;
                
                destination = nextTile;
                nextTile = FindOpenTile(moveDirection, destination.transform.position);
            }

            if (destination != null) {
                MoveTo(destination);
            }
        } else {
            Tile openTileInDirection = FindOpenTile(moveDirection, transform.position);

            if (openTileInDirection != null) {
                MoveTo(openTileInDirection);
            }
        }
    }

    private void MoveTo(Tile tile, bool instant = false) {
        _currentTile?.ClearTile();
        _currentTile = tile.TakeTile(this);

        if (instant) {
            transform.position = tile.transform.position + Vector3.up * heightOffset;
        } else {
            _targetPos = tile.transform.position;
            _isMoving = true;
        }
    }

    private void AlignToTile() {
        Tile openTileUnder = FindOpenTile(-transform.up, transform.position + transform.up * .5f);

        if (openTileUnder != null) {
            MoveTo(openTileUnder, true);
        }
    }

    private Tile FindOpenTile(Vector3 searchDirection, Vector3 rayOrigin) {
        Ray ray = new Ray(rayOrigin, searchDirection);

        if (Physics.Raycast(ray, out var hit, raycastLength)) {
            var tile = hit.transform.GetComponent<Tile>();
            if (tile == null) return null;
            if (tile.TileOccupied) return null;
            return tile;
        }

        return null;
    }

    private Vector3 GetMoveDirection(Vector3 playerPosition) {
        Vector3 playerVector = (transform.position - playerPosition).normalized;

        var forwardDot = Vector3.Dot(playerVector, transform.forward);
        var rightDot = Vector3.Dot(playerVector, transform.right);

        if (Mathf.Abs(forwardDot) >= Mathf.Abs(rightDot)) {
            if (forwardDot > 0) return transform.forward;
            return -transform.forward;
        }

        if (rightDot > 0) return transform.right;
        return -transform.right;
    }

    //To visualize where the box will check for grid tiles
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;

        var t = transform;
        var position = t.position;
        Gizmos.DrawLine(position, position + t.forward * raycastLength);
        Gizmos.DrawLine(position, position - t.forward * raycastLength);
        Gizmos.DrawLine(position, position + t.right * raycastLength);
        Gizmos.DrawLine(position, position - t.right * raycastLength);
    }

}
