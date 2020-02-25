using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [CreateAssetMenu(menuName ="CultManager/Camera/Settings")]
    public class CameraControllerSettings : ScriptableObject
    {
        [Header("General")]
        public float cameraLerpForce = 0.2f;

        [Header("Up & Down ontrol")]
        public float verticalSpeed = 2.0f;
        public float minPositionControlYvalue = 1.5f;
        public float maxPositionControlYValue = 10.5f;
        public float minFocusPointYValue = 1.5f;
        public float maxFocusPointYValue = 3.75f;

        [Header("Orbit Control")]
        public float rotationSpeed = 35.0f;
        public float spanAngle = 30.0f;
        public AnimationCurve orbitCurve = default;


        [Header("Zoom Control")]
        public float maxFOV = 75.0f;
        public float minFOV = 20.0f;
        public AnimationCurve zoomLerpCurve = default;
        public float zoomSpeed = 10.0f;
        public float unzoomDuration = 0.25f;
    }
}