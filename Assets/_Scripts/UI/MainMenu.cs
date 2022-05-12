using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject mainScreen;
    [SerializeField] private GameObject saveScreen;
    [SerializeField] private GameObject logo;

    public void PlayButton() {
        if (PlayerData.isSavedGame.GetBool()) {
            mainScreen.SetActive(false);
            saveScreen.SetActive(true);
            return;
        } 
        
        LoadGame(true);
    }

    private IEnumerator LoadSequence(bool newGame) {
        logo.SetActive(false);
        mainScreen.SetActive(false);
        saveScreen.SetActive(false);
        animator.SetTrigger("play");
        Time.timeScale = 10f;
        yield return new WaitForSecondsRealtime(2.1f);
        Time.timeScale = 1f;
        
        if (newGame) {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(1);
        }
        else {
            PlayerData.Load();
        }
    }

    public void LoadGame(bool newGame) => StartCoroutine(LoadSequence(newGame));

    public void QuitButton() {
        Application.Quit();
    }
    
}