using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CultManager.HexagonalGrid;
using HexGrid = CultManager.HexagonalGrid.HexGrid;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Puzzle/PuzzleData")]
    public class PuzzleData : ScriptableObject, ILoadable
    {
        public HexGrid grid;
        public Pattern pattern;

        public List<PuzzleSegment> puzzle;

        public void LoadSave(Save _save)
        {
            grid = _save.puzzleGrid;
            pattern = new Pattern(_save.puzzlePatternSegments, grid);
        }
    }
}