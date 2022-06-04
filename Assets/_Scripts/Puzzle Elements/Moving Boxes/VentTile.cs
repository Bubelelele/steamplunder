using System;
using UnityEngine;

public class VentTile : PressableTile {

    [SerializeField] private AudioClip ventBlocked;
    [SerializeField] private AudioClip ventUnblocked;
    [SerializeField] private AudioSource audioSource;

    public override bool IsPressed => TileOccupied && _currentBox.GetType() != typeof(WoodenBox);

    public override Tile TakeTile(Box box) {
        _currentBox = box;
        if (IsPressed) {
            StateChanged();
            audioSource.clip = ventBlocked;
            audioSource.Play();
        }
        return this;
    }

    public override void ClearTile() {
        if (IsPressed) {
            audioSource.clip = ventUnblocked;
            audioSource.Play();
        }
        base.ClearTile();
    }
}
