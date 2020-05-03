using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class SacrificedBehaviour : MonoBehaviour
    {
        [SerializeField] private float upShift = 1.0f;
        [SerializeField] private AnimationCurve upShiftCurve = default;
        [SerializeField] private float duration = 1.0f;

        public Cultist cultist { get; private set; }

        public IEnumerator DieRoutine(Vector2 _endPos, System.Action<Cultist> _callback)
        {
            Iteration iteration = new Iteration(duration);
            Vector2 startPos = transform.position;

            float x = default;
            float y = default;
            float yDiff = _endPos.y - startPos.y;

            while (iteration.isBelowOne)
            {
                x = Mathf.Lerp(startPos.x, _endPos.x, iteration.fraction);
                y = Mathf.Lerp(startPos.y, _endPos.y, iteration.fraction) + upShiftCurve.Evaluate(iteration.fraction) * yDiff * upShift;

                transform.position = new Vector2(x, y);

                yield return iteration.YieldIncrement();
            }

            _callback(cultist);
            Debug.Log("Sacrficed !");
        }

        public void SetCultist(Cultist _cultist)
        {
            cultist = _cultist;
        }
    }
}