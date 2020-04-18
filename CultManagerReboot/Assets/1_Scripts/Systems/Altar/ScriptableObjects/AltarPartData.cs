using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Systems/AltarPartData")]
    public class AltarPartData : ScriptableObject
    {
        public string altarPartName;
        public int altarPartIndex;
        public int maxCultists;
        public int maxBuildPoints;
        public int cost;
        [TextArea(2, 6)]
        public string description;
        public Sprite altarSprite;

        AltarPart currentAltarPart;

        public AltarPart Init(AltarData altarData)
        {
            currentAltarPart = altarData.altarParts[altarPartIndex];
            return currentAltarPart;
        }
    }
}

