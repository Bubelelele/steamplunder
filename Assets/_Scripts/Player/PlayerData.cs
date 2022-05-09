using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public static class PlayerData {
    /*
     * Data which needs to travel between scenes gets stored here
    */

    //Initialization

    private static bool _initialized;

    public static void Init(int maxHealth) {
        if (_initialized) {
            NewSceneInit();
            return;
        }

        if (Health == 0) SetHealth(maxHealth);
        SetupArtifactStatus();

        _initialized = true;
    }

    private static void NewSceneInit() {
        //run all static events?
    }

    //Health
    public static int Health { get; private set; }

    public static void SetHealth(int amount) {
        Health = amount;
        OnHealthChanged?.Invoke(Health);
    }

    public static event Action<int> OnHealthChanged;

    //Artifact
    public static Dictionary<Artifact, bool> ArtifactStatus { get; } = new();

    private static void SetupArtifactStatus() {
        if (ArtifactStatus.Count != 0) return;
        
        var listOfArtifacts = Enum.GetValues(typeof(Artifact)).Cast<Artifact>();
        foreach (var artifactType in listOfArtifacts) {
            ArtifactStatus.Add(artifactType, false);
        }
    }

    public static void UnlockArtifact(Artifact artifactType) {
        if (ArtifactStatus.ContainsKey(artifactType)) {
            if (ArtifactStatus[artifactType]) {
                Debug.Log($"{artifactType} tried to be unlocked, but already is!");
                return;
            }

            ArtifactStatus[artifactType] = true;
            OnArtifactUnlocked?.Invoke(artifactType);
        } else {
            Debug.Log($"No {artifactType.ToString()} in the system yet!");
        }
    }

    public static event Action<Artifact> OnArtifactUnlocked;

    //SceneTransition Door
    public static int? previousDoorId;

    //Story beats, ow puzzle completions
    public enum StoryBeats {//Important events, would often be things that would happen next to a shrine
        Tutorial_AxeGet = 0,
        D1_HammerGet = 1,
        Plains_StrangerEvent = 2
    }

    private static List<string> _watchedStoryCutscenes = new();
    
    public static void AddToWatchedStoryCutscenes(string storyCutsceneId) => _watchedStoryCutscenes.Add(storyCutsceneId);

    //Saving and Loading
    public static readonly string isSavedGame = nameof(isSavedGame);
    private static readonly string savedScene = nameof(savedScene);
    private static readonly string savedHealth = nameof(savedHealth);

    public static void Save() {
        isSavedGame.SaveBool(true);
        
        //Player related
        savedScene.SaveString(SceneManager.GetActiveScene().name);
        savedHealth.SaveInt(Health);
        foreach (var pair in ArtifactStatus) {
            //PlayerPrefs key for these is the name of the artifact enum value
            pair.Key.ToString().SaveBool(pair.Value);
        }
        
        //world and progression related
        foreach (var watchedStoryCutscene in _watchedStoryCutscenes) {
            watchedStoryCutscene.SaveBool(true);
        }

        PlayerPrefs.Save();
    }

    public static void Load() {
        Health = savedHealth.GetSavedInt();
        SetupArtifactStatus();
        foreach (var artifact in Enum.GetValues(typeof(Artifact)).Cast<Artifact>()) {
            if (artifact.ToString().GetSavedBool()) UnlockArtifact(artifact);
        }

        SceneManager.LoadScene(savedScene.GetSavedString());
    }

    private static void SaveInt(this string key, int value) => PlayerPrefs.SetInt(key, value);
    private static void SaveString(this string key, string value) => PlayerPrefs.SetString(key, value);
    private static void SaveBool(this string key, bool value) => PlayerPrefs.SetInt(key, value ? 1 : 0);
    
    public static int GetSavedInt(this string key) => PlayerPrefs.GetInt(key);
    public static string GetSavedString(this string key) => PlayerPrefs.GetString(key);
    public static bool GetSavedBool(this string key) => PlayerPrefs.GetInt(key) == 1;


}