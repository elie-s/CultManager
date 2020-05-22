using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class WaitingCandidates : MonoBehaviour
    {
        [SerializeField] private GameObject[] candidates = default;

        [SerializeField] private CultManager cult = default;

        int currentCandidates;

        private void Start()
        {
        }

        private void Update()
        {
            if (currentCandidates != cult.currentCandidatesDebug)
            {
                currentCandidates = cult.currentCandidatesDebug;
                DisplayCandidates(currentCandidates);
            }
            
        }

        public void DisplayCandidates(int amount)
        {
            for (int i = 0; i < candidates.Length; i++)
            {
                candidates[i].SetActive(false);
            }
            if (amount > 0 && amount <= candidates.Length)
            {
                for (int i = 0; i < amount; i++)
                {
                    candidates[i].SetActive(true);
                }
            }
            else if(amount>candidates.Length)
            {
                for (int i = 0; i < candidates.Length; i++)
                {
                    candidates[i].SetActive(true);
                }
            }
            
        }

    }
}

