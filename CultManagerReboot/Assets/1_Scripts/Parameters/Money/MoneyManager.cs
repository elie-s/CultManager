using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0414
namespace CultManager
{
    public class MoneyManager : MonoBehaviour
    {
        [SerializeField] private MoneyData data = default;
        [SerializeField] private ModifierReference reference = default;
        public int value => (int)data.value;

        public void Increase(int _value)
        {
            float temp = _value /** (1 + reference.storage.MoneyIncrementModifier)*/;
            data.Increase((uint)Mathf.RoundToInt(temp));
        }

        public void Decrease(int _value)
        {
            float temp = _value /** (1 + reference.storage.MoneyDecrementModifier)*/;
            data.Decrease((uint)Mathf.RoundToInt(temp));
        }

        public void ResetValue(int _value)
        {
            data.Reset((uint)_value);
        }

        public void ResetData()
        {
            data.Reset();
        }

        public void ResetCult(int level)
        {
            data.ResetMoneyData(level);
        }
    }
}
