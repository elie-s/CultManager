using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class InfluenceManager : MonoBehaviour
    {
        [SerializeField] private DebugInstance debug = default;
        public InfluenceData data;
        [SerializeField] private CultManager cultManager = default;

        private float candidatesFrequency => (float)data.value / 100.0f * 60.0f;

        void Update()
        {
            CandidatesHandler();
        }

        public void InitializeData()
        {
            if(!SaveManager.saveLoaded)data.Reset();
        }

        private void CandidatesHandler()
        {
            System.TimeSpan timeSpan = System.DateTime.Now - data.lastCandidateTimeReference;
            int candidatesToAdd = Mathf.FloorToInt((float)timeSpan.TotalSeconds / candidatesFrequency);

            debug.Log("Operation: " + System.DateTime.Now +" - "+ data.lastCandidateTimeReference, DebugInstance.Importance.Lesser);
            debug.Log(timeSpan.TotalSeconds, DebugInstance.Importance.Lesser);
            debug.Log("Candidates to add: " + candidatesToAdd, DebugInstance.Importance.Lesser);

            for (int i = 0; i < candidatesToAdd; i++)
            {
                SendCandidate();
            }

        }

        private void SendCandidate()
        {
            cultManager.IncreaseCandidates();
            data.SetCandidateTimeReference();
        }
    }
}