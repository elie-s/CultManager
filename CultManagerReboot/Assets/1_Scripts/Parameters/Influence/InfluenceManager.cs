using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class InfluenceManager : MonoBehaviour
    {
        public InfluenceData data;
        [SerializeField] private CultManager cultManager = default;

        private float candidatesFrequency => data.value / 100.0f * 60f;
        public int value => data.value;

        

        public void Increase(int _value)
        {
            data.Increment(_value);
        }

        public void Decrease(int _value)
        {
            data.Decrement(_value);
        }

        public void ResetValue(int _value)
        {
            data.Set(_value);
        }

        public void ResetData()
        {
            data.ResetData(100);
        }

        void Update()
        {
            CandidatesHandler();
        }

        [ContextMenu("ForedReset")]
        public void ForcedReset()
        {
            data.ResetData(100);
        }

        public void InitializeData()
        {
            if (!SaveManager.saveLoaded) data.ResetData(100);
        }

        private void CandidatesHandler()
        {
            System.TimeSpan timeSpan = System.DateTime.Now - data.lastCandidateTimeReference;
            int candidatesToAdd = Mathf.FloorToInt((float)timeSpan.TotalSeconds / candidatesFrequency);

            for (int i = 0; i < candidatesToAdd; i++)
            {
                SendCandidate();
            }

        }

        [ContextMenu("SendCandidate")]
        private void SendCandidate()
        {
            cultManager.IncreaseCandidates();
            data.ResetCandidateTimeReference();
        }
    }
}

