using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Juicy/FloatingSettings")]
    public class FloatingSettings : ScriptableObject
    {
        public float span = 1.0f;
        [DrawScriptable] public AnimationSet set;


        public IEnumerator Play(Transform _transform)
        {
            Iteration iteration = new Iteration(set.animationDuration/2.0f, set.curve);

            Vector2 startPos = _transform.position;
            Vector2 endPos = startPos + Vector2.up * span;

            while (iteration.isBelowOne)
            {
                _transform.position = Vector2.Lerp(startPos, endPos, iteration.curveEvaluation);

                yield return iteration.YieldIncrement();
            }

            _transform.position = endPos;
            iteration = new Iteration(set.animationDuration, set.curve);

            while (iteration.isBelowOne)
            {
                _transform.position = Vector2.Lerp(startPos, endPos, 1.0f - iteration.curveEvaluation);

                yield return iteration.YieldIncrement();
            }

            _transform.position = startPos;
        }

        public IEnumerator PlayLocal(Transform _transform)
        {
            Iteration iteration = new Iteration(set.animationDuration / 2.0f, set.curve);

            Vector2 startPos = _transform.localPosition;
            Vector2 endPos = startPos + Vector2.up * span;

            while (iteration.isBelowOne)
            {
                _transform.localPosition = Vector2.Lerp(startPos, endPos, iteration.curveEvaluation);

                yield return iteration.YieldIncrement();
            }

            _transform.localPosition = endPos;
            iteration = new Iteration(set.animationDuration, set.curve);

            while (iteration.isBelowOne)
            {
                _transform.localPosition = Vector2.Lerp(startPos, endPos, 1.0f - iteration.curveEvaluation);

                yield return iteration.YieldIncrement();
            }

            _transform.localPosition = startPos;
        }
    }
}