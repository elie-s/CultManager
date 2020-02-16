using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CultManager
{
    public class GestureManager : MonoBehaviour
    {
        [SerializeField] private float debugStartAngle = default;
        [SerializeField] private float debugGestureValue = default;
        [SerializeField] private Camera playCam = default;
        [SerializeField] private DebugInstance debug = default;
        [SerializeField] private UnityEvent OnQuickTouch = default;
        [SerializeField] private UnityEvent OnDoubleTap = default;
        [SerializeField] private UnityEvent OnLongTapEnd = default;
        [SerializeField] private GesturesSettings settings = default;

        private bool isGettingGesture = false;

        void Start()
        {

        }

        void Update()
        {
            if (!isGettingGesture && Input.touchCount != 0) StartCoroutine(TouchRoutine());
            debugGestureValue = Gesture.RotationValue;
        }

        private IEnumerator TouchRoutine()
        {
            debug.Log("Starting TouchRoutine.", DebugInstance.Importance.Lesser);
            isGettingGesture = true;
            float localTimer = 0.0f;
            bool cancelled = false;
            bool moved = false;
            Vector2 oldPos = AdjustedViewportRatioPosition(Input.GetTouch(0).position);
            Vector2 startPos = AdjustedViewportRatioPosition(Input.GetTouch(0).position);

            while (Input.touchCount > 0)
            {
                if (Input.touchCount > 1)
                {
                    cancelled = true;
                    StartCoroutine(MultipleTouchesRoutine());
                    break;
                }
                else if (Vector2.Distance(oldPos, AdjustedViewportRatioPosition(Input.GetTouch(0).position)) > settings.movingDetectionThreshold)
                {
                    debug.Log("Moved: " + Vector2.Distance(oldPos, AdjustedViewportRatioPosition(playCam.ScreenToViewportPoint(Input.GetTouch(0).position))), DebugInstance.Importance.Lesser);
                    moved = true;
                    Gesture.Movement = AdjustedViewportRatioPosition(Input.GetTouch(0).position) - startPos;
                }

                oldPos = AdjustedViewportRatioPosition(Input.GetTouch(0).position);
                yield return null;
                localTimer += Time.deltaTime;
            }


            if (!cancelled)
            {
                if (localTimer < settings.quickTouchDelay)
                {
                    localTimer = 0.0f;

                    while (localTimer < settings.doubleTapDelay && Input.touchCount == 0)
                    {
                        yield return null;
                        localTimer += Time.deltaTime;
                    }

                    if (localTimer < settings.doubleTapDelay && Input.touchCount == 1)
                    {
                        localTimer = 0.0f;

                        while (localTimer < settings.quickTouchDelay && Input.touchCount == 1)
                        {
                            yield return null;
                            localTimer += Time.deltaTime;
                        }

                        if (localTimer < settings.quickTouchDelay)
                        {
                            debug.Log("Double touch.", DebugInstance.Importance.Average);
                            Gesture.DoubleTouch = true;
                            OnDoubleTap.Invoke();
                        }
                    }
                    else
                    {
                        debug.Log("Quick touch.", DebugInstance.Importance.Average);
                        Gesture.QuickTouch = true;
                        OnQuickTouch.Invoke();
                    }
                }
                else if (!moved)
                {
                    debug.Log("Long touch.", DebugInstance.Importance.Average);
                    Gesture.LongTouch = true;
                    OnLongTapEnd.Invoke();
                }

                yield return null;


                EndGesture();
            }
        }

        private IEnumerator MultipleTouchesRoutine()
        {
            debug.Log("Multiple touches detected. Starting MultipleTouchesRoutine.", DebugInstance.Importance.Lesser);

            Vector2 touch0startPos = AdjustedViewportRatioPosition(Input.GetTouch(0).position);
            Vector2 touch1startPos = AdjustedViewportRatioPosition(Input.GetTouch(1).position);
            Vector2 touch0pos = touch0startPos;
            Vector2 touch1pos = touch1startPos;
            Vector2 centerPos = Vector2.Lerp(touch0pos, touch1pos, 0.5f);

            int topFingerIndex = touch0startPos.y > touch1startPos.y ? 0 : 1;
            Vector2 refAngleVector = AdjustedViewportRatioPosition(Input.GetTouch(topFingerIndex).position) - centerPos;

            float startDistance = Vector2.Distance(touch0pos, touch1pos);
            float startAngle = Vector2.SignedAngle(refAngleVector - centerPos, Vector2.down);
            float distance = startDistance;
            float angle = 0.0f;

            MultipleTouchMode mode = default;

            debugStartAngle = startAngle;

            // Rotation or Pinch detection loop.
            while (Input.touchCount > 1)
            {
                touch0pos = AdjustedViewportRatioPosition(Input.GetTouch(0).position);
                touch1pos = AdjustedViewportRatioPosition(Input.GetTouch(1).position);

                distance = Vector2.Distance(touch0pos, touch1pos);
                angle = Vector2.SignedAngle(AdjustedViewportRatioPosition(Input.GetTouch(topFingerIndex).position) - centerPos, refAngleVector);

                if (Mathf.Abs(startDistance - distance) > settings.pinchDistanceDetectionThreshold)
                {
                    Gesture.Pinching = true;
                    mode = startDistance > (settings.pinchMaxDistance - settings.pinchMinDistance) / 2 ? MultipleTouchMode.Pinch : MultipleTouchMode.Stretch;
                    debug.Log("Pinch detected: "+ Mathf.Abs(startDistance - distance), DebugInstance.Importance.Lesser);

                    break;
                }
                else if (angle > settings.RotationAngleDetectionThreshold)
                {
                    Gesture.Rotating = true;
                    mode = MultipleTouchMode.Rotation;
                    debug.Log("Rotation detected: " + angle, DebugInstance.Importance.Lesser);

                    break;
                }

                yield return null;
            }

            while (Input.touchCount > 1)
            {
                touch0pos = AdjustedViewportRatioPosition(Input.GetTouch(0).position);
                touch1pos = AdjustedViewportRatioPosition(Input.GetTouch(1).position);
                centerPos = Vector2.Lerp(touch0pos, touch1pos, 0.5f);

                distance =  Vector2.Distance(touch0pos, touch1pos);
                angle = Vector2.SignedAngle(AdjustedViewportRatioPosition(Input.GetTouch(topFingerIndex).position) - centerPos, refAngleVector);

                switch (mode)
                {
                    case MultipleTouchMode.Pinch:
                        Gesture.PinchValue = Mathf.InverseLerp(startDistance, settings.pinchMinDistance, distance) * -1;
                        break;
                    case MultipleTouchMode.Stretch:
                        Gesture.PinchValue = Mathf.InverseLerp(startDistance, settings.pinchMaxDistance, distance);
                        break;
                    case MultipleTouchMode.Rotation:
                        Gesture.RotationValue = LerpAngle(angle, Gesture.RotationValue);
                        break;
                }

                yield return null;
            }

            yield return null;

            EndGesture();
        }

        private void EndGesture()
        {
            ResetGestures();

            StartCoroutine(EndGestureRoutine());
        }

        private IEnumerator EndGestureRoutine()
        {
            debug.Log("Starting EndGestureRoutine.", DebugInstance.Importance.Lesser);

            while (Input.touchCount > 0)
            {
                yield return null;
            }
            
            isGettingGesture = false;
            debug.Log("Stopped getting gesture.", DebugInstance.Importance.Average);
        }

        private Vector2 AdjustedViewportRatioPosition(Vector2 _OnScreenPosition)
        {
            Vector2 result = playCam.ScreenToViewportPoint(_OnScreenPosition);
            float screenWidthHeightRatio = (float)Screen.height / (float)Screen.width;

            return new Vector2(result.x, result.y * screenWidthHeightRatio);
        }

        private float LerpAngle(float _angle, float _oldValue)
        {
            if(_angle < 0.0f && _oldValue < 0.15f)
            {
                return Mathf.InverseLerp(0.0f, -180.0f, _angle)*-1;
            }
            else if(_angle > 0.0f && _oldValue > -0.15f)
            {
                return Mathf.InverseLerp(0.0f, 180.0f, _angle);
            }

            return _oldValue;
        }

        private void ResetGestures()
        {
            Gesture.Movement = Vector2.zero;
            Gesture.LongTouch = false;
            Gesture.DoubleTouch = false;
            Gesture.QuickTouch = false;
            Gesture.PinchValue = 0.0f;
            Gesture.Pinching = false;
            Gesture.RotationValue = 0.0f;
            Gesture.Rotating = false;
        }

        private enum MultipleTouchMode { Pinch, Stretch, Rotation}
    }
}