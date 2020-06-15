using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Parameters/MoneyData")]
    public class MoneyData : ScriptableObject, ILoadable
    {
        public uint money { get; private set; }
        public uint relics { get; private set; }

        public void Increase(int _money, int _relic)
        {
            money += (uint)_money;
            relics += (uint)_relic;
        }

        public void Decrease(int _money, int _relic)
        {
            money -= (uint)_money;
            relics -= (uint)_relic;
        }

        public void Reset(int _money, int _relic)
        {
            money = (uint)_money;
            relics = (uint)_relic;
        }

        public void Reset(uint _money, uint _relic)
        {
            money = _money;
            relics = _relic;
        }

        public void Reset()
        {
            money = 0;
            relics = 0;
        }

        public void LoadSave(Save _save)
        {
            Reset(_save.moneyValue, _save.relicValue);
        }

        public void ResetMoneyData(int level)
        {
            Reset();
        }
    }
}

