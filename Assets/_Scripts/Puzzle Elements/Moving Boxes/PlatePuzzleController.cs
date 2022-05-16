using UnityEngine;
using UnityEngine.Events;

public class PlatePuzzleController : MonoBehaviour {

    [SerializeField] private bool canUndoPuzzle;
    [SerializeField] private PlateTile[] platesToCheck;
    [SerializeField] private UnityEvent onPuzzleCompletion;
    [SerializeField] private UnityEvent onPuzzleUndone;
    [SerializeField] private UnityEvent<string> onPlatePressed;

    private bool _completed;
    
    private void Start() {
        foreach (var plateTile in platesToCheck) {
            plateTile.OnPressStateChanged += OnPlatePressStateChanged;
        }
    }

    private void OnDestroy() {
        foreach (var plateTile in platesToCheck) {
            plateTile.OnPressStateChanged -= OnPlatePressStateChanged;
        }
    }

    private void OnPlatePressStateChanged() {
        int numPlatesPressed = 0;
        foreach (var plateTile in platesToCheck) {
            if (plateTile.IsPressed) numPlatesPressed++;
        }

        onPlatePressed.Invoke(numPlatesPressed.ToString());
        
        if (numPlatesPressed == platesToCheck.Length) {
            onPuzzleCompletion.Invoke();
            _completed = true;
        } else if (canUndoPuzzle && _completed) {
            onPuzzleUndone.Invoke();
            _completed = false;
        }
    }
    
}
