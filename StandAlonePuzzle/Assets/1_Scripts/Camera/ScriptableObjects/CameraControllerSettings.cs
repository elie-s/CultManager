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
        public float maxZoomValue = 1.0f;
        public float zoomForce = 1.0f;
        public float panningSpeed = 1.2f;
        public float minWidth = 0.15f;
        public float allowNavigationSwipeThreshold = 0.1f;
    }
}