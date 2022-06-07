using TMPro;
using UnityEngine;

public class NoteUI : MonoBehaviour {
    
    [SerializeField] private KeyCode exitKey = KeyCode.E;
    [SerializeField] private GameObject noteImage;
    [SerializeField] private TMP_Text noteText;

    private bool _readyToDisplay = true;

    private void Awake() {
        Note.OnDisplayNote += DisplayNote;
    }

    private void OnDestroy() {
        Note.OnDisplayNote -= DisplayNote;
    }

    private void Update() {
        if (noteImage.activeSelf && Input.GetKeyDown(exitKey)) {
            noteImage.SetActive(false);
            GameCanvas.SetHudActive(true);
            Invoke(nameof(ReadyToDisplay), .2f);
            Time.timeScale = 1f;
        } 
    }

    private void DisplayNote(string text) {
        if (!_readyToDisplay) return;
        
        noteText.text = text;
        noteImage.SetActive(true);
        GameCanvas.SetHudActive(false);
        _readyToDisplay = false;
        Time.timeScale = 0f;
    }

    private void ReadyToDisplay() {
        _readyToDisplay = true;
    }

}
