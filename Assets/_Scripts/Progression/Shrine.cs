using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Shrine : MonoBehaviour, IInteractable {
    
    [Tooltip("Use an unique shrine ID if there are multiple shrines in the scene")]
    [SerializeField] private int shrineId;
    [SerializeField] private UnityEvent onSave;
    [SerializeField] private float saveDelay = 1f;
    [SerializeField] private Transform spawnpoint;
    
    private bool _onSaveTimeout;
    
    public bool HoldToInteract { get; }
    public void Interact() {
        if (_onSaveTimeout) return;

        StartCoroutine(Save());
    }

    private void Start() {
        if (PlayerData.ShouldSpawnAtShrine(shrineId))
            Player.GetPlayer().transform.position = spawnpoint.position;
    }

    private IEnumerator Save() {
        onSave.Invoke();
        PlayerData.Save(shrineId);
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
