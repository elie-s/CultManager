using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class CultistAnimationBehaviour : MonoBehaviour
    {
        [SerializeField] private Platform platform;
        [SerializeField] private CultistAnimationState state = default;
        [SerializeField, DrawScriptable] private CultistAnimationSettings settings = default;


        

        private void OnEnable()
        {
            StartCoroutine(BehavioursRoutine());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        public void Init(Platform _platform)
        {
            platform = _platform;
            platform.RegisterCultist();
        }

        public void OnDestroy()
        {
            platform.UnregisterCultist();
        }



        private IEnumerator BehavioursRoutine()
        {
            if (settings.idleWanderRandom.Binary) yield return IdlingRoutine();
            else yield return WanderingRoutine();

            state = CultistAnimationState.idle;

            yield return new WaitForSeconds(1.0f);

            StartCoroutine(BehavioursRoutine());
        }

        private IEnumerator WanderingRoutine()
        {
            state = CultistAnimationState.wandering;

            Vector2 endPos = GetNewPos();
            Vector2 startPos = transform.localPosition;
            Iteration iteration = new Iteration(Vector2.Distance(startPos, endPos) / settings.movementSpeed);

            while (iteration.isBelowOne)
            {
                transform.localPosition = Vector2.Lerp(startPos, endPos, iteration.fraction);

                yield return iteration.YieldIncrement();
            }

            transform.localPosition = endPos;
        }


        private IEnumerator IdlingRoutine()
        {
            state = CultistAnimationState.idle;

            yield return new WaitForSeconds(Random.Range(settings.minIdleDuration, settings.maxIdleDuration));
        }

        private Vector2 GetNewPos()
        {
            if (!platform) return transform.localPosition;

            Vector2 result = default;

            do
            {
                result = platform.Evaluate(Random.value);
            } while (Vector2.Distance(result, transform.position) < settings.minimumDistance);

            Vector2 tmpPos = transform.position;
            transform.position = result;
            result = transform.localPosition;
            transform.position = tmpPos;

            return result;
        }
    }
}