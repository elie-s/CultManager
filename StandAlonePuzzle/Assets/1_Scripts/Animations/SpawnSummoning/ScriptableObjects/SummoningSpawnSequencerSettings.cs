using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Animations/Summon/Spawn Settings")]
    public class SummoningSpawnSequencerSettings : ScriptableObject
    {
        [Header("TransitionToSummon")]
        public float fadeShapeDuration = 1.0f;
        public float fadeShapeAlphaLimit = 0.1f;
        public AnimationCurve fadeCurve = default;
        public float delayBeforeCamTransition = 1.0f;
        [Header("Summoning")]
        public float spawnAppearingDuration = 1.0f;
        public Gradient spawnAppearingGradient = default;
        public AnimationCurve spawnAppearingCurve = default;

    }
}