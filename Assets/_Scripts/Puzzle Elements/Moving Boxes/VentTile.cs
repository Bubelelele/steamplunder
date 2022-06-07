using UnityEngine;
using UnityEngine.VFX;

public class VentTile : PressableTile {

    [SerializeField] private AudioClip ventBlocked;
    [SerializeField] private AudioClip ventUnblocked;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private VisualEffect steamVFX;

    public override bool IsPressed => TileOccupied && _currentBox.GetType() != typeof(WoodenBox);

    public override Tile TakeTile(Box box) {
        _currentBox = box;
        if (IsPressed) {
            StateChanged();
            audioSource.clip = ventBlocked;
            audioSource.loop = false;
            steamVFX.Stop();
            audioSource.Play();
        }
        return this;
    }

    public override void ClearTile() {
        if (IsPressed) {
            audioSource.clip = ventUnblocked;
            audioSource.loop = true;
            steamVFX.Play();
            audioSource.Play();
        }
        base.ClearTile();
    }
}
