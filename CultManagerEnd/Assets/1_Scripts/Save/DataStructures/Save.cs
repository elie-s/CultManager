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
        public int currentDemon;

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

            cultIdIndex = _cultData ? _cultData.idIndex : default;
            cultists = _cultData ? _cultData.cultists.ToArray() : default;
            candidatesCount = _cultData ? _cultData.candidatesCount : default;
            maxCandidatesCount = _cultData ? _cultData.maxCandidatesCount : default;
            currentLevel = _cultData ? _cultData.currentlevel : default;

            influenceCurrentValue = _influenceData ? _influenceData.value : default;
            influenceCandidateTimeReference = _influenceData ? _influenceData.lastCandidateTimeReference : default;

            moneyValue = _moneyData ? _moneyData.money : default;
            relicValue = _moneyData ? _moneyData.relics : default;

            policeMaxValue = _policeData ? _policeData.max : default;
            policeCurrentValue = _policeData ? _policeData.value : default;
            lastHourReference = _policeData ? _policeData.lastHourReference : default;
            bribeValue = _policeData ? _policeData.bribeLevelValue : default;

            //altarParts = _altarData.altarParts.ToArray();
            //availableCultists = _altarData.availableCultists;
            //altarCompletion = _altarData.altarCompletion;
            //lastBuildTimeReference = _altarData.lastBuildTimeReference;

            noteTabSegments = _noteTabData ? _noteTabData.noteTabSegments.ToArray() : default;

            puzzle = _puzzleData ? _puzzleData.puzzle.ToArray() : default;
            puzzleLayers = _puzzleData ? _puzzleData.layers : default;

            bloodBanks = _bloodBankData ? _bloodBankData.bloodBanks : default;
            level = _bloodBankData ? _bloodBankData.level : default;

            demons = _demonData ? _demonData.demons.ToArray() : default;
            spawns = _demonData ? _demonData.spawns.ToArray() : default;
            demonIdIndex = _demonData ? _demonData.idIndex : default;
            spawnCount = _demonData ? _demonData.spawnCount : default;
            currentDemon = _demonData ? (int)_demonData.currentDemon : default;

            persistentDemons = _persistentDemonData ? _persistentDemonData.persistentDemons.ToArray() : default;
            persistentdemonIdIndex = _persistentDemonData ? _persistentDemonData.idIndex : default;

            storage = _modifierReference ? _modifierReference.storage : default;

            statueSetSaves = _statueData ? _statueData.SaveSets() : default;
            demonName = _statueData ? (int)_statueData.currentDemon : default;
            statueTimeRef = _statueData ? _statueData.timeRef : default;
            demonsBought = _statueData ? _statueData.DemonsBoughtToIntArray() : default;
        }
    }
}

