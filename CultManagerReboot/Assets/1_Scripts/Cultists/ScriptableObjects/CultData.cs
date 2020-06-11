using System.Collections.Generic;
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
        public int currentlevel=0;

        public void Reset()
        {
            cultists = new List<Cultist>();
            candidatesCount = 10;
            maxCandidatesCount = 500;
            currentlevel = 0;
        }

        public void ResetCultData(int level)
        {
            cultists = new List<Cultist>();
            switch (level)
            {
                case 0:
                    {
                        candidatesCount = 5;
                        maxCandidatesCount = 15;
                    }
                    break;
                case 1:
                    {
                        candidatesCount = 10;
                        maxCandidatesCount = 15;
                    }
                    break;
                case 2:
                    {
                        candidatesCount = 10;
                        maxCandidatesCount = 16;
                    }
                    break;
                case 3:
                    {
                        candidatesCount = 10;
                        maxCandidatesCount = 18;
                    }
                    break;
                case 4:
                    {
                        candidatesCount = 10;
                        maxCandidatesCount = 20;
                    }
                    break;
                case 5:
                    {
                        candidatesCount = 10;
                        maxCandidatesCount = 25;
                    }
                    break;
            }
        }

        public void UpdateLevel()
        {
            currentlevel++;
        }

        public void SetCandidates(int _value)
        {
            candidatesCount = _value;
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
                if (cultists[i].working)
                    ctr++;
            }
            return ctr;
        }

        public int AvailableCultists()
        {
            return cultists.Count - FindOccupied();
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
            currentlevel = _save.currentLevel;
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

