﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class PuzzleSetSaver : MonoBehaviour
    {
        [SerializeField] private PuzzleData puzzleData = default;
        [SerializeField] private PuzzleDisplay displayer = default;
        [SerializeField] private PuzzleDataSet.Level level = PuzzleDataSet.Level.One;
        [SerializeField] private int index = 0;
        [SerializeField, DrawScriptable] private PuzzleDataSet data = default;

        public void SaveCurrentPuzzle()
        {
            data.AddPuzzle(level, puzzleData.puzzle);
        }

        public void Display()
        {
            switch (level)
            {
                case PuzzleDataSet.Level.Tutorial:
                    if (data.tutorial.Count > 0) displayer.DisplayPuzzle(1.0f, data.tutorial);
                    else Debug.LogWarning("No saved tutorial.");
                    break;
                case PuzzleDataSet.Level.One:
                    if (data.levelOne.Count > 0) displayer.DisplayPuzzle(1.0f, data.levelOne[index]);
                    else Debug.LogWarning("No saved levelOne.");
                    break;
                case PuzzleDataSet.Level.Two:
                    if (data.levelOne.Count > 0) displayer.DisplayPuzzle(1.0f, data.levelTwo[index]);
                    else Debug.LogWarning("No saved levelTwo.");
                    break;
                case PuzzleDataSet.Level.Three:
                    if (data.levelOne.Count > 0) displayer.DisplayPuzzle(1.0f, data.levelThree[index]);
                    else Debug.LogWarning("No saved levelThree.");
                    break;
                case PuzzleDataSet.Level.For:
                    if (data.levelOne.Count > 0) displayer.DisplayPuzzle(1.0f, data.levelFor[index]);
                    else Debug.LogWarning("No saved levelFor.");
                    break;
                case PuzzleDataSet.Level.Five:
                    if (data.levelOne.Count > 0) displayer.DisplayPuzzle(1.0f, data.levelFive[index]);
                    else Debug.LogWarning("No saved levelFive.");
                    break;
                default:
                    break;
            }
        }
    }
}