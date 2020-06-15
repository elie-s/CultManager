using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [CreateAssetMenu(menuName ="CultManager/Animations/Cultist Animation Settings")]
    public class CultistAnimationSettings : ScriptableObject
    {

        [Header("Movement")]
        public float movementSpeed = 1.0f;
        public float minimumDistance = 0.1f;
        [Header("Idle")]
        public float idleBaseDuration = 2.0f;
        public float idleDurationRandomModifier = 0.5f;
        public float minIdleDuration => idleBaseDuration - idleDurationRandomModifier;
        public float maxIdleDuration => idleBaseDuration + idleDurationRandomModifier;
        [Header("Random")]
        [DrawScriptable] public ScriptableRandom idleWanderRandom = default;
    }
}