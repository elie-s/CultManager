using System.Collections;
using System.Collections.Generic;
using CultManager.HexagonalGrid;
using System.Linq;
using UnityEngine;


namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Puzzle/PuzzleSaveData")]
    public class PuzzleSaveData : ScriptableObject
    {
        public List<PuzzleGeneration> generations;
        public int currentIndex;

        public void AddGeneration(PatternGenerationSettings[] generationSettings, PuzzleSegment[] puzzle)
        {
            PuzzleGeneration generation = new PuzzleGeneration(currentIndex, generationSettings, puzzle);
            generations.Add(generation);
            currentIndex++;
            Debug.Log(currentIndex);
        }

        public void Increment()
        {
            currentIndex ++;
        }

        public void LoadSave(PuzzleSave _puzzleSave)
        {
            generations = _puzzleSave.generations.ToList();
            currentIndex = _puzzleSave.currentIndex;
        }

        public void ResetData()
        {
            generations = new List<PuzzleGeneration>();
            currentIndex = 0;
            Debug.Log(currentIndex);
        }
    }
}

