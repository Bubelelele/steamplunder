using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class RotatingSymbolBlock : MonoBehaviour, IInteractable {

    [SerializeField] private float rotateDelay = .01f;
    [SerializeField] private Transform blockTransform;
    [SerializeField] private bool noScramble;
    
    public int CurrentSymbol { get; private set; } = 1;
    // 1-Clubs, 2-Diamonds, 3-Hearts, 4-Spades //
    public event Action OnRotate;

    private bool _isRotating;

    private void Start() {
        //Random start symbol
        if (noScramble) return;
        CurrentSymbol = Random.Range(1, 5);
        blockTransform.Rotate(new Vector3(0, (CurrentSymbol-1)*90f));
    }

    public bool HoldToInteract { get; }

    public void Interact() {
        if (_isRotating) return;
        
        _isRotating = true;
        StartCoroutine(Rotate());
    }
    
    public string GetDescription() => _isRotating ? "(Rotating)" : "Rotate Block";

    public string GetKeyText() => null;

    private IEnumerator Rotate() {
        for (int i = 0; i < 90; i++) {
            blockTransform.Rotate(new Vector3(0, 1));
            yield return new WaitForSeconds(rotateDelay);
        }

        CurrentSymbol++;
        if (CurrentSymbol > 4) CurrentSymbol = 1;
        OnRotate?.Invoke();
        _isRotating = false;
    }
}