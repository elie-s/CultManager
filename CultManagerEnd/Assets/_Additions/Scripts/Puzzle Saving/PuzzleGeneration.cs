using System.Collections;
using System.Collections.Generic;
using CultManager.HexagonalGrid;
using UnityEngine;


namespace CultManager
{
    [System.Serializable]
    public class PuzzleGeneration
    {
        public int generationIndex;
        public PatternGenerationSettings[] generationSettings;
        public PuzzleSegment[] puzzle;
        public List<Demon> attemptDemons=new List<Demon>();

        public PuzzleGeneration(int _generationIndex,PatternGenerationSettings[] _generationSettings,PuzzleSegment[] _puzzle)
        {
            generationIndex = _generationIndex;
            generationSettings = _generationSettings;
            puzzle = _puzzle;
        }

        public void AddAttempt(Demon _attemptDemon)
        {
            attemptDemons.Add(_attemptDemon);
        }
    }
}

