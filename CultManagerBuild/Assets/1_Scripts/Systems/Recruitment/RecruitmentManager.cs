using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class RecruitmentManager : MonoBehaviour
    {
        [SerializeField] private CultData data = default;
        [SerializeField] private CultManager cultManager = default;
        [SerializeField] private GameObject seeList = default;
        [SerializeField] private GameObject sendRecruitButton = default;
        [SerializeField] private Transform cardParents = default;
        [SerializeField] private RoomsManager roomsManager = default;
        [SerializeField, DrawScriptable] private RecruitmentManagerSettings settings = default;

        private GameObject[] cards;
        private Cultist[] cultistProposed;
        private List<Cultist> cultistKept;
        private int index;
        private int maxSelected;

        private const Room room = Room.Recruitment;

        void Start()
        {

        }

        private void Update()
        {
            CheckAvailability();
        }

        [ContextMenu("New List")]
        public void NewCardList()
        {
            roomsManager.UnregisterCultistsFromRoom(room);

            cultistKept = new List<Cultist>();
            cards = new GameObject[settings.propositionsAmount];
            cultistProposed = new Cultist[settings.propositionsAmount];

            for (int i = 0; i < settings.propositionsAmount; i++)
            {
                NewCard(i);
            }

            index = 0;
            maxSelected = settings.validations;
            NextCard();
        }

        public void NewCardList(int _proposition, int _selection)
        {
            cultistKept = new List<Cultist>();
            cards = new GameObject[_proposition];
            cultistProposed = new Cultist[_proposition];

            for (int i = 0; i < _proposition; i++)
            {
                NewCard(i);
            }

            index = 0;
            maxSelected = _selection;
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
            if (index < cards.Length && cultistKept.Count < maxSelected) cards[index].SetActive(true);
            else AddToCult();
        }

        private void AddToCult()
        {
            cultManager.AddCultists(cultistKept.ToArray());
        }

        private void CheckAvailability()
        {
            if(data && data.roomsRegistrations[(int)room].hasPassed)
            {
                if (!seeList.activeSelf) seeList.SetActive(true);
            }
            else if ( data && data.roomsRegistrations[(int)room].isEmpty)
            {
                if (!sendRecruitButton.activeSelf) sendRecruitButton.SetActive(true);
            }
            else
            {
                if (seeList.activeSelf) seeList.SetActive(false);
                if (sendRecruitButton.activeSelf) sendRecruitButton.SetActive(false);
            }
        }
    }
}