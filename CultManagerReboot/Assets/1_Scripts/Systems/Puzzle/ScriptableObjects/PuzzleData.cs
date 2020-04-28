using CultManager.HexagonalGrid;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Puzzle/PuzzleData")]
    public class PuzzleData : ScriptableObject, ILoadable
    {
        public List<PuzzleSegment> puzzle;

        public void ClearSelections()
        {
            for (int i = 0; i < puzzle.Count; i++)
            {
                puzzle[i].DisableSegment();
                puzzle[i].selected = false;
            }
        }
        public Segment[] GatherPatternSegments()
        {
            List<Segment> result = new List<Segment>();
            for (int i = 0; i < puzzle.Count; i++)
            {
                if (puzzle[i].patternSegment)
                {
                    result.Add(puzzle[i].segment);
                }
            }
            return result.ToArray();
        }


        public void LoadSave(Save _save)
        {
            puzzle = _save.puzzle.ToList();
        }
    }
}