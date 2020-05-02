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

        //Influence Data
        public uint influenceValue;
        public DateTime influenceCandidateTimeReference;

        //MoneyData
        public uint moneyValue;

        //Police Data
        public int policeMaxValue;
        public int policeCurrentValue;

        //Altar Data
        public AltarPart[] altarParts;
        public int availableCultists;
        public bool altarCompletion;
        public DateTime lastBuildTimeReference;

        //Note Tab
        public NoteTabSegment[] noteTabSegments;

        //Puzzle Data
        public PuzzleSegment[] puzzle;

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

        public Save(int _savingSystemVersion, CultData _cultData, InfluenceData _influenceData, MoneyData _moneyData,
            PoliceData _policeData, AltarData _altarData,NoteTabData _noteTabData, PuzzleData _puzzleData,
            BloodBankData _bloodBankData,DemonData _demonData,PersistentDemonData _persistentDemonData)
        {
            savingSystemVersion = _savingSystemVersion;
            dateTime = DateTime.Now;

            cultIdIndex = _cultData.idIndex;
            cultists = _cultData.cultists.ToArray();
            candidatesCount = _cultData.candidatesCount;
            maxCandidatesCount = _cultData.maxCandidatesCount;

            influenceValue = _influenceData.value;
            influenceCandidateTimeReference = _influenceData.lastCandidateTimeReference;

            moneyValue = _moneyData.value;

            policeMaxValue = _policeData.max;
            policeCurrentValue = _policeData.value;

            altarParts = _altarData.altarParts;
            availableCultists = _altarData.availableCultists;
            altarCompletion = _altarData.altarCompletion;
            lastBuildTimeReference = _altarData.lastBuildTimeReference;

            noteTabSegments = _noteTabData.noteTabSegments.ToArray();

            puzzle = _puzzleData.puzzle.ToArray();

            bloodBanks = _bloodBankData.bloodBanks;
            level = _bloodBankData.level;

            demons = _demonData.demons.ToArray();
            spawns = _demonData.spawns.ToArray();
            demonIdIndex = _demonData.idIndex;
            spawnCount = _demonData.spawnCount;

            persistentDemons = _persistentDemonData.persistentDemons.ToArray();
            persistentdemonIdIndex = _persistentDemonData.idIndex;

        }
    }
}

