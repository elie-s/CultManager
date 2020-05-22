using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Camera/Settings")]
    public class CameraControllerSettings : ScriptableObject
    {
        public AnimationCurve transitionCurve;
        public float transitionDuration;
    }
}