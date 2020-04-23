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

        public void LoadSave(Save _save)
        {
            puzzle = _save.puzzle.ToList();
        }
    }
}