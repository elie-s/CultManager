using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CultManager.HexagonalGrid;
using HexGrid = CultManager.HexagonalGrid.HexGrid;


namespace CultManager
{
    [Serializable]
    public struct Save
    {
        public int savingSystemVersion;
        public DateTime dateTime;

        //Note Tab
        public NoteTabSegment[] noteTabSegments;

        //Puzzle Data
        public PuzzleSegment[] puzzle;
        public int puzzleLayers;

        //Blood Bank Data
        public BloodBank[] bloodBanks;
        public int level;

        //Demon Data
        public Demon[] demons;
        public Spawn[] spawns;
        public int demonIdIndex;
        public int spawnCount;

        //Persistent Demon Data
        public PersistentDemon[] persistentDemons;
        public int persistentdemonIdIndex;

        //Modifier Database
        public ModifierStorage storage;

        public Save(int _savingSystemVersion, NoteTabData _noteTabData, PuzzleData _puzzleData,
            BloodBankData _bloodBankData,DemonData _demonData,PersistentDemonData _persistentDemonData,ModifierReference _modifierReference)
        {
            savingSystemVersion = _savingSystemVersion;
            dateTime = DateTime.Now;

            noteTabSegments = _noteTabData.noteTabSegments.ToArray();

            puzzle = _puzzleData.puzzle.ToArray();
            puzzleLayers = _puzzleData.layers;

            bloodBanks = _bloodBankData.bloodBanks;
            level = _bloodBankData.level;

            demons = _demonData.demons.ToArray();
            spawns = _demonData.spawns.ToArray();
            demonIdIndex = _demonData.idIndex;
            spawnCount = _demonData.spawnCount;

            persistentDemons = _persistentDemonData.persistentDemons.ToArray();
            persistentdemonIdIndex = _persistentDemonData.idIndex;

            storage = _modifierReference.storage;
        }
    }
}

