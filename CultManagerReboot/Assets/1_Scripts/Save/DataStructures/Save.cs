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

        //Cult Data
        public ulong cultIdIndex;
        public Cultist[] cultists;
        public int candidatesCount;
        public int maxCandidatesCount;
        public int currentLevel;

        //Influence Data
        public ulong influenceCurrentValue;
        public DateTime influenceCandidateTimeReference;

        //MoneyData
        public uint moneyValue;
        public uint relicValue;

        //Police Data
        public int policeMaxValue;
        public int policeCurrentValue;
        public DateTime lastHourReference;
        public int bribeValue;

        //Altar Data
        //public AltarPart[] altarParts;
        //public int availableCultists;
        //public bool altarCompletion;
        //public DateTime lastBuildTimeReference;

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

        //Statue
        public StatueSetSave[] statueSetSaves;
        public int demonName;
        public DateTime statueTimeRef;
        public int[] demonsBought;

        public Save(int _savingSystemVersion, CultData _cultData, InfluenceData _influenceData, MoneyData _moneyData,
            PoliceData _policeData, NoteTabData _noteTabData, PuzzleData _puzzleData,
            BloodBankData _bloodBankData, DemonData _demonData, PersistentDemonData _persistentDemonData, ModifierReference _modifierReference, StatuesData _statueData)
        {
            savingSystemVersion = _savingSystemVersion;
            dateTime = DateTime.Now;

            cultIdIndex = _cultData.idIndex;
            cultists = _cultData.cultists.ToArray();
            candidatesCount = _cultData.candidatesCount;
            maxCandidatesCount = _cultData.maxCandidatesCount;
            currentLevel = _cultData.currentlevel;

            influenceCurrentValue = _influenceData.value;
            influenceCandidateTimeReference = _influenceData.lastCandidateTimeReference;

            moneyValue = _moneyData.money;
            relicValue = _moneyData.relics;

            policeMaxValue = _policeData.max;
            policeCurrentValue = _policeData.value;
            lastHourReference = _policeData.lastHourReference;
            bribeValue = _policeData.bribeLevelValue;

            //altarParts = _altarData.altarParts.ToArray();
            //availableCultists = _altarData.availableCultists;
            //altarCompletion = _altarData.altarCompletion;
            //lastBuildTimeReference = _altarData.lastBuildTimeReference;

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

            statueSetSaves = _statueData.SaveSets();
            demonName = (int)_statueData.currentDemon;
            statueTimeRef = _statueData.timeRef;
            demonsBought = _statueData.DemonsBoughtToIntArray();
        }
    }
}

