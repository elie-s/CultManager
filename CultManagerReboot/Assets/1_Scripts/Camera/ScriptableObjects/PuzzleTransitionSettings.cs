using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Camera/Puzzle Transition")]
    public class PuzzleTransitionSettings : ScriptableObject
    {
        public float transitionDuration = 0.5f;
        public AnimationCurve movementCurve = default;
        public AnimationCurve fadeCurve = default;
        public Gradient fade = default;
    }
}