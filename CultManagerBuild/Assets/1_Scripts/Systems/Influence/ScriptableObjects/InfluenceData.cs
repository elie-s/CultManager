using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Influence/Data")]
    public class InfluenceData : ScriptableObject
    {
        public uint value { get; private set; }
        public System.DateTime lastCandidateTimeReference { get; private set; }

        public void Increase(uint _value)
        {
            value += _value;
        }

        public void Decrease(uint _value)
        {
            value -= _value;
        }

        public void Set(uint _value)
        {
            value = _value;
        }

        public void SetCandidateTimeReference()
        {
            lastCandidateTimeReference = System.DateTime.Now;
        }

        public void Reset()
        {
            value = 100;
            SetCandidateTimeReference();
        }

        public void LoadSave(Save _save)
        {
            value = _save.influenceValue;
            lastCandidateTimeReference = _save.influenceCandidateTimeReference;
        }
    }
}