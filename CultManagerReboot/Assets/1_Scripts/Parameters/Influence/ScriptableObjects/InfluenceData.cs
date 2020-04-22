using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Parameters/InfluenceData")]
    public class InfluenceData : ScriptableObject, ILoadable
    {
        public uint value { get; private set; }
        public DateTime lastCandidateTimeReference { get; private set; }

        public void Increase(uint _value)
        {
            value += _value;
        }

        public void Decrease(uint _value)
        {
            value -= _value;
        }

        public void Reset()
        {
            value = 100;
            ResetCandidateTimeReference();
        }

        public void Reset(uint _value)
        {
            value = _value;
        }

        public void ResetCandidateTimeReference()
        {
            lastCandidateTimeReference = System.DateTime.Now;
        }

        public void ResetCandidateTimeReference(DateTime _dateTime)
        {
            lastCandidateTimeReference = _dateTime;
        }

        public void LoadSave(Save _save)
        {
            Reset(_save.influenceValue);
            ResetCandidateTimeReference(_save.influenceCandidateTimeReference);
        }
    }
}

