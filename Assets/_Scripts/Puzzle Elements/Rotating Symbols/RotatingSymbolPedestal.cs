using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RotatingSymbolPedestal : MonoBehaviour, IInteractable {

    [Header("Visual")]
    [SerializeField] private float rotateDelay = .01f;
    [SerializeField] private int rotationDegrees = 60;
    [SerializeField] private Transform rotatingTransform;
    [SerializeField] private Image[] symbolImages;

    [Header("Puzzle")]
    [SerializeField] private int correctSymbolIndex;
    [SerializeField] private List<Sprite> symbols;
    public UnityEvent onCorrect;

    public bool Solved => _currentSide == _correctSide;

    private bool _isRotating;
    private int _currentSide;
    private int _correctSide;

    private void Awake() {
        Scramble();
    }

    public bool HoldToInteract { get; }

    public void Interact() {
        if (_isRotating) return;
        
        _isRotating = true;
        StartCoroutine(Rotate());
    }
    
    public string GetDescription() => _isRotating ? "(Rotating)" : "Rotate Block";

    public string GetKeyText() => null;

    public void Scramble() {
        //Will display the correct symbol + 2 random ones.
        var symbolsList = new List<Sprite>(symbols);
        var correctSymbol = symbolsList[correctSymbolIndex];
        symbolsList.Remove(correctSymbol);
        var randomSymbol1 = GetRandomFromListAndRemove(symbolsList);
        var randomSymbol2 = GetRandomFromListAndRemove(symbolsList);

        //Choose random start positions for each symbol
        List<int> sides = new() {0, 1, 2};
        _correctSide = GetRandomFromListAndRemove(sides);
        symbolImages[_correctSide].sprite = correctSymbol;
        symbolImages[GetRandomFromListAndRemove(sides)].sprite = randomSymbol1;
        symbolImages[GetRandomFromListAndRemove(sides)].sprite = randomSymbol2;
    }

    private T GetRandomFromListAndRemove<T>(List<T> list) {
        var randomItem = list[Random.Range(0, list.Count)];
        list.Remove(randomItem);
        return randomItem;
    }

    private IEnumerator Rotate() {
        for (int i = 0; i < rotationDegrees; i++) {
            rotatingTransform.Rotate(Vector3.up);
            yield return new WaitForSeconds(rotateDelay);
        }

        _currentSide++;
        if (_currentSide >= symbolImages.Length) _currentSide = 0;
        if (Solved) onCorrect.Invoke();
        _isRotating = false;
    }
}