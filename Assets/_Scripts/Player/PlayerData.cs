using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public static class PlayerData {
    /*
     * Data which needs to travel between scenes lives here
    */

    #region Initialization
    
    private static bool _initialized;

    public static void Init(int maxHealth) {
        if (_initialized) {
            NewSceneInit();
            return;
        }

        MaxHealth = maxHealth;
        SetHealth(maxHealth);
        SetupArtifactStatus();

        _initialized = true;
    }

    private static void NewSceneInit() {
        _dead = false;
        //run all static events?
    }
    
    #endregion

    #region Health

    public static int Health { get; private set; }
    public static int MaxHealth { get; private set; }

    private static bool _dead;

    public static void SetHealth(int amount) {
        Health = amount;
        OnHealthChanged?.Invoke(Health, MaxHealth);
    }
    
    public static void Damage(int amount, Transform source = null) {
        SetHealth(Health - amount);
        if (Health <= 0) Die(source);
    }

    public static void Heal(int amount) {
        int healthToSet = Health + amount;
        if (healthToSet > MaxHealth) healthToSet = MaxHealth;
        SetHealth(healthToSet);
    }

    private static void Die(Transform source) {
        if (_dead) return;
        
        _dead = true;
        OnDie?.Invoke(source);
    }
    
    public static event Action<int, int> OnHealthChanged;
    public static event Action<Transform> OnDie;

    #endregion

    #region Syringe and Cog Count

    public const int CogsToFillSyringe = 5;
    public const int MaxSyringeSlots = 4;

    private static int _cogCount, _syringeSlots, _filledSyringes;
    
    public static void CogPickedUp() {
        Debug.Log("New cog");
        _cogCount++;
        OnCogCountIncreased?.Invoke(_cogCount);
        if (_cogCount >= CogsToFillSyringe) FillSyringe();
    }
    
    public static void UnlockSyringeSlot() {
        if (_syringeSlots >= MaxSyringeSlots) {
            Debug.Log("Max syringe slots already!");
            return;
        }

        Debug.Log("New syringe");
        _syringeSlots++;
        OnSyringeSlotUnlocked?.Invoke(_syringeSlots);
    }

    public static void ResetCogCount() {
        _cogCount = 0;
        OnCogCountIncreased?.Invoke(_cogCount);
    }

    private static void FillSyringe() {
        if (_syringeSlots == 0) {
            Debug.Log("Cannot fill any syringes as none are unlocked!");
            return;
        }
        if (_filledSyringes >= _syringeSlots) {
            Debug.Log("No more syringes to fill!");
            return;
        }

        Debug.Log("Fill syringe");
        _filledSyringes++;
        OnSyringeFilled?.Invoke(_filledSyringes);
    }

    public static event Action<int> OnCogCountIncreased;
    public static event Action<int> OnSyringeSlotUnlocked;
    public static event Action<int> OnSyringeFilled;

    #endregion

    #region Artifacts

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
            Debug.Log($"{artifactType} unlocked");
        } else {
            Debug.Log($"No {artifactType} in the system yet!");
        }
    }

    public static event Action<Artifact> OnArtifactUnlocked;

    #endregion

    #region Misc

    //SceneTransition Door
    public static int? previousDoorId;

    //Story beats, ow puzzle completions, watched cutscenes
    public static List<StoryBeat> AchievedStoryBeats { get; } = new();
    public static void AddToAchievedStoryBeats(this StoryBeat storyBeat) => AchievedStoryBeats.Add(storyBeat);

    private static List<string> _watchedStoryCutscenes = new();
    public static void AddToWatchedStoryCutscenes(this string storyCutsceneId) => _watchedStoryCutscenes.Add(storyCutsceneId);

    #endregion

    #region Saving & Loading

    public static readonly string isSavedGame = nameof(isSavedGame);
    private static readonly string savedScene = nameof(savedScene);
    private static readonly string savedHealth = nameof(savedHealth);
    private static readonly string savedCogCount = nameof(savedCogCount);
    private static readonly string savedSyringeSlots = nameof(savedSyringeSlots);
    private static readonly string savedFilledSyringes = nameof(savedFilledSyringes);

    public static void Save() {
        isSavedGame.SaveBool(true);
        
        //Player related
        savedScene.SaveString(SceneManager.GetActiveScene().name);
        savedHealth.SaveInt(Health);
        foreach (var pair in ArtifactStatus) {
            //PlayerPrefs key for these is the name of the artifact enum value
            pair.Key.ToString().SaveBool(pair.Value);
        }
        
        savedCogCount.SaveInt(_cogCount);
        savedSyringeSlots.SaveInt(_syringeSlots);
        savedFilledSyringes.SaveInt(_filledSyringes);
        
        //world and progression related
        foreach (var watchedStoryCutscene in _watchedStoryCutscenes) {
            watchedStoryCutscene.SaveBool(true);
        }

        foreach (var achievedStoryBeat in AchievedStoryBeats) {
            achievedStoryBeat.ToString().SaveBool(true);
        }

        PlayerPrefs.Save();
    }

    public static void Load() {
        Health = savedHealth.GetSavedInt();
        SetupArtifactStatus();
        foreach (var artifact in Enum.GetValues(typeof(Artifact)).Cast<Artifact>()) {
            if (artifact.ToString().GetSavedBool()) UnlockArtifact(artifact);
        }

        _cogCount = savedCogCount.GetSavedInt();
        _syringeSlots = savedSyringeSlots.GetSavedInt();
        _filledSyringes = savedFilledSyringes.GetSavedInt();

        SceneManager.LoadScene(savedScene.GetSavedString());
    }

    //Saving and loading extension methods
    private static void SaveInt(this string key, int value) => PlayerPrefs.SetInt(key, value);
    private static void SaveString(this string key, string value) => PlayerPrefs.SetString(key, value);
    private static void SaveBool(this string key, bool value) => PlayerPrefs.SetInt(key, value ? 1 : 0);
    
    public static int GetSavedInt(this string key) => PlayerPrefs.GetInt(key);
    public static string GetSavedString(this string key) => PlayerPrefs.GetString(key);
    public static bool GetSavedBool(this string key) => PlayerPrefs.GetInt(key) == 1;

    #endregion

}