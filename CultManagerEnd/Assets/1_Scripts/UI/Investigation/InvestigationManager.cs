using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class InvestigationManager : MonoBehaviour
    {
        [SerializeField] private PoliceData data = default;
        [SerializeField] private MoneyManager moneyManager = default;
        [SerializeField] private UIGeneralManager uiManager = default;
        [Header("Settings")]
        [SerializeField] private float bribeRatio = 0.05f;
        [SerializeField] private float investigationEventFrequency = 1.0f;
        

        private int currentBribeLevel = 0;
        private Dictionary<Cultist, InvestigatorBehaviour> investigators = new Dictionary<Cultist, InvestigatorBehaviour>();
        private List<Cultist> investigatorsList = new List<Cultist>();

        private int currentBribeValue => currentBribeLevel * data.bribeLevelValue;
        public float totalRatio => currentBribeLevel * bribeRatio;

        private void Start()
        {
            
        }


        public int GetBribe()
        {
            return currentBribeValue;
        }

        public float PredictGauge()
        {
            IntGauge tmpGauge = new IntGauge(data.GetGauge());

            tmpGauge.Decrement(Mathf.RoundToInt(totalRatio * data.max));

            return tmpGauge.ratio;
        }

        public bool IncreaseBribe()
        {
            if(totalRatio < data.ratio)
            {
                currentBribeLevel++;
                return true;
            }

            return false;
        }

        public bool DecreaseBribe()
        {
            if (totalRatio > 0)
            {
                currentBribeLevel--;
                return true;
            }

            return false;
        }

        public bool TryBuy()
        {
            if(moneyManager.TryBuy(currentBribeValue, 0))
            {
                data.Decrease(totalRatio);
                currentBribeLevel = 0;
                data.SetBribeDate();

                uiManager?.UpdateDisplayer();

                return true;
            }

            return false;
        }

        public void RegisterInvestigator(Cultist _cultist, InvestigatorBehaviour _behaviour)
        {
            if (investigators == null) investigators = new Dictionary<Cultist, InvestigatorBehaviour>();
            if (investigatorsList == null) investigatorsList = new List<Cultist>();

            if(!investigators.ContainsKey(_cultist)) investigators.Add(_cultist, _behaviour);
            if (!investigatorsList.Contains(_cultist)) investigatorsList.Add(_cultist);
        }

        public void Unregister(Cultist _cultist)
        {
            if (investigators.ContainsKey(_cultist)) investigators.Remove(_cultist);
            if (investigatorsList.Contains(_cultist)) investigatorsList.Remove(_cultist);
        }

        public void ResetData()
        {
            data.Reset(100);
        }

        public void InitAysnchValues()
        {

        }

        #region Debug
        [ContextMenu("fill police")]
        public void SetPoliceMax()
        {
            data.Increment(data.max);
            uiManager?.UpdateDisplayer();
        }

        [ContextMenu("set 4th quarter police")]
        public void SetPolice4q()
        {
            data.Set(Mathf.RoundToInt(data.max * 0.75f) + 1);
            uiManager?.UpdateDisplayer();
        }

        [ContextMenu("Set 3rd Quarter police")]
        public void SetPolice3q()
        {
            data.Set(Mathf.RoundToInt(data.max * 0.5f) + 1);
            uiManager?.UpdateDisplayer();
        }

        [ContextMenu("Set 2d Quarter")]
        public void SetPolice2q()
        {
            data.Set(Mathf.RoundToInt(data.max*0.25f)+1);
            uiManager?.UpdateDisplayer();
        }

        [ContextMenu("Set 1st Quarter")]
        public void SetPolice1q()
        {
            data.Set(Mathf.RoundToInt(data.max * 0.25f) - 1);
            uiManager?.UpdateDisplayer();
        }

        [ContextMenu("Empty police")]
        public void SetPoliceMin()
        {
            data.Set(0);
            uiManager?.UpdateDisplayer();
        }

        [ContextMenu("DisplayInvestigators")]
        public void DisplayInvestigators()
        {
            foreach (Cultist cultist in investigatorsList)
            {
                investigators[cultist].SuspiciousBehaviour();
            }
        }
        #endregion
    }
}