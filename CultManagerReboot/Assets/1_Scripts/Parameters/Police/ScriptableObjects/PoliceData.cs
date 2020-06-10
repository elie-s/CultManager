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
        public DateTime lastBribe { get; private set; }
        public int value => gauge.value;
        public int max => gauge.max;
        public float ratio => gauge.ratio;

        public int bribeLevelValue { get; private set; }

        public void Increment(int _value)
        {
            gauge.Increment(_value);
        }

        public void Decrement(int _value)
        {
            gauge.Decrement(_value);
        }

        public void Decrease(float _value)
        {
            gauge.Decrease(_value);
        }

        public void Set(int _value)
        {
            gauge.SetValue(_value);
        }

        public void SetBribeValue(int _value)
        {
            bribeLevelValue = _value;
        }

        public IntGauge GetGauge()
        {
            return gauge;
        }

        public void Reset(int _max)
        {
            gauge = new IntGauge(0, _max, false);
            SetBribeValue(10);
        }

        public void LoadSave(Save _save)
        {
            Debug.Log("_save.policeCurrentValue");
            gauge = new IntGauge(0, _save.policeMaxValue, _save.policeCurrentValue);
            SetHourReference(_save.lastHourReference);
            SetBribeValue(_save.bribeValue);
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

        public void SetBribeDate()
        {
            lastBribe = DateTime.Now;
        }

        public bool CanBribe()
        {
            return DateTime.Now.DayOfWeek != lastBribe.DayOfWeek && DateTime.Now.DayOfYear != lastBribe.DayOfYear;
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

