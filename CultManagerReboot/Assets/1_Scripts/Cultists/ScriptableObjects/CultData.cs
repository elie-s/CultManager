﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Cult/CultData")]
    public class CultData : ScriptableObject, ILoadable
    {
        public List<Cultist> cultists { get; private set; }
        public ulong idIndex { get; private set; }
        public int candidatesCount { get; private set; }
        public int maxCandidatesCount { get; private set; }

        public void Reset()
        {
            cultists = new List<Cultist>();
            candidatesCount = 10;
            maxCandidatesCount = 30;
        }

        public void ResetCultData(int level)
        {
            cultists = new List<Cultist>();
            candidatesCount = 10;
            maxCandidatesCount = 30;
        }


        public void ToggleOccupy()
        {
            for (int i = 0; i < cultists.Count; i++)
            {
                cultists[i].ToggleOccupy();
            }
        }

        public int FindOccupied()
        {
            int ctr = 0;
            for (int i = 0; i < cultists.Count; i++)
            {
                if (cultists[i].IsOccupied())
                    ctr++;
            }
            return ctr;
        }

        public void Initialize()
        {
            idIndex = 0;
            Reset();
        }

        public void LoadSave(Save _save)
        {
            cultists = _save.cultists.ToList();
            idIndex = _save.cultIdIndex;
            candidatesCount = _save.candidatesCount;
            maxCandidatesCount = _save.maxCandidatesCount;
        }

        public void AddCultist(Cultist _cultist)
        {
            cultists.Add(_cultist);
        }

        public void RemoveCultist(Cultist _cultist)
        {
            cultists.Remove(_cultist);
        }

        public Cultist GetCultist(ulong _id)
        {
            for (int i = 0; i < cultists.Count; i++)
            {
                if (cultists[i].id == _id) return cultists[i];
            }

            return null;
        }

        public Cultist CreateCultist(string _cultistName, int _spriteIndex)
        {
            Cultist result = new Cultist(idIndex, _cultistName, _spriteIndex);
            idIndex++;

            return result;
        }

        public void SetCandidatesCount(int _amount)
        {
            candidatesCount = Mathf.Clamp(_amount, 0, maxCandidatesCount);
        }

        public void SetMaxCandidatesCount(int _max)
        {
            maxCandidatesCount = _max;
        }

        public void AddCandidateToCount()
        {
            candidatesCount = Mathf.Clamp(candidatesCount + 1, 0, maxCandidatesCount);
        }

        public void RemoveCandidateFromCount()
        {
            candidatesCount = Mathf.Clamp(candidatesCount - 1, 0, maxCandidatesCount);
        }
    }
}

