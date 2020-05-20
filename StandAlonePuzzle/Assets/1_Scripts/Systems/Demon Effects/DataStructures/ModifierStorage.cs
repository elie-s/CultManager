using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    [System.Serializable]
    public class ModifierStorage
    {
        [Header("Police")]
        public float PoliceDecrementModifier;
        public float PoliceIncrementModifier;
        public float PoliceBribeModifier;

        [Header("Influence")]
        public float InfluenceDecrementModifier;
        public float InfluenceIncrementModifier;

        [Header("Money")]
        public float MoneyDecrementModifier;
        public float MoneyIncrementModifier;

        [Header("Recruitment")]
        public float RecruitmentQueueModifier;
        public float RecruitmentMoneyModifier;
        public float RecruitmentPoliceModifier;

    }
}

