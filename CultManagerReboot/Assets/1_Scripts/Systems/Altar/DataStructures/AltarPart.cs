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
        public Gauge currentBuildPoints;

        public AltarPart(string _altarPartName,int _assignedCultists, Gauge _currentBuildPoints)
        {
            altarPartName = _altarPartName;
            assignedCultists = _assignedCultists;
            currentBuildPoints = _currentBuildPoints;
        }

    }
}
