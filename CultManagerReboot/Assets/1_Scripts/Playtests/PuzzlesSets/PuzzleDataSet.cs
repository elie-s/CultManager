 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CultManager.HexagonalGrid;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Puzzle/SaveDataSet")]
    public class PuzzleDataSet : ScriptableObject
    {
        public bool save;
        public List<PuzzleSegment>[] demonsPuzzles;
        public bool[] added;


        public void AddPuzzle(DemonName _demon, List<PuzzleSegment> _puzzle)
        {
            if (demonsPuzzles == null)
            {
                demonsPuzzles = new List<PuzzleSegment>[10];
                added = new bool[10];
            }

            demonsPuzzles[(int)_demon] = _puzzle;
            added[(int)_demon] = true;

            save = false;
        }

        public void CheckAdd()
        {
            for (int i = 0; i < 10; i++)
            {
                added[i] = demonsPuzzles[i] != null;
            }
        }

        public List<PuzzleSegment> GetPuzzle(DemonName _demon)
        {
            Debug.Log("Puzzle gotten");

            return demonsPuzzles[(int)_demon];
        }
    }
}