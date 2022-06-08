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
    
    [MenuItem("DevTools/Die")]
    public static void Die() {
        PlayerData.Damage(99999);
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
    
    [MenuItem("DevTools/Increase Cog Count")]
    public static void IncreaseCogCount() {
        PlayerData.CogPickedUp();
    }
    
    [MenuItem("DevTools/Increase Cog Count x5")]
    public static void IncreaseCogCount5() {
        for (int i = 0; i < 5; i++)
            PlayerData.CogPickedUp();
    }
    
    [MenuItem("DevTools/Unlock Syringe Slot")]
    public static void UnlockSyringeSlot() {
        PlayerData.UnlockSyringeSlot();
    }
    
    [MenuItem("DevTools/Spawn Cogs x5")]
    public static void SpawnCogs() {
        for (int i = 0; i < 5; i++)
            EffectSpawner.SpawnDroppedCog(Player.GetPosition() + Vector3.up);
    }

    [MenuItem("DevTools/Delete Save (!!!)")]
    private static void DeleteSave() {
        PlayerPrefs.DeleteAll();
    }
}
