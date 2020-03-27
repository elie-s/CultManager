using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Altar/PillarData")]
    public class AltarPartData : ScriptableObject
    {
        public string pillarName;
        [TextArea(6,10)]
        public string pillarTrivia;
        [TextArea(6, 10)]
        public string pillarRequirement;
        public float maxBuildPoints;
        public float rateOfBuildingPerUnit;
        public int requiredResource;
        public int maxUnitsForBuilding;
    }
}

