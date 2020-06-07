using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Animations/UI/Jiggle")]
    public class JiggleAnimation : UIAnimation
    {
        [SerializeField] private Vector2 direction = default;
        [SerializeField] private AnimationCurve animation = default;
        [SerializeField] private float force = 1.0f;

        public override IEnumerator Play(float _duration, RectTransform _transform)
        {
            Vector2 startPos = _transform.anchoredPosition;
            Iteration iteration = new Iteration(_duration, animation);

            while (iteration.isBelowOne)
            {
                _transform.anchoredPosition = Vector2.Lerp(startPos - direction.normalized * force, startPos + direction.normalized * force, iteration.curveEvaluation);

                yield return iteration.YieldIncrement();
            }

            _transform.anchoredPosition = startPos;
        }
    }
}