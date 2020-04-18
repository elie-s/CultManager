using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class RecruitmentManager : MonoBehaviour
    {
        [SerializeField] private CultData data = default;
        [SerializeField] private CultSettings settings = default;
        [SerializeField] private GameManager gameManager = default;
        [SerializeField] private CultManager cultManager = default;
        [SerializeField] private MoneyManager money = default;
        [SerializeField] private PoliceManager police = default;

        [SerializeField] private CurrentPanel thisPanelName;


        public GameObject card;
        public GameObject recruitmentObj;
        private Candidate currentCandidate;
        private RecruitmentCardBehavior cardDisplay;

        void Start()
        {
            cardDisplay = card.GetComponent<RecruitmentCardBehavior>();
        }

        private Candidate CreateCandidate()
        {
            Cultist cultist = cultManager.CreateRandomCultist();
            int money = Random.Range(0, 101);
            int police = Random.Range(0, 101);

            return new Candidate(cultist, police, money);
        }

        public void Kept()
        {
            UpdateCult();
            currentCandidate = null;
            cultManager.DecreaseCandidates();
            SetCard();
        }

        public void Left()
        {
            currentCandidate = null;
            cultManager.DecreaseCandidates();
            SetCard();
        }

        public void NextCard()
        {
            currentCandidate = CreateCandidate();
            Sprite sprite = settings.cultistThumbnails[currentCandidate.cultist.spriteIndex];
            string name = currentCandidate.cultist.cultistName;
            int age = currentCandidate.cultist.age;
            int policeValue = currentCandidate.policeValue;
            int moneyValue = currentCandidate.moneyValue;
            CultistTraits traits = currentCandidate.cultist.traits;
            cardDisplay.Display(sprite, name, age, policeValue, moneyValue,traits);
        }

        private void UpdateCult()
        {
            if (currentCandidate!=null)
            {
                cultManager.AddCultists(currentCandidate.cultist);
                police?.Incerment(currentCandidate.policeValue);
                money?.Increase(currentCandidate.moneyValue);
            }
            
        }

        void SetCard()
        {
            if (data.candidatesCount > 0)
            {
                NextCard();
            }
            else
            {
                StopRecruitment();
            }
        }

        [ContextMenu("Recruit")]
        public void StartRecruitment()
        {
            if (GameManager.currentPanel == CurrentPanel.None)
            {
                GameManager.currentPanel = thisPanelName;
                Debug.Log("Candidates are " + data.candidatesCount);
                card.SetActive(true);
                SetCard();
            }
        }
        [ContextMenu("Close Recruitment")]
        public void StopRecruitment()
        {
            if (GameManager.currentPanel == thisPanelName)
            {
                card.SetActive(false);
                GameManager.currentPanel = CurrentPanel.None;
            }
        }
    }
}

