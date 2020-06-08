using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class InfluenceManager : MonoBehaviour
    {
        public InfluenceData data;
        [SerializeField] private CultManager cultManager = default;
        [SerializeField] private UIGeneralManager uiManager = default;

        private float candidatesFrequency => data.value / 100.0f * 60f;
        public int value => (int)data.value;

        

        public void Increase(int _value)
        {
            data.Increment(_value);
            uiManager?.UpdateDisplayer();
        }

        public void Decrease(int _value)
        {
            data.Decrement(_value);
            uiManager?.UpdateDisplayer();
        }

        public void ResetValue(ulong _value)
        {
            data.Reset(_value);
            uiManager?.UpdateDisplayer();
        }

        [ContextMenu("Reset")]
        public void ResetData()
        {
            data.ResetData(100);
            uiManager?.UpdateDisplayer();
        }

        void Update()
        {
            CandidatesHandler();
        }

        [ContextMenu("ForedReset")]
        public void ForcedReset()
        {
            data.ResetData(100);
            uiManager?.UpdateDisplayer();
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

                Debug.Log("Candidate added");
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

