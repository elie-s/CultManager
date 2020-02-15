using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CultManager
{
    public class GestureManager : MonoBehaviour
    {
        [SerializeField] private float distance = default;
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
            Vector2 touch0oldPos = touch0startPos;
            Vector2 touch1oldPos = touch1startPos;
            Vector2 touch0pos = touch0startPos;
            Vector2 touch1pos = touch1startPos;

            while (Input.touchCount > 1)
            {
                touch0pos = AdjustedViewportRatioPosition(Input.GetTouch(0).position);
                touch1pos = AdjustedViewportRatioPosition(Input.GetTouch(1).position);

                distance =  Vector2.Distance(touch0pos, touch1pos);

                touch0oldPos = touch0pos;
                touch1oldPos = touch1pos;
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

        private void ResetGestures()
        {
            Gesture.Movement = Vector2.zero;
            Gesture.LongTouch = false;
            Gesture.DoubleTouch = false;
            Gesture.QuickTouch = false;
        }
    }
}