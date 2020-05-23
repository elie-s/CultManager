using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Animations/UI/Squeeze")]
    public class SqueezeAnimation : UIAnimation
    {
        [SerializeField] private Vector3 deformationMax = default;
        [SerializeField] private Vector3 deformationMin = default;
        [SerializeField] private AnimationCurve animation = default;
        [SerializeField] private float forceMax = 1.0f;
        [SerializeField] private float forceMin = 1.0f;

        public override IEnumerator Play(float _duration, RectTransform _transform)
        {
            Vector3 startScale = _transform.localScale;
            Iteration iteration = new Iteration(_duration, animation);

            while (iteration.isBelowOne)
            {
                float currentValue = iteration.curveEvaluation;

                _transform.localScale = currentValue >= 0.5 ? Vector3.Lerp(startScale, deformationMax.normalized * forceMax, currentValue * 2.0f - 1.0f) :
                                                            Vector3.Lerp(deformationMin.normalized * forceMin, startScale, currentValue * 2.0f);

                yield return iteration.YieldIncrement();
            }

            _transform.localScale = startScale;
        }
    }
}