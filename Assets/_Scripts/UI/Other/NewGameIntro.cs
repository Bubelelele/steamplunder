using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class NewGameIntro : MonoBehaviour {

    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private GameObject fadeInPanel;
    [SerializeField] private GameObject fadeOutPanel;
    [SerializeField] private float fadeWait;

    private void Awake() {
        videoPlayer.loopPointReached += VideoEnded;
        if (fadeInPanel != null) {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity);
            Destroy(panel, 1);
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) VideoEnded(null);
    }

    private void VideoEnded(VideoPlayer source) {
        StartCoroutine(Fade());
    }
    
    private IEnumerator Fade() {
        if (fadeOutPanel != null) {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        
        yield return new WaitForSeconds(fadeWait);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(2);
        while (!asyncOperation.isDone) {
            yield return null;
        }
    }
}
