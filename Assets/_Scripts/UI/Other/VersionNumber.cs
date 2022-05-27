using System;
using TMPro;
using UnityEngine;

public class VersionNumber : MonoBehaviour {

    [SerializeField] private TMP_Text text;

    private void Awake() {
        text.text = $"v{Application.version}";
    }
}
