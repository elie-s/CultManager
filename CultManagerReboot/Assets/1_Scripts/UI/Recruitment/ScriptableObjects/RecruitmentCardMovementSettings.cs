using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Recruitment/CardMovementSettings")]
    public class RecruitmentCardMovementSettings : ScriptableObject
    {
        public Vector2 endPosition;
        public Vector3 endRotation;
        public AnimationCurve movementCurve;
        public AnimationCurve rotationCurve;
        public AnimationCurve fadeCurve;
        public float slideThreshold;
        public float slideMultiplier;
        public float exitingSpeed;
        public float backForce;
    }
}