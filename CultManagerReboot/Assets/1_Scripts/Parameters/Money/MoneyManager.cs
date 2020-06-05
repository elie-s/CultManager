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
        public int value => (int)data.money;

        public void Increase(int _money, int _relic)
        {
            //float temp = _value /** (1 + reference.storage.MoneyIncrementModifier)*/;
            data.Increase(_money, _relic);
        }

        public void Decrease(int _money, int _relic)
        {
            //float temp = _value /** (1 + reference.storage.MoneyDecrementModifier)*/;
            data.Decrease(_money, _relic);
        }

        public void ResetValue(int _money, int _relic)
        {
            data.Reset(_money, _relic);
        }

        public void ResetData()
        {
            data.Reset();
        }

        public void ResetCult(int level)
        {
            data.ResetMoneyData(level);
        }

        public bool TryBuy(int _money, int _relic)
        {
            if (_money > data.money || _relic > data.relics) return false;

            data.Decrease(_money, _relic);

            return true;
        }
    }
}
