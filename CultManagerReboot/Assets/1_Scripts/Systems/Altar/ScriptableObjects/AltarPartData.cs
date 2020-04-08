using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Systems/AltarPartData")]
    public class AltarPartData : ScriptableObject
    {
        public string altarPartName;
        public int maxCultists;
        public int maxBuildPoints;
        [TextArea(2, 6)]
        public string description;

        AltarPart currentAltarPart;

        void Init(AltarPart _altarPart)
        {
            currentAltarPart = _altarPart;
        }
    }
}

