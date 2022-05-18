using UnityEngine;
using UnityEngine.Events;

public class RotatingSymbolsPuzzleController : MonoBehaviour {

    [SerializeField] private RotatingSymbolPedestal[] symbolPedestals;
    [SerializeField] private UnityEvent onPuzzleCompletion;
    
    private bool _solved;

    private void Start() {
        foreach (var pedestal in symbolPedestals) {
            pedestal.onCorrect.AddListener(CheckSolution);
        }

        while (AllPedestalsSolved()) {
            //will scramble all pedestals until it is not a completed state
            Debug.Log("Scrambling");
            foreach (var pedestal in symbolPedestals) {
                pedestal.Scramble();
            }
        }
    }

    private void CheckSolution() {
        if (_solved || !AllPedestalsSolved()) return;

        //Symbols are correctly rotated if this point is reached
        _solved = true;
        onPuzzleCompletion.Invoke();
    }
    
    private bool AllPedestalsSolved() {
        foreach (var pedestal in symbolPedestals) {
            if (!pedestal.Solved) return false;
        }
        return true;
    }
}