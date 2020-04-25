using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    [System.Serializable]
    public class BloodBank
    {
        public BloodGroup bloodGroup;
        public IntGauge gauge;

        public BloodBank(BloodGroup _bloodGroup, IntGauge _gauge)
        {
            bloodGroup = _bloodGroup;
            gauge = _gauge;
        }

        public void SetBloodGroup(BloodGroup _bloodGroup)
        {
            bloodGroup = _bloodGroup;
        }

        public void IncrementGauge(int val)
        {
            gauge.Increment(val);
        }

        public void DecrementGauge(int val)
        {
            gauge.Decrement(val);
        }

        public void SetGaugeValue(int val)
        {
            gauge.SetValue(val);
        }

        public void SetGaugeValue()
        {
            gauge.SetValue();
        }

        public void SetGaugeMax(int max)
        {
            gauge.SetMax(max);
        }

        public void Init(int min, int max, bool startFull = true)
        {
            gauge = new IntGauge(min, max, startFull);
        }

    }
}

