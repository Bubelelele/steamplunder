using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RotatingSymbolsPuzzleController : MonoBehaviour {

    [SerializeField] private int[] solution;
    [SerializeField] private RotatingSymbolBlock[] blocks;
    [SerializeField] private UnityEvent onPuzzleCompletion;
    
    private bool _solved;

    private void Start() {
        if (solution.Length != blocks.Length) Debug.LogWarning("Rotating block puzzle solution length error");

        foreach (var block in blocks) {
            block.OnRotate += CheckSolution;
        }
    }

    private void CheckSolution() {
        if (_solved) return;
        
        for (int i = 0; i < blocks.Length; i++) {
            if (blocks[i].CurrentSymbol != solution[i]) return;
        }

        _solved = true;
        onPuzzleCompletion.Invoke();
    }
}