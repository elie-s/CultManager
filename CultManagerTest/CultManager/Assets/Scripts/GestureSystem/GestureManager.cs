using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class GestureManager : MonoBehaviour
    {
        [SerializeField] private Camera playCam = default;
        [SerializeField] private DebugInstance debug = default;
        [SerializeField] private GesturesSettings settings = default;
        public static GestureState simpleTouch = default;
        public static GestureState pinch = default;

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
            Vector3 oldPos = Vector3.zero;
            Vector2 oldScreenPos = Vector2.zero;

            while (Input.touchCount > 0)
            {
                if (Input.touchCount > 1)
                {
                    cancelled = true;
                    StartCoroutine(MultipleTouchesRoutine());
                    break;
                }
                else if (Input.touches[0].phase == TouchPhase.Moved)
                {
                    if(Input.touches[0].deltaPosition.magnitude > 5.0f)
                        debug.Log("Touch moved: "+ Input.GetTouch(0).deltaPosition.magnitude + " - "+Vector3.Distance(oldPos, Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position)), DebugInstance.Importance.Lesser);
                    debug.Log("Pos: (" + oldScreenPos + " - " + oldPos + ") / (" + Input.GetTouch(0).position + " - " + Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position) + ")", DebugInstance.Importance.Lesser);
                }

                oldPos = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position);
                oldScreenPos = Input.GetTouch(0).position;
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

                        if (localTimer < settings.quickTouchDelay) debug.Log("Double touch.", DebugInstance.Importance.Average);
                    }
                    else
                    {
                        debug.Log("Quick touch.", DebugInstance.Importance.Average);
                    }
                }
                else
                {
                    debug.Log("Long touch.", DebugInstance.Importance.Average);
                }

                EndGesture();
            }
        }

        private IEnumerator MultipleTouchesRoutine()
        {
            debug.Log("Multiple touches detected. Starting MultipleTouchesRoutine.", DebugInstance.Importance.Lesser);

            yield return null;

            EndGesture();
        }

        private void EndGesture()
        {
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
    }
}