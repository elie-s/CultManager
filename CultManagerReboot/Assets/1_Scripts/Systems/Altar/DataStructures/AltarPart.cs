using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    [System.Serializable]
    public struct AltarPart
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
        }

        public void Increase(int _value)
        {
            assignedCultists += _value;
        }

        public void Decrease(int _value)
        {
            assignedCultists -= _value;
        }

        public void Reset()
        {
            assignedCultists = 0;
        }

        public void Reset(int _value)
        {
            assignedCultists = _value;
        }

    }
}
