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
        [SerializeField] private float bribeRatio = 0.05f;

        private int currentBribeLevel = 0;

        private int currentBribeValue => currentBribeLevel * data.bribeLevelValue;
        public float totalRatio => currentBribeLevel * bribeRatio; 

        [ContextMenu("fill police")]
        public void SetPoliceMax()
        {
            data.Increment(data.max);
            uiManager?.UpdateDisplayer();
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

                uiManager?.UpdateDisplayer();

                return true;
            }

            return false;
        }
    }
}