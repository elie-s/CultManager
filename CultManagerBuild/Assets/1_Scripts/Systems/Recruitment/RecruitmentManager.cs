﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class RecruitmentManager : MonoBehaviour
    {
        [SerializeField] private CultManager cultManager = default;
        [SerializeField] private Transform cardParents = default;
        [SerializeField, DrawScriptable] private RecruitmentManagerSettings settings = default;

        private GameObject[] cards;
        private Cultist[] cultistProposed;
        private List<Cultist> cultistKept;
        private int index;

        private const Room room = Room.Recruitment;

        void Start()
        {

        }

        [ContextMenu("New List")]
        public void NewCardList()
        {
            cultistKept = new List<Cultist>();
            cards = new GameObject[settings.propositionsAmount];
            cultistProposed = new Cultist[settings.propositionsAmount];

            for (int i = 0; i < settings.propositionsAmount; i++)
            {
                NewCard(i);
            }

            index = 0;
            NextCard();
        }

        private void NewCard(int _index)
        {
            cultistProposed[_index] = cultManager.CreateRandomCultist();
            GameObject card = Instantiate(settings.recruitmentCardPrefab, cardParents);
            RecruitmentCardBehaviour cardBehaviour = card.GetComponent<RecruitmentCardBehaviour>();
            cardBehaviour.SetCallbacks(CardLeft, CardKept);
            cardBehaviour.SetCultist(cultistProposed[_index]);
            cards[_index] = card;
            card.SetActive(false);
        }

        private void CardKept()
        {
            Destroy(cards[index]);
            cultistKept.Add(cultistProposed[index]);
            index++;
            NextCard();
        }

        private void CardLeft()
        {
            Destroy(cards[index]);
            index++;
            NextCard();
        }

        private void NextCard()
        {
            if (index < cards.Length) cards[index].SetActive(true);
        }
    }
}