using UnityEngine;

namespace CultManager
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private DebugInstance debug = default;
        [SerializeField] private Transform camTransform = default;
        [SerializeField] private Transform positionController = default;
        [SerializeField] private Transform positionTarget = default;
        [SerializeField] private Transform focusPoint = default;
        [SerializeField] private Transform focusController = default;
        [SerializeField] private Camera cam = default;
        [SerializeField, Range(0.0f, 1.0f)] private float verticalLerpValue;
        [SerializeField, Range(0.0f, 1.0f)] private float azimuthalLerpValue;
        [SerializeField, Range(0.0f, 1.0f)] private float zoomLerpValue;
        [SerializeField, DrawScriptable] private CameraControllerSettings settings;


        [SerializeField, HideInInspector] private Vector2 startDirection;
        private float startZoomValue;

        private float maxAngle;
        private float minAngle;
        [SerializeField, HideInInspector] private float radius;
        private float authorizedRadius;

        private bool disable = false;
        

        void Start()
        {
            SetStartDirection();
            zoomLerpValue = 1.0f;
            azimuthalLerpValue = 0.5f;
            verticalLerpValue = Mathf.InverseLerp(settings.minPositionControlYvalue, settings.maxPositionControlYValue, camTransform.position.y);
            startZoomValue = cam.fieldOfView;
        }

        void Update()
        {
            if(!disable) LerpsHandler();
            VerticalMovement();
            AzimuthalMovement();
            if(!disable) Zoom();
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
            zoomLerpValue = Mathf.Clamp(zoomLerpValue, settings.maxZoomDistanceFraction, 1.0f);
            positionTarget.position = Vector3.Lerp(focusPoint.position, positionController.position, zoomLerpValue);
            authorizedRadius = Vector2.Distance(new Vector2(focusPoint.position.x, focusPoint.position.z), new Vector2(positionTarget.position.x, positionTarget.position.z));
            //cam.fieldOfView = Mathf.Lerp(settings.minFOV, settings.maxFOV, zoomLerpValue);

            //if (!Gesture.Pinching)
            //{
            //    if (Mathf.Abs(zoomLerpValue - 0.5f) < 0.025f) zoomLerpValue = 0.5f;
            //    else if (zoomLerpValue > 0.5f) zoomLerpValue -= 0.5f / settings.unzoomDuration * Time.deltaTime;
            //    else if (zoomLerpValue < 0.5f) zoomLerpValue += 0.5f / settings.unzoomDuration * Time.deltaTime;
            //}
        }

        private void MoveCamera()
        {
            camTransform.position = Vector3.Lerp(camTransform.position, positionTarget.position, settings.cameraLerpForce);
            focusPoint.position = Vector3.Lerp(focusPoint.position, focusController.position, settings.cameraLerpForce);
            //KeepDistance();
            camTransform.LookAt(focusPoint);
        }

        private void KeepDistance()
        {
            Vector2 direction = (new Vector2(camTransform.position.x, camTransform.position.z).normalized - new Vector2(focusPoint.position.x, focusPoint.position.z).normalized).normalized * authorizedRadius;
            camTransform.position = new Vector3(direction.x, camTransform.position.y, direction.y);
        }

        public Transform GetCamTransform()
        {
            return camTransform;
        }

        public void Disable()
        {
            disable = true;
        }

        public void Enable()
        {
            disable = false;
        }
    }
}