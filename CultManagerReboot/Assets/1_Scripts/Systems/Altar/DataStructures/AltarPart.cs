using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    [System.Serializable]
    public class AltarPart
    {
        public string altarPartName;
        public bool isBought;
        public IntGauge assignedCultists;
        public IntGauge currentBuildPoints;

        public AltarPart(string _altarPartName)
        {
            altarPartName = _altarPartName;
            isBought = false;
        }

        public void Buy()
        {
            isBought = true;
        }

        public void InitAssignedCultists(int _min, int _max, bool _startFull = true)
        {
            assignedCultists = new IntGauge(_min, _max, _startFull);
        }

        public void IncrementAssignedCultists(int _value)
        {
            assignedCultists.Increment(_value);
        }

        public void DecrementAssignedCultists(int _value)
        {
            assignedCultists.Decrement(_value);
        }

        public void SetAssignedCultists()
        {
            assignedCultists.SetValue();
        }

        public void SetAssignedCultists(int _value)
        {
            assignedCultists.SetValue(_value);
        }

        public void SetMaxAssignedCultists(int _value)
        {
            assignedCultists.SetMax(_value);
        }

        public void InitBuildPoints(int _min, int _max, bool _startFull = true)
        {
            currentBuildPoints = new IntGauge(_min, _max, _startFull);
        }

        public void SetBuildPoints()
        {
            currentBuildPoints.SetValue();
        }

        public void SetBuildPoints(int _value)
        {
            currentBuildPoints.SetValue(_value);
        }

        public void SetMaxBuildPoints(int _max)
        {
            currentBuildPoints.SetMax(_max);
        }

        public void IncrementBuildPoints(int _increment)
        {
            currentBuildPoints.Increment(_increment);
        }

        public void DecrementBuildPoints(int _decrement)
        {
            currentBuildPoints.Decrement(_decrement);
        }

        
    }
}
