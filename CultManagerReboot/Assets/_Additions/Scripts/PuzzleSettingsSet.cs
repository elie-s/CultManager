using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CultManager.HexagonalGrid;


namespace CultManager
{
    [CreateAssetMenu(menuName = "Standalone/Generation Set")]
    public class PuzzleSettingsSet : ScriptableObject
    {
        public GenerationSet[] set;
    }

    [System.Serializable]
    public class GenerationSet
    {
        public PatternGenerationSettings[] puzzleSettings;
        public PatternGenerationSettings demonPattern;
    }
}