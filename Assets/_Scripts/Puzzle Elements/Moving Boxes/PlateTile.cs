public class PlateTile : PressableTile {
    
    private bool _playerOn;

    public override bool IsPressed => _playerOn || TileOccupied;

    //No longer allows player to step on plate
    /*private void OnCollisionEnter(Collision collision) {
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
    }*/
    
    
}
