using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

namespace CultManager
{
    public class ScrollTree : MonoBehaviour
    {
        [SerializeField] private RectTransform scrollArea = default;
        [SerializeField] private float[] positions = default;
        [SerializeField] private float slideForce = 1.0f;
        [SerializeField] private float smoothValue = 0.1f;

        private float currentPos => scrollArea.anchoredPosition.y;

        void Update()
        {
            if (Gesture.BeginTouch) StartCoroutine(HandleMovement());
        }

        private IEnumerator HandleMovement()
        {
            yield return null;

            float startPos = currentPos;

            while (Gesture.Touching)
            {
                scrollArea.anchoredPosition = new Vector2(scrollArea.anchoredPosition.x, startPos + Gesture.Movement.y * slideForce * Screen.height);

                yield return null;
            }

            float closest = GetClosestPoint2(startPos < currentPos);

            while (GetDifference(currentPos, closest) > 1.0f)
            {
                scrollArea.anchoredPosition = Vector2.Lerp(scrollArea.anchoredPosition, new Vector2(scrollArea.anchoredPosition.x, closest), smoothValue);

                if (Gesture.BeginTouch) break;

                yield return null;
            }
        }

        private float GetClosestPoint()
        {
            float difference = GetDifference(currentPos, positions[0]);

            for (int i = 1; i < positions.Length; i++)
            {
                if (difference > GetDifference(currentPos, positions[i])) return positions[i];
            }

            return positions[positions.Length - 1];
        }

        private float GetDifference(float _a, float _b)
        {
            float a = Mathf.Abs(_a);
            float b = Mathf.Abs(_b);

            return a > b ? a - b : b - a;
        }

        private float GetClosestPoint2(bool _down)
        {
            for (int i = 0; i < positions.Length -1; i++)
            {
                Debug.Log("current pos: " + currentPos + " < (" + positions[i] + ", " + positions[i + 1] + ") -> " + Mathf.Lerp(positions[i], positions[i + 1], 0.5f));
                if (currentPos < Mathf.Lerp(positions[i], positions[i + 1], _down ? 0.25f : 0.75f)) return positions[i];
            }


            return positions[positions.Length - 1];
        }
    }
}