using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Parameters/InfluenceData")]
    public class InfluenceData : ScriptableObject, ILoadable
    {
        public IntGauge gauge;
        public int value => gauge.value;
        public int max => gauge.max;
        public float ratio => gauge.ratio;

        public DateTime lastCandidateTimeReference;

        public void Increment(int _value)
        {
            gauge.Increment(_value);
        }

        public void Decrement(int _value)
        {
            gauge.Decrement(_value);
        }

        public void Set(int _value)
        {
            gauge.SetValue(_value);
        }

        public void Reset(int _max)
        {
            gauge = new IntGauge(0, _max,true);
            gauge.SetValue(_max);
        }


        public void ResetData(int _max)
        {
            Reset(_max);
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
            gauge = new IntGauge(0, _save.influenceMaxValue, _save.influenceCurrentValue);
        }
    }
}

