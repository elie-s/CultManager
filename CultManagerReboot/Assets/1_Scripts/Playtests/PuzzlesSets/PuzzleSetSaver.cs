using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class PuzzleSetSaver : MonoBehaviour
    {
        [SerializeField] private PuzzleData puzzleData = default;
        [SerializeField] private PuzzleDisplay displayer = default;
        [SerializeField] private DemonName demon = DemonName.Mortimer;
        [SerializeField, DrawScriptable] private PuzzleDataSet data = default;

        public void SaveCurrentPuzzle()
        {
            data.AddPuzzle(demon, puzzleData.puzzle);
        }

        public void Display()
        {
            displayer.DisplayPuzzle(1.0f, data.GetPuzzle(demon));
        }
    }
}