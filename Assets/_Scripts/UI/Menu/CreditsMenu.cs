using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMenu : MonoBehaviour {

    public void ToMainMenu() => SceneManager.LoadScene(0);
    public void QuitButton() => Application.Quit();
    
}
