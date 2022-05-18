using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {

    [Header("New Scene")]
    [SerializeField] private string sceneToLoad;
    [SerializeField] private int linkedDoorId;
    [SerializeField] private Transform playerPosNextToDoor;
    
    [Header("Transition")]
    [SerializeField] private GameObject fadeInPanel;
    [SerializeField] private GameObject fadeOutPanel;
    [SerializeField] private float fadeWait;

    private void Awake() {
        //Stop player from accidentally going back in right after spawning?

        if (fadeInPanel != null) {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity);
            Destroy(panel, 1);
        }
    }

    private void Start() {
        if (PlayerData.previousDoorId != null && PlayerData.previousDoorId == linkedDoorId)
            Player.GetPlayer().transform.position = playerPosNextToDoor.position;
    }

    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            StartCoroutine(Fade());
        }
    }

    private IEnumerator Fade() {
        if (fadeOutPanel != null) {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }

        PlayerData.previousDoorId = linkedDoorId;

        yield return new WaitForSeconds(fadeWait);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncOperation.isDone) {
            yield return null;
        }
    }
}