using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class UIAnimator : MonoBehaviour
    {
        [SerializeField] private RectTransform uiElement = default;
        [SerializeField] private float duration = 1.0f;
        [SerializeField, DrawScriptable] private UIAnimation uiAnimation = default;

        private bool inUse = false;

        [ContextMenu("Play")]
        public void Play()
        {
            if (!inUse) StartCoroutine(AnimationRoutine(duration));
        }

        public void Play(float _duration)
        {
            if (!inUse) StartCoroutine(AnimationRoutine(_duration));
        }

        private IEnumerator AnimationRoutine(float _duration)
        {
            inUse = true;
            yield return uiAnimation.Play(_duration, uiElement);
            inUse = false;
        }
    }
}