using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private DebugInstance debug = default;
        [SerializeField] private Transform camTransform = default;
        [SerializeField] private Transform positionController = default;
        [SerializeField] private Transform focusPoint = default;
        [SerializeField] private Transform focusController = default;
        [SerializeField] private Camera cam = default;
        [SerializeField, DrawScriptable] private CameraControllerSettings settings;
        

        private Vector2 startDirection;
        private float startZoomValue;

        private float maxAngle;
        private float minAngle;
        private float radius;
        private float verticalLerpValue;
        private float azimuthalLerpValue;
        private float zoomLerpValue;

        private bool unzooming;

        void Start()
        {
            SetStartDirection();
            zoomLerpValue = Mathf.InverseLerp(settings.minFOV, settings.maxFOV, cam.fieldOfView);
            azimuthalLerpValue = 0.5f;
            verticalLerpValue = Mathf.InverseLerp(settings.minPositionControlYvalue, settings.maxPositionControlYValue, camTransform.position.y);
            startZoomValue = cam.fieldOfView;
        }

        void Update()
        {
            LerpsHandler();
            VerticalMovement();
            AzimuthalMovement();
            Zoom();
            MoveCamera();
        }

        private void SetStartDirection()
        {
            Vector2 maxAnglePos;
            Vector2 minAnglePos;
            Vector3 direction = (focusPoint.position - camTransform.position).normalized;
            startDirection = new Vector2(direction.x, direction.z) * -1;
            debug.Log("Start direction: " + startDirection, DebugInstance.Importance.Lesser);

            radius = Vector2.Distance(new Vector2(camTransform.position.x, camTransform.position.z), new Vector2(focusPoint.position.x, focusPoint.position.z));

            float angle = settings.spanAngle * Mathf.Deg2Rad;
            float angleCorrection = Mathf.Atan2(startDirection.y, startDirection.x);

            maxAnglePos = (new Vector2(Mathf.Cos(angleCorrection + angle), Mathf.Sin(angleCorrection + angle))).normalized;
            minAnglePos = (new Vector2(Mathf.Cos(angleCorrection - angle), Mathf.Sin(angleCorrection - angle))).normalized;

            maxAngle = Mathf.Atan2(maxAnglePos.y, maxAnglePos.x);
            minAngle = Mathf.Atan2(minAnglePos.y, minAnglePos.x);

            debug.Log(maxAnglePos, DebugInstance.Importance.Lesser);
            debug.Log(minAnglePos, DebugInstance.Importance.Lesser);
        }


        private void LerpsHandler()
        {
            if (Mathf.Abs(Gesture.DeltaMovement.x) > Mathf.Abs(Gesture.DeltaMovement.y)) azimuthalLerpValue -= Gesture.DeltaMovement.x * settings.rotationSpeed;
            else verticalLerpValue -= Gesture.DeltaMovement.y * settings.verticalSpeed;

            azimuthalLerpValue = Mathf.Clamp01(azimuthalLerpValue);
            verticalLerpValue = Mathf.Clamp01(verticalLerpValue);
        }

        private void VerticalMovement()
        {
            float verticalControllerValue = Mathf.Lerp(settings.minPositionControlYvalue, settings.maxPositionControlYValue, verticalLerpValue);
            float verticalFocusValue = Mathf.Lerp(settings.minFocusPointYValue, settings.maxFocusPointYValue, verticalLerpValue);

            positionController.position = new Vector3(positionController.position.x, verticalControllerValue, positionController.position.z);
            focusController.position = new Vector3(focusController.position.x, verticalFocusValue, focusController.position.z);
        }

        private void AzimuthalMovement()
        {
            float angle = Mathf.Lerp(minAngle, maxAngle, settings.orbitCurve.Evaluate(azimuthalLerpValue));
            Vector2 horizontalProjection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            positionController.position = new Vector3(horizontalProjection.x, positionController.position.y, horizontalProjection.y);
        }

        private void Zoom()
        {
            zoomLerpValue += Gesture.PinchDeltaValue * settings.zoomSpeed * -1;
            zoomLerpValue = Mathf.Clamp01(zoomLerpValue);
            cam.fieldOfView = Mathf.Lerp(settings.minFOV, settings.maxFOV, settings.zoomLerpCurve.Evaluate(zoomLerpValue));
            if (!Gesture.Pinching)
            {
                if (Mathf.Abs(zoomLerpValue - 0.5f) < 0.025f) zoomLerpValue = 0.5f;
                else if (zoomLerpValue > 0.5f) zoomLerpValue -= 0.5f / settings.unzoomDuration * Time.deltaTime;
                else if (zoomLerpValue < 0.5f) zoomLerpValue += 0.5f / settings.unzoomDuration * Time.deltaTime;
            }
        }

        private void MoveCamera()
        {
            camTransform.position = Vector3.Lerp(camTransform.position, positionController.position, settings.cameraLerpForce);
            focusPoint.position = Vector3.Lerp(focusPoint.position, focusController.position, settings.cameraLerpForce);
            camTransform.LookAt(focusPoint);
        }

        public Transform GetCamTransform()
        {
            return camTransform;
        }
    }
}