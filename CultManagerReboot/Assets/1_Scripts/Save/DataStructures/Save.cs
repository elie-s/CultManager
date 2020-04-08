using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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


        public Save(int _savingSystemVersion, CultData _cultData, InfluenceData _influenceData, MoneyData _moneyData, PoliceData _policeData, AltarData _altarData)
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
        }
    }
}

