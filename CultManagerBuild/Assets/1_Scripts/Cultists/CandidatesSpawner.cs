using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class CandidatesSpawner : MonoBehaviour
    {
        [SerializeField] private CultData cultData = default;
        [SerializeField] private GameObject[] candidates = default;

        private int lastCount = 0;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (lastCount != cultData.candidatesCount) DisplayCandidates();
        }

        private void DisplayCandidates()
        {
            lastCount = cultData.candidatesCount;

            for (int i = 0; i < cultData.maxCandidatesCount; i++)
            {
                candidates[i].SetActive(i < lastCount);
            }
        }
    }
}