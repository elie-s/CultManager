using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class RecruitmentManager2 : MonoBehaviour
    {
        [SerializeField] private CultData data = default;
        [SerializeField] private CultManager cultManager = default;
        [SerializeField] private MoneyManager money = default;
        [SerializeField] private PoliceManager police = default;
        [SerializeField] private Transform cardParents = default;
        [SerializeField, DrawScriptable] private RecruitmentManagerSettings settings = default;

        private GameObject currentCard;
        private Candidate currentCandidate;

        private void NewCard()
        {
            RecruitmentCardBehaviour card = Instantiate(settings.recruitmentCardPrefab, cardParents).GetComponent<RecruitmentCardBehaviour>();
            card.SetCallbacks(CardLeft, CardKept);
            currentCandidate = CreateCandidate();
            card.SetCandidate(currentCandidate);
            card.SetPhoto(settings.recruitmentPhotos[currentCandidate.cultist.spriteIndex]);
            currentCard = card.gameObject;
        }

        private Candidate CreateCandidate()
        {
            Cultist cultist = cultManager.CreateRandomCultist();
            int money = Random.Range(0, 101);
            int police = Random.Range(0, 101);

            return new Candidate(cultist, police, money);
        }

        private void CardKept()
        {
            Destroy(currentCard);
            UpdateCult();
            cultManager.DecreaseCandidates();
            SetCard();
        }

        private void CardLeft()
        {
            Destroy(currentCard);
            cultManager.DecreaseCandidates();
            SetCard();
        }

        [ContextMenu("SetCard")]
        public void SetCard()
        {
            if (data.candidatesCount > 0) NewCard();
        }

        private void UpdateCult()
        {
            cultManager.AddCultists(currentCandidate.cultist);
            police?.Incerment(currentCandidate.policeValue);
            money?.Increase(currentCandidate.money);
        }
    }
}