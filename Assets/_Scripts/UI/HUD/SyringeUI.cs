using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SyringeUI : MonoBehaviour {
    
    [SerializeField] private KeyCode healKey = KeyCode.X;
    [SerializeField] private int healAmount = 50;

    [Header("References")]
    [SerializeField] private Animator cogIndicatorAnim;
    [SerializeField] private Animator[] syringeCanisterAnims;
    [SerializeField] private Image canisterFrame;
    [SerializeField] private Sprite[] canisterFrames;
    [SerializeField] private GameObject canHealText;
    [SerializeField] private Animator syringeHealAnim;
    [SerializeField] private AudioSource audioSource;

    private void Awake() {
        PlayerData.OnCogCountIncreased += CogCountIncreased;
        PlayerData.OnSyringeSlotUnlocked += SyringeSlotUnlocked;
        PlayerData.OnSyringeFilled += FillSyringe;
    }

    private void OnDestroy() {
        PlayerData.OnCogCountIncreased -= CogCountIncreased;
        PlayerData.OnSyringeSlotUnlocked -= SyringeSlotUnlocked;
        PlayerData.OnSyringeFilled -= FillSyringe;
    }
    
    private void Update() {
        if (Input.GetKeyDown(healKey))
            Heal();
    }

    //Rotation degrees based on cog count -> 0: -110,1: -88,2: -66,3: -44,4: -22,5: 0
    private void CogCountIncreased(int newAmount) {
        cogIndicatorAnim.SetInteger("Count", newAmount);
    }
    
    private void SyringeSlotUnlocked(int syringeSlot) {
        canisterFrame.sprite = canisterFrames[syringeSlot];
    }
    
    private void FillSyringe(int syringeSlot) {
        cogIndicatorAnim.SetInteger("Count", PlayerData.CogsToFillSyringe+1);
        syringeCanisterAnims[syringeSlot-1].SetBool("Filled", true);
        canHealText.SetActive(true);
    }

    private void Heal() {
        int slotToUse = PlayerData.UseSyringe();
        if (slotToUse == 0) return;
        
        if (slotToUse == 1) canHealText.SetActive(false);
        PlayerData.Heal(healAmount);
        syringeHealAnim.Play("SyringeFade");
        audioSource.Play();
        syringeCanisterAnims[slotToUse-1].SetBool("Filled", false);
    }

}