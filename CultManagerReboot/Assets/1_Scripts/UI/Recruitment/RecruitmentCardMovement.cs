using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace CultManager
{
    public class RecruitmentCardMovement : MonoBehaviour
    {
        [SerializeField] private CanvasGroup group = default;
        [SerializeField] private RectTransform rectTransform = default;
        [SerializeField, Range(-1.0f, 1.0f)] private float lerpValue = 0.0f;
        [SerializeField] private UnityEvent onSwipeLeft = default;
        [SerializeField] private UnityEvent onSwipeRight = default;
        [SerializeField, DrawScriptable] private RecruitmentCardMovementSettings settings = default;

        private Vector2 startPosition;
        bool isExiting = false;

        // Start is called before the first frame update
        void Start()
        {
            startPosition = rectTransform.anchoredPosition;
        }

        // Update is called once per frame
        void Update()
        {
            InputHandler();
            LerpsHandler();
            BackToNormal();
        }

        private void LerpsHandler()
        {
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, settings.endPosition, settings.movementCurve.Evaluate(Mathf.Abs(lerpValue)));
            rectTransform.localEulerAngles = Vector3.Lerp(Vector3.zero, settings.endRotation, settings.rotationCurve.Evaluate(Mathf.Abs(lerpValue)));
            group.alpha = Mathf.Lerp(1.0f, 0.0f, settings.fadeCurve.Evaluate(Mathf.Abs(lerpValue)));

            if(lerpValue < 0)
            {
                rectTransform.anchoredPosition = new Vector2(-rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y);
                rectTransform.localEulerAngles *= -1;
            }
        }

        private void BackToNormal()
        {
            if (!Gesture.Touching && !isExiting) lerpValue = Mathf.Lerp(lerpValue, 0.0f, settings.backForce);
        }

        private void InputHandler()
        {
            if (isExiting) return;

            if (Gesture.BeginTouch) StartCoroutine(SlideRoutine());
        }

        private IEnumerator SlideRoutine()
        {
            yield return null;

            while (Gesture.Touching)
            {
                lerpValue += Gesture.DeltaMovement.x * settings.slideMultiplier;

                yield return null;

                if(Mathf.Abs(lerpValue) > settings.slideThreshold)
                {
                    StartCoroutine(ExitRoutine());
                    break;
                }
            }
        }

        private IEnumerator ExitRoutine()
        {
            Debug.Log("Exiting");
            isExiting = true;

            while (Mathf.Abs(lerpValue) < 1.0f)
            {
                lerpValue = lerpValue > 0 ? lerpValue + settings.exitingSpeed / 100.0f : lerpValue - settings.exitingSpeed / 100.0f;

                yield return null;
            }

            lerpValue = lerpValue > 0 ? 1.0f : -1.0f;

            if (lerpValue < 0) onSwipeLeft.Invoke();
            else onSwipeRight.Invoke();
        }

        public void ResetValues()
        {
            lerpValue = 0;
            isExiting = false;
            StopAllCoroutines();
        }
    }
}