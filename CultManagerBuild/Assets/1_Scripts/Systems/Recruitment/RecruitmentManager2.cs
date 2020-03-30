using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class RecruitmentManager2 : MonoBehaviour
    {
        [SerializeField] private CultData data = default;
        [SerializeField] private GameManager gameManager = default;
        [SerializeField] private CultManager cultManager = default;
        [SerializeField] private MoneyManager money = default;
        [SerializeField] private PoliceManager police = default;
        [SerializeField] private Transform cardParents = default;
        [SerializeField] private GameObject closeButton = default;
        [SerializeField] private GameObject interactableCollider = default;
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
            currentCard = null;
            UpdateCult();
            cultManager.DecreaseCandidates();
            SetCard();
        }

        private void CardLeft()
        {
            Destroy(currentCard);
            currentCard = null;
            cultManager.DecreaseCandidates();
            SetCard();
        }
        
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

        [ContextMenu("Open")]
        public void Open()
        {
            StartCoroutine(DelayedOpen());

            //if (currentCard) currentCard.SetActive(true);
            //else SetCard();

            //closeButton.SetActive(true);
            //gameManager.DisableCamController();
        }

        public void Close()
        {
            if (currentCard) currentCard.SetActive(false);

            closeButton.SetActive(false);
            //gameManager.EnableCamController();
            gameManager.SetCameraState(CameraController.CameraState.Default);
            interactableCollider?.SetActive(true);
        }

        private IEnumerator DelayedOpen()
        {
            gameManager.SetCameraState(CameraController.CameraState.Candidates);
            interactableCollider?.SetActive(false);
            
            yield return new WaitForSeconds(1.0f);

            if(currentCard) currentCard.SetActive(true);
            else SetCard();

            closeButton.SetActive(true);
            //gameManager.DisableCamController();
        }
    }
}