using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Parameters/InfluenceData")]
    public class InfluenceData : ScriptableObject, ILoadable
    {
        public ulong value { get; private set; }

        public DateTime lastCandidateTimeReference;

        public void Increment(int _value)
        {
            value +=(ulong)value;
        }

        public void Decrement(int _value)
        {
            value -= (ulong) _value;
        }

        public void Reset(ulong _value)
        {
            value = (ulong)_value;
        }


        public void ResetData(ulong _value)
        {
            Reset(_value);
            ResetCandidateTimeReference();
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
            ResetCandidateTimeReference(_save.influenceCandidateTimeReference);
            value =  _save.influenceCurrentValue;
        }
    }
}

