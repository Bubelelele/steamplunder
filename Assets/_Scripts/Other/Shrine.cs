using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Shrine : MonoBehaviour, IInteractable {

    [SerializeField] private float saveDelay = 1f;
    [SerializeField] private UnityEvent onSave;

    private bool _onSaveTimeout;
    
    public bool HoldToInteract { get; }
    public void Interact() {
        if (_onSaveTimeout) return;

        StartCoroutine(Save());
    }

    private IEnumerator Save() {
        onSave.Invoke();
        PlayerData.Save();
        _onSaveTimeout = true;
        yield return new WaitForSeconds(saveDelay);
        _onSaveTimeout = false;
    }

    public string GetDescription() => _onSaveTimeout ? "(Saving)" : "Save";

    public string GetKeyText() => null;
    
    [ContextMenu("Delete Save (!!!)")]
    private void DeleteSave() {
        PlayerPrefs.DeleteAll();
    }
    
}
