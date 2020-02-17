using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Recruitment/Card Behaviour Settings")]
    public class RecruitmentCardBehaviourSettings : ScriptableObject
    {
        public float swipeMovementThreshold = 0.25f;
        public float lateralSpeed = 5.0f;
        public float outDuration = 0.5f;
        public float centerDUration = 0.25f;
    }
}