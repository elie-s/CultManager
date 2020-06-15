using CultManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class TinderManager : MonoBehaviour
    {
        [SerializeField] private CultData data = default;
        [SerializeField] private CultManager cultManager = default;
        [SerializeField] private UIGeneralManager uiManager = default;
        [SerializeField] private MoneyManager moneyManager = default;
        [SerializeField] private PoliceManager policeManager = default;
        [SerializeField] private ModifierReference reference = default;

        private Candidate currentCandidate;

        private Candidate CreateCandidate()
        {
            
            Cultist cultist = cultManager.CreateRandomCultist();
            Debug.Log(cultist == null ? "Cultist is null" : cultist.cultistName);
            int money = Mathf.RoundToInt(Random.Range(1, 101) * (1 + reference.storage.RecruitmentMoneyModifier));
            int police = Mathf.RoundToInt(Random.Range(1, 11) * (1 + reference.storage.RecruitmentPoliceModifier));

            return new Candidate(cultist, police, money);
        }

        public Candidate TryGetCandidate()
        {
            if (currentCandidate != null && currentCandidate.cultist != null)
            {
                return currentCandidate;
            }

            if (data.candidatesCount == 0)
            {
                Debug.Log("No candidates to display.");
                return null;
            }

            currentCandidate = CreateCandidate();

            return currentCandidate;
        }

        public void Accept(Candidate _candidate)
        {
            if (_candidate == null) return;

            cultManager?.AddCultists(_candidate.cultist);
            moneyManager?.Increase(_candidate.moneyValue, 0);
            policeManager?.Incerment(_candidate.policeValue);
            currentCandidate = null;
            cultManager?.DecreaseCandidates();

            uiManager.UpdateDisplayer();
        }

        public void AcceptCurrent()
        {
            Accept(currentCandidate);
        }

        public void Reject()
        {
            if (currentCandidate == null) return;

            currentCandidate = null;
            cultManager?.DecreaseCandidates();

            uiManager.UpdateDisplayer();
        }
    }
}