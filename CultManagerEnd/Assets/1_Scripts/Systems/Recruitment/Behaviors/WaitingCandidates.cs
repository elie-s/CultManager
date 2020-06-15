using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class WaitingCandidates : MonoBehaviour
    {
        [SerializeField] private GameObject[] candidates = default;

        [SerializeField] private CultData data = default;

        private void Update()
        {
            DisplayCandidates(data.candidatesCount);
        }

        public void DisplayCandidates(int amount)
        {
            for (int i = 0; i < candidates.Length; i++)
            {
                candidates[i].SetActive(false);
            }

            for (int i = 0; i < Mathf.Clamp(amount, 0, candidates.Length); i++)
            {
                candidates[i].SetActive(true);
            }
        }

    }
}

