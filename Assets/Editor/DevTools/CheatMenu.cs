using System.Linq;
using UnityEditor;
using UnityEngine;

public static class CheatMenu {

    [MenuItem("DevTools/100 HP")]
    public static void MaxHP() {
        PlayerData.SetHealth(100);
    }
    
    [MenuItem("DevTools/1 HP")]
    public static void OneHP() {
        PlayerData.SetHealth(1);
    }
    
    [MenuItem("DevTools/Unlock All Artifacts")]
    public static void UnlockAll() {
        foreach (var artifact in PlayerData.ArtifactStatus.Keys.ToList()) {
            PlayerData.UnlockArtifact(artifact);
        }
    }
    
    [MenuItem("DevTools/Unlock Axe")]
    public static void UnlockAxe() {
        PlayerData.UnlockArtifact(Artifact.Axe);
    }
    
    [MenuItem("DevTools/Unlock Spin")]
    public static void UnlockSpin() {
        PlayerData.UnlockArtifact(Artifact.Spin);
    }
    
    [MenuItem("DevTools/Unlock Gun")]
    public static void UnlockGun() {
        PlayerData.UnlockArtifact(Artifact.Gun);
    }
    
    [MenuItem("DevTools/Unlock Hammer")]
    public static void UnlockHammer() {
        PlayerData.UnlockArtifact(Artifact.Hammer);
    }
    
    [MenuItem("DevTools/Unlock Grapple")]
    public static void UnlockGrapple() {
        PlayerData.UnlockArtifact(Artifact.Grapple);
    }
    
    [MenuItem("DevTools/Unlock Steamer")]
    public static void UnlockSteamer() {
        PlayerData.UnlockArtifact(Artifact.Steamer);
    }

    [MenuItem("DevTools/Delete Save (!!!)")]
    private static void DeleteSave() {
        PlayerPrefs.DeleteAll();
    }
}
