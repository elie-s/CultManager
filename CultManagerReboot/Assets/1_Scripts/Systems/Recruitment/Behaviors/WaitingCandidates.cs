using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class WaitingCandidates : MonoBehaviour
    {
        [SerializeField] private GameObject[] candidates;

        [SerializeField] private CultData data=default;

        int currentCandidates => data.candidatesCount;

        private void Start()
        {
        }

        private void Update()
        {
            DisplayCandidates(currentCandidates);
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

