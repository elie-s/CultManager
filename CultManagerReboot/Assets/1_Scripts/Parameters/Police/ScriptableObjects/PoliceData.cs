using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Parameters/PoliceData")]
    public class PoliceData : ScriptableObject
    {
        private IntGauge gauge;
        public int value => gauge.value;
        public int max => gauge.max;

        public void Increment(int _value)
        {
            gauge.Increment(_value);
        }

        public void Decrement(int _value)
        {
            gauge.Decrement(_value);
        }

        public void Set(int _value)
        {
            gauge.SetValue(_value);
        }

        public void Reset(int _max)
        {
            gauge = new IntGauge(0, _max, false);
        }

        public void LoadSave(Save _save)
        {
            gauge = new IntGauge(_save.policeCurrentValue, _save.policeMaxValue, false);
        }
    }
}

