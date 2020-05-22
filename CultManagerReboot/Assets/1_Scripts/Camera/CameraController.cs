using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CultManager
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera worldCam = default;
        [SerializeField] private Camera puzzleCam = default;
        [SerializeField] private UnityEvent onTransitionStart = default;
        [SerializeField] private UnityEvent onTransitionEnd = default;
        [SerializeField] private CameraTarget origin = default;
        [SerializeField] private CameraTarget[] targets = default;
        [SerializeField] private Transform debugTopRight = default;
        [SerializeField] private Transform debugBottomLeft = default;
        [SerializeField, DrawScriptable] private CameraControllerSettings settings = default;



        public static bool isAtOrigin;
        public static bool allowControl;
        public static Camera CurrentCam { get; private set; }

        private bool locked;
        private PanningArea panningArea;
        private int currentTarget = -1;

        private void OnEnable()
        {
            //InitCam();
            isAtOrigin = true;
            allowControl = true;
            UseWorldCam();
        }

        private void Update()
        {
            if (!isAtOrigin && allowControl && GameManager.currentPanel == CurrentPanel.None)
            {
                Zoom();
                Pan();
            }

            if (panningArea != null)
            {
                debugTopRight.position = panningArea.topRight;
                debugBottomLeft.position = panningArea.bottomLeft;
            }
        }

        public void UseWorldCam() { CurrentCam = worldCam; }
        public void UsePuzzleCam() { CurrentCam = puzzleCam; }
        public void SwitchCam() { CurrentCam = CurrentCam == worldCam ? puzzleCam : worldCam; }

        // Transitions //
        private void InitCam()
        {
            if(CurrentCam) origin = new CameraTarget(CurrentCam);
            else origin = new CameraTarget(Camera.main);
        }

        public void Transition(CameraTarget _target)
        {
            if (locked) return;

            SetPanningArea(_target);
            StartCoroutine(TransitionRoutine(_target));
        }

        public void SetTo(CameraTarget _target)
        {
            if (locked) return;

            SetPanningArea(_target);
            StartCoroutine(SetWithDelay(_target));
        }

        public void Transition(int _index)
        {
            if (GameManager.currentPanel == CurrentPanel.None )
            {
                Transition(targets[_index]);
                //StartCoroutine(DelaySetIsland((CurrentIsland)(_index + 1), 2));
                //GameManager.currentIsland = (CurrentIsland)(_index + 1);
                isAtOrigin = false;
                currentTarget = _index;
            }
        }

        public void SetTo(int _index)
        {
            if (GameManager.currentPanel == CurrentPanel.None)
            {
                SetTo(targets[_index]);
                //StartCoroutine(DelaySetIsland((CurrentIsland)(_index + 1), 2));
                //GameManager.currentIsland = (CurrentIsland)(_index + 1);
                isAtOrigin = false;
                currentTarget = _index;
            }
        }

        public void TransitionToOrigin()
        {
            if (GameManager.currentPanel == CurrentPanel.None )
            {
                Transition(origin);
                //StartCoroutine(DelaySetIsland((CurrentIsland)(0), 2));
                //GameManager.currentIsland = (CurrentIsland)(0);
                currentTarget = -1;
            }
        }

        public void SetToOrigin()
        {
            if (GameManager.currentPanel == CurrentPanel.None)
            {
                SetTo(origin);
                //StartCoroutine(DelaySetIsland((CurrentIsland)(0), 2));
                //GameManager.currentIsland = (CurrentIsland)(0);
                isAtOrigin = true;
                currentTarget = -1;
            }
        }

        private IEnumerator TransitionRoutine(CameraTarget _target)
        {
            locked = true;
            Iteration iteration = new Iteration(settings.transitionDuration, settings.transitionCurve);
            Vector2 startPosition = CurrentCam.transform.localPosition;
            float startSize = CurrentCam.orthographicSize;

            GameManager.currentIsland = CurrentIsland.Transition;

            onTransitionStart.Invoke();

            while (iteration.isBelowOne)
            {
                CurrentCam.transform.localPosition = Vector2.Lerp(startPosition, _target.waypoint.position, iteration.curveEvaluation);
                CurrentCam.orthographicSize = Mathf.Lerp(startSize, _target.size, iteration.curveEvaluation);

                yield return iteration.YieldIncrement();
            }

            CurrentCam.transform.localPosition = _target.waypoint.position;
            CurrentCam.orthographicSize = _target.size;
            locked = false;
            GameManager.currentIsland = _target.island;
            isAtOrigin = _target.island == CurrentIsland.Origin;

            onTransitionEnd.Invoke();
        }

        private IEnumerator SetWithDelay(CameraTarget _target)
        {
            locked = true;
            GameManager.currentIsland = CurrentIsland.Transition;

            onTransitionStart.Invoke();

            yield return new WaitForSeconds(settings.transitionDuration / 2.0f);

            CurrentCam.transform.localPosition = _target.waypoint.position;
            CurrentCam.orthographicSize = _target.size;

            yield return new WaitForSeconds(settings.transitionDuration / 2.0f);

            locked = false;
            GameManager.currentIsland = _target.island;

            onTransitionEnd.Invoke();
        }

        private IEnumerator DelaySetIsland(CurrentIsland _island, int _frameDelay)
        {
            for (int i = 0; i < _frameDelay; i++)
            {
                yield return null;
            }

            GameManager.currentIsland = _island;
        }

        // Zoom //
        private void SetPanningArea(CameraTarget _target)
        {
            panningArea = new PanningArea(_target, settings.maxZoomValue, settings.minWidth);
        }

        public void Zoom()
        {
            if(Gesture.Pinching)
            {
                Vector2 areaPos = panningArea.GetAreaPosition(CurrentCam.transform.localPosition);

                panningArea.ZoomIn(Gesture.PinchDeltaValue * settings.zoomForce);
                CurrentCam.orthographicSize = panningArea.camSize;

                CurrentCam.transform.localPosition = panningArea.WorldFromAreaPosition(areaPos);
            }
            else if(Input.mouseScrollDelta.y !=0)
            {
                Vector2 areaPos = panningArea.GetAreaPosition(CurrentCam.transform.localPosition);

                panningArea.ZoomIn(Input.mouseScrollDelta.y*0.1f * settings.zoomForce);
                CurrentCam.orthographicSize = panningArea.camSize;

                CurrentCam.transform.localPosition = panningArea.WorldFromAreaPosition(areaPos);
            }
        }

        public void Pan()
        {
            if(Gesture.Touching && !Gesture.MultipleTouches)
            {
                Vector2 newPos = (Vector2)CurrentCam.transform.localPosition - (Gesture.DeltaMovement*settings.panningSpeed);
                if (panningArea.Contains(newPos))
                {
                    CurrentCam.transform.localPosition = newPos;
                }
                else
                {
                    //Debug.Log("Out of Boundaries: "+ newPos+" \n"+panningArea.topRight + " - " + panningArea.bottomLeft);
                }
            }
        }

        public void Swipe(int _value)
        {
            if (isAtOrigin || !allowControl || GameManager.currentPanel != CurrentPanel.None || panningArea.zoom > settings.allowNavigationSwipeThreshold) return;

            int nextTarget = currentTarget + _value;
            nextTarget = nextTarget > 3 ? 0 : nextTarget;
            nextTarget = nextTarget < 0 ? 3 : nextTarget;

            Debug.Log("target: " + nextTarget);

            Transition(nextTarget);
        }

        private void OnDrawGizmos()
        {
            if(panningArea != null)
            {
                Gizmos.color = Color.green;

                Gizmos.DrawLine(debugBottomLeft.position, debugBottomLeft.position + Vector3.right * panningArea.width);
                Gizmos.DrawLine(debugBottomLeft.position, debugBottomLeft.position + Vector3.up * panningArea.height);
                Gizmos.DrawLine(debugTopRight.position, debugTopRight.position - Vector3.right * panningArea.width);
                Gizmos.DrawLine(debugTopRight.position, debugTopRight.position - Vector3.up * panningArea.height);

                Gizmos.DrawSphere(debugBottomLeft.position, 0.1f);
                Gizmos.DrawSphere(debugTopRight.position, 0.1f);
            }
        }

    }
}