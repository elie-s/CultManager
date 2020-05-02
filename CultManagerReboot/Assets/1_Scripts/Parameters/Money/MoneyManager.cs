using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class MoneyManager : MonoBehaviour
    {
        [SerializeField] private MoneyData data = default;

        public void Increase(int _value)
        {
            data.Increase((uint)_value);
        }

        public void Decrease(int _value)
        {
            data.Decrease((uint)_value);
        }

        public void InitializeData()
        {
            if (!SaveManager.saveLoaded)
            {
                data.Reset();
            } 
        }

        public void ResetCult(int level)
        {
            data.ResetMoneyData(level);
        }
    }
}
