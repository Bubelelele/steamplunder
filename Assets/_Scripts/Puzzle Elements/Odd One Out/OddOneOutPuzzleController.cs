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

    private void Awake() {
        if (oddOneOutPillars.Length < 5) {
            Debug.Log("Odd One Out puzzle needs at least 5 pillars!");
            Destroy(this);
        }
        Scramble();
    }

    private void Scramble() {
        int[] correctPillars = new int[5];
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
        
        //Row 5 - 3 different symbols (2 pairs and 1 unique). Correct one is in one of the pairs
        TwoPairs(4);
        
        void AssignRandomCorrectPillars() {
            List<int> rowIndexes = new List<int> {0, 1, 2, 3, 4};
            for (int i = 0; i < correctPillars.Length; i++) {
                correctPillars[i] = GetRandomFromListAndRemove(rowIndexes);
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
            oddOneOutPillars[correctPillars[rowIndex]].SetSymbolSprite(correctSymbol, rowIndex);
        }

        void TwoAreDifferent(int rowIndex) {
            ResetSymbols();
            if (rowIndex < 1) {
                Debug.Log("TwoAreDifferent cannot be used earlier than line 2");
                return;
            }
            OneIsDifferent(rowIndex);
            int impostorPillar = correctPillars[Random.Range(0, rowIndex)];
            oddOneOutPillars[impostorPillar].SetSymbolSprite(correctSymbol, rowIndex);
        }

        void FillRest(int rowIndex) {
            ResetSymbols();
            foreach (var pillar in oddOneOutPillars) {
                pillar.SetSymbolSprite(correctSymbol, rowIndex);
            }

            for (int i = 0; i < rowIndex; i++) {
                oddOneOutPillars[correctPillars[i]].SetSymbolSprite(incorrectSymbol, rowIndex);
            }
        }

        void TwoPairs(int rowIndex) {
            ResetSymbols();
            OneIsDifferent(rowIndex);
            int randomInPair;
            do {
                randomInPair = Random.Range(0, 5);
            } while (randomInPair == correctPillars[rowIndex]);
            oddOneOutPillars[randomInPair].SetSymbolSprite(correctSymbol, rowIndex);
            int randomUnique;
            do {
                randomUnique = Random.Range(0, 5);
            } while (randomUnique == correctPillars[rowIndex]);
            oddOneOutPillars[randomInPair].SetSymbolSprite(correctSymbol, rowIndex);
            List<int> pillars = new List<int> {0, 1, 2, 3, 4};
            for (int i = 0; i < 5; i++) {
                correctPillars[i] = GetRandomFromListAndRemove(pillars);
            }
        }
    }
    
    private T GetRandomFromListAndRemove<T>(List<T> list) {
        var randomItem = list[Random.Range(0, list.Count)];
        list.Remove(randomItem);
        return randomItem;
    }
}
