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

        #endregion
    }
}