using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CultManager
{
    public class StatueManager : MonoBehaviour
    {
        [SerializeField] private StatuesData data = default;
        [SerializeField] private DemonData demonData = default;
        [SerializeField] private CultManager cultManager = default;
        [SerializeField] private MoneyManager moneyManager = default;
        [SerializeField] private PuzzeManager puzzleManager = default;
        [SerializeField] private Transform statueParent = default;
        [SerializeField] private UnityEvent onPartCompleted = default;
        [SerializeField] private UnityEvent onStatueCompleted = default;
        [Header("Menu UI")]
        [SerializeField] private StatueMenuController statueMenu = default;

        
        private StatuePrefabManager statue;

        private void Start()
        {
            LoadStatue();
            UpdateAvailabilities();
        }

        private void Update()
        {
            Build();
        }

        public void OpenStatueMenu()
        {
            statueMenu.Open();
        }

        public void CloseStatueMenu()
        {
            statueMenu.Close();
        }

        public void LoadStatue()
        {
            if (data.currentDemon == DemonName.None) return;
            statue = data.currentStatueSet.Instantiate(statueParent.position, statueParent).GetComponent<StatuePrefabManager>();
        }

        public int AssignWorkers(int _amount, int _index)
        {
            int available = cultManager.CountAvailableCultist();
            int toAssign = _amount <= available ? _amount : available;

            return data.currentStatueSet.AssignWorkers(toAssign, _index);
        }

        public int AssignWorkers(int _amount)
        {
            int available = cultManager.CountAvailableCultist();
            int toAssign = _amount <= available ? _amount : available;

            return data.currentStatueSet.AssignWorkersCurrent(toAssign);
        }

        public int RemoveWorkers(int _amount, int _index)
        {
            return data.currentStatueSet.RemoveWorkers(_amount, _index);
        }

        public int RemoveWorkers(int _amount)
        {
            return data.currentStatueSet.RemoveWorkersCurrent(_amount);
        }

        public bool TryBuyPart()
        {
            if( moneyManager.TryBuy(data.currentStatueSet.currentPart.cost.money, data.currentStatueSet.currentPart.cost.relic))
            {
                data.currentStatueSet.currentPart.Buy();
                return true;
            }

            return false;
        }

        public bool TryBuyStatue(DemonName _demon)
        {
            if(moneyManager.TryBuy(data.GetStatueSet(_demon).cost, 0))
            {
                data.BuyStatue(_demon);
                LoadStatue();
                puzzleManager.GetDemonPuzzle();
                demonData.currentDemon = _demon;
                return true;
            }

            Debug.Log("!m: "+ data.GetStatueSet(_demon).cost);
            return false;
        }

        public void BreakStatue()
        {
            data.currentStatueSet.Break();
        }

        public void DemonSummoned()
        {
            Debug.Log("Demon Summoned");
            data.CurrentSummoned();
            UpdateAvailabilities();
        }

        public void Build()
        {
            if (data.currentDemon == DemonName.None)
            {
                data.UpdateTimeRef();
                return;
            }

            if (data.SecondsFromTimeRef() > 1.0f)
            {
                data.currentStatueSet.UpdateCompletion(data.baseWorkForce, data.SecondsFromTimeRef(), onPartCompleted.Invoke, onStatueCompleted.Invoke);
                data.UpdateTimeRef();
                statue.UpdateAllPartsSprites();
            }
        }

        public void UpdateAvailabilities()
        {
            for (int i = 0; i < 10; i++)
            {
                data.UpdateAvailability((DemonName)i);
            }
        }

        public void ResetData()
        {
            Debug.Log("Rest Statue");

            data.Reset();
        }
    }
}