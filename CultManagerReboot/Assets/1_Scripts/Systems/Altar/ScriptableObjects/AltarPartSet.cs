using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Altar/AltarSet")]
    public class AltarPartSet : ScriptableObject
    {
        public AltarPartData[] altarPartDatas;
    }
}

