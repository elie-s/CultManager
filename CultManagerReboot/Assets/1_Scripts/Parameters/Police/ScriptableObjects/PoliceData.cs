using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Parameters/PoliceData")]
    public class PoliceData : ScriptableObject, ILoadable
    {
        private IntGauge gauge;
        //public int PoliceRecoveryModifier;
        public DateTime lastHourReference;
        public int value => gauge.value;
        public int max => gauge.max;
        public float ratio => gauge.ratio;

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
            gauge = new IntGauge(0, _save.policeMaxValue, _save.policeCurrentValue);
            SetHourReference(_save.lastHourReference);
        }

        public void ResetPoliceData(int level)
        {
            switch (level)
            {
                case 1:
                    {
                        gauge = new IntGauge(0, 1000, false);
                    }
                    break;
                case 2:
                    {
                        gauge = new IntGauge(0, 1200, false);
                    }
                    break;
                case 3:
                    {
                        gauge = new IntGauge(0, 1400, false);
                    }
                    break;
                case 4:
                    {
                        gauge = new IntGauge(0, 1600, false);
                    }
                    break;
                case 5:
                    {
                        gauge = new IntGauge(0, 2000, false);
                    }
                    break;
            }
        }

        public void SetHourReference()
        {
            lastHourReference = DateTime.Now;
        }

        public void SetHourReference(DateTime _lastHourReference)
        {
            lastHourReference = _lastHourReference;
        }
    }
}

