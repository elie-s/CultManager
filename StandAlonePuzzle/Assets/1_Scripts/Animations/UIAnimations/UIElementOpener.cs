using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class UIElementOpener : MonoBehaviour
    {
        [SerializeField] private RectTransform element = default;
        [SerializeField] private RectTransform closePosition = default;
        [SerializeField] private RectTransform openPosition = default;
        [SerializeField] private AnimationCurve movementCurve = default;
        [SerializeField] private float animationDuration = default;
        [SerializeField] private bool startOpen = false;
        [SerializeField] private bool deactivateWhenClosed = true;

        private bool moving;

        private void Start()
        {
            ResetPosition();
        }

        public void ResetPosition()
        {
            if (startOpen) SetOpen();
            else SetClose();
        }

        public void SetOpen()
        {
            StopAllCoroutines();
            moving = false;
            element.gameObject.SetActive(true);
            element.anchoredPosition = openPosition.anchoredPosition;
        }

        public void SetClose()
        {
            StopAllCoroutines();
            moving = false;
            element.gameObject.SetActive(!deactivateWhenClosed);
            element.anchoredPosition = closePosition.anchoredPosition;
        }

        public void Open()
        {
            if (!moving) StartCoroutine(OpenRoutine());
        }

        public void Close()
        {
            if (!moving) StartCoroutine(CloseRoutine());
        }

        private IEnumerator OpenRoutine()
        {
            moving = true;
            element.gameObject.SetActive(true);

            Iteration iteration = new Iteration(animationDuration, movementCurve);

            while (iteration.isBelowOne)
            {
                element.anchoredPosition = Vector2.Lerp(closePosition.anchoredPosition, openPosition.anchoredPosition, iteration.curveEvaluation);

                yield return iteration.YieldIncrement();
            }

            element.anchoredPosition = openPosition.anchoredPosition;
        }

        private IEnumerator CloseRoutine()
        {
            moving = true;

            Iteration iteration = new Iteration(animationDuration, movementCurve);

            while (iteration.isBelowOne)
            {
                element.anchoredPosition = Vector2.Lerp(openPosition.anchoredPosition, closePosition.anchoredPosition, iteration.curveEvaluation);

                yield return iteration.YieldIncrement();
            }

            element.anchoredPosition = closePosition.anchoredPosition;
            moving = false;
            element.gameObject.SetActive(!deactivateWhenClosed);
        }
    }
}