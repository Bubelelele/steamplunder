using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class OddOneOutPuzzleController : MonoBehaviour {
    
    [SerializeField] private UnityEvent onCompletion;
    [SerializeField] private OddOneOutPillar[] oddOneOutPillars;
    [SerializeField] private List<Sprite> symbolSprites;
    [SerializeField] private AudioClip[] sequenceSounds;
    [SerializeField] private AudioClip completionSound;
    [SerializeField] private AudioClip failSound;
    [SerializeField] private AudioSource audioSource;

    private int[] _correctPillars;
    private int _currentRow;

    private void Awake() {
        if (oddOneOutPillars.Length < 5) {
            Debug.Log("Odd One Out puzzle needs at least 5 pillars!");
            Destroy(this);
        }
        Scramble();

        foreach (var pillar in oddOneOutPillars) {
            pillar.OnPillarTouched += PillarTouched;
        }
    }

    private void Scramble() {
        _correctPillars = new int[5];
        AssignRandomCorrectPillars();
        List<Sprite> symbolsList;
        Sprite correctSymbol;
        Sprite incorrectSymbol;

        //Row 1 - one is different
        OneIsDifferent(0);
        
        //Row 2 - one is different, cant be same as row 1
        OneIsDifferent(1);
        
        //Row 3 - two are different, but one of them should be on the same pillar as correct row 1 or 2
        TwoAreDifferent(2);
        
        //Row 4 - two are different. they are the two remaining ones, meaning that you have to look at the row beneath to know which to choose here
        FillRest(3);
        
        //Row 5 - 1 unique
        FillRest(4);
        
        void AssignRandomCorrectPillars() {
            List<int> rowIndexes = new List<int> {0, 1, 2, 3, 4};
            for (int i = 0; i < _correctPillars.Length; i++) {
                _correctPillars[i] = GetRandomFromListAndRemove(rowIndexes);
            }
        }

        void ResetSymbols() {
            symbolsList = new List<Sprite>(symbolSprites);
            correctSymbol = GetRandomFromListAndRemove(symbolsList);
            incorrectSymbol = GetRandomFromListAndRemove(symbolsList);
        }

        void OneIsDifferent(int rowIndex) {
            ResetSymbols();
            foreach (var pillar in oddOneOutPillars) {
                pillar.SetSymbolSprite(incorrectSymbol, rowIndex);
            }
            oddOneOutPillars[_correctPillars[rowIndex]].SetSymbolSprite(correctSymbol, rowIndex);
        }

        void TwoAreDifferent(int rowIndex) {
            ResetSymbols();
            if (rowIndex < 1) {
                Debug.Log("TwoAreDifferent cannot be used earlier than line 2");
                return;
            }
            OneIsDifferent(rowIndex);
            int impostorPillar = _correctPillars[Random.Range(0, rowIndex)];
            oddOneOutPillars[impostorPillar].SetSymbolSprite(correctSymbol, rowIndex);
        }

        void FillRest(int rowIndex) {
            ResetSymbols();
            foreach (var pillar in oddOneOutPillars) {
                pillar.SetSymbolSprite(correctSymbol, rowIndex);
            }

            for (int i = 0; i < rowIndex; i++) {
                oddOneOutPillars[_correctPillars[i]].SetSymbolSprite(incorrectSymbol, rowIndex);
            }
        }
    }

    private void PillarTouched(OddOneOutPillar touchedPillar) {
        int pillarIndex = 0;
        for (var i = 0; i < oddOneOutPillars.Length; i++) {
            var pillar = oddOneOutPillars[i];
            if (pillar == touchedPillar) pillarIndex = i;
        }

        if (_correctPillars[_currentRow] == pillarIndex) {
            //Correct pillar touched
            touchedPillar.PlaySound(sequenceSounds[_currentRow]);
            _currentRow++;
            if (_currentRow == 5) {
                onCompletion.Invoke();
                audioSource.clip = completionSound;
                audioSource.Play();
            }
        } else {
            //Incorrect pillar touched
            _currentRow = 0;
            audioSource.clip = failSound;
            audioSource.Play();
            foreach (var pillar in oddOneOutPillars) {
                pillar.Deactivate();
            }
        }
    }
    
    private T GetRandomFromListAndRemove<T>(List<T> list) {
        var randomItem = list[Random.Range(0, list.Count)];
        list.Remove(randomItem);
        return randomItem;
    }
}
