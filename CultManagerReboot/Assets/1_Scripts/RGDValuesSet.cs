using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Balancing/RGD Set")]
    public class RGDValuesSet : ScriptableObject
    {
        public DemonName currentDemon;
        public long[] influenceEarnedOnSummon;
        public int[] moneyMax;
        public int[] policeGaugeMax;
        public int[] bribeLevelCost;

    }
}