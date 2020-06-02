using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Animations/UI/Tilt")]
    public class TiltAnimation : UIAnimation
    {
        [SerializeField] private float angleSpan = 15.0f;
        [SerializeField] private AnimationCurve animation = default;

        public override IEnumerator Play(float _duration, RectTransform _transform)
        {
            Iteration iteration = new Iteration(_duration, animation);

            while (iteration.isBelowOne)
            {
                _transform.localEulerAngles = Vector3.forward * angleSpan * (iteration.curveEvaluation * 2.0f - 1.0f);

                yield return iteration.YieldIncrement();
            }

            _transform.localEulerAngles = Vector3.zero;
        }
    }
}