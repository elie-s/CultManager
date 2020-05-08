﻿using System.Collections;
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
        [SerializeField] private RecruitmentCardBehavior cardDisplay = default;
        [SerializeField] private RecruitmentCardMovement cardMovement = default;

        public GameObject recruitmentObj;
        private Candidate currentCandidate;

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
            cardMovement.ResetValues();
            if(currentCandidate==null)
            currentCandidate = CreateCandidate();
            Sprite sprite = settings.cultistThumbnails[currentCandidate.cultist.spriteIndex];
            string name = currentCandidate.cultist.cultistName;
            int age = currentCandidate.cultist.age;
            int policeValue = currentCandidate.policeValue;
            int moneyValue = currentCandidate.moneyValue;
            BloodType type = currentCandidate.cultist.blood;
            cardDisplay.Display(sprite, name, age, policeValue, moneyValue,type);
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
                StartCoroutine(DelayedAction(NextCard, 0.3f));
            }
            else
            {
                StopRecruitment();
            }
        }

        private IEnumerator DelayedAction(System.Action _action, float _delay)
        {
            yield return new WaitForSeconds(_delay);

            _action();
        }

        [ContextMenu("Recruit")]
        public void StartRecruitment()
        {
            if (GameManager.currentPanel == CurrentPanel.None)
            {
                GameManager.currentPanel = thisPanelName;
                Debug.Log("Candidates are " + data.candidatesCount);
                StartCoroutine(DelayedAction(EnableCard, 0.3f));
                SetCard();
            }
        }

        [ContextMenu("Close Recruitment")]
        public void StopRecruitment()
        {
            if (GameManager.currentPanel == thisPanelName)
            {
                DisableCard();
                GameManager.currentPanel = CurrentPanel.None;
            }
        }

        private void EnableCard()
        {
            cardDisplay.gameObject.SetActive(true);
        }

        private void DisableCard()
        {
            cardDisplay.gameObject.SetActive(false);
        }
    }
}

