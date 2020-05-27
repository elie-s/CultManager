using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CultManager.HexagonalGrid;
using HexGrid = CultManager.HexagonalGrid.HexGrid;


namespace CultManager
{
    [Serializable]
    public struct PuzzleSave
    {
        public int savingSystemVersion;
        public DateTime dateTime;

        //Puzzle Save Data
        public PuzzleGeneration[] generations;
        public int currentIndex;


        public PuzzleSave(int _savingSystemVersion, PuzzleSaveData _puzzleSaveData)
        {
            savingSystemVersion = _savingSystemVersion;
            dateTime = DateTime.Now;

            generations = _puzzleSaveData.generations.ToArray();
            currentIndex = _puzzleSaveData.currentIndex;

        }
    }
}