using System;
using TMPro;
using UnityEngine;

public class NoteUI : MonoBehaviour {
    
    [SerializeField] private KeyCode exitKey = KeyCode.E;
    [SerializeField] private GameObject panelObjects;
    [SerializeField] private TMP_Text noteText;
    [SerializeField] private GameObject mysteryPanel;
    [SerializeField] private GameObject paperPanel;
    

    private bool _readyToDisplay = true;

    private void Awake() {
        Note.OnDisplayNote += DisplayNote;
    }

    private void OnDestroy() {
        Note.OnDisplayNote -= DisplayNote;
    }

    private void Update() {
        if (panelObjects.activeSelf && Input.GetKeyDown(exitKey)) {
            panelObjects.SetActive(false);
            mysteryPanel.SetActive(false);
            paperPanel.SetActive(false);
            GameCanvas.SetHudActive(true);
            Invoke(nameof(ReadyToDisplay), .2f);
            Time.timeScale = 1f;
        } 
    }

    private void DisplayNote(string text, NoteType noteType) {
        if (!_readyToDisplay) return;

        if (noteType == NoteType.Mystery)
            mysteryPanel.SetActive(true);
        else if (noteType == NoteType.Paper) 
            paperPanel.SetActive(true);

        noteText.text = text;
        panelObjects.SetActive(true);
        GameCanvas.SetHudActive(false);
        _readyToDisplay = false;
        Time.timeScale = 0f;
    }

    private void ReadyToDisplay() {
        _readyToDisplay = true;
    }

}
