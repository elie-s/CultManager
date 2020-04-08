using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Parameters/MoneyData")]
    public class MoneyData : ScriptableObject
    {
        public uint value { get; private set; }

        public void Increase(uint _value)
        {
            value += _value;
        }

        public void Decrease(uint _value)
        {
            value -= _value;
        }

        public void Reset(uint _value)
        {
            value = _value;
        }

        public void Reset()
        {
            value = 0;
        }

        public void LoadSave(Save _save)
        {
            Reset(_save.moneyValue);
        }
    }
}

