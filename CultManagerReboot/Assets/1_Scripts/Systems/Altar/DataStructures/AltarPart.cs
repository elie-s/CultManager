using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    [System.Serializable]
    public class AltarPart
    {
        public string altarPartName;
        public int assignedCultists;
        public bool isBought;
        public IntGauge currentBuildPoints;

        public AltarPart(string _altarPartName,int _assignedCultists, IntGauge _currentBuildPoints,bool _isBought)
        {
            altarPartName = _altarPartName;
            assignedCultists = _assignedCultists;
            currentBuildPoints = _currentBuildPoints;
            isBought = _isBought;
        }

        public void Buy()
        {
            isBought = true;
            Debug.Log("Bought !");
        }

        public void IncreaseAssignedCultists(int _value)
        {
            assignedCultists += _value;
        }

        public void DecreaseAssignedCultists(int _value)
        {
            assignedCultists -= _value;
        }

        public void ResetAssignedCultists()
        {
            assignedCultists = 0;
        }

        public void ResetAssignedCultists(int _value)
        {
            assignedCultists = _value;
        }


        public void Init(int _min, int _max, bool _startFull = true)
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
