using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class InfluenceManager : MonoBehaviour
    {
        public InfluenceData data;
        [SerializeField] private CultManager cultManager = default;

        private float candidatesFrequency => (float)data.value / 100.0f * 60f;

        void Update()
        {
            CandidatesHandler();
        }

        [ContextMenu("ForedReset")]
        public void ForcedReset()
        {
            data.Reset();
        }

        public void InitializeData()
        {
            if (!SaveManager.saveLoaded) data.Reset();
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

