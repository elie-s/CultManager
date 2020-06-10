using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class DemonSummoningBehaviour : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer effectRenderer = default;
        [SerializeField] private AnimationCurve summoningCurve = default;
        [SerializeField] private float animationForce = default;
        [SerializeField] private float timeSpawn = 0.5f;
        [SerializeField] private float animationDuration = 1.0f;

        
        private Spawn spawn;
        private bool succeeded;

        public void Init(Spawn _spawn)
        {
            spawn = _spawn;

            succeeded = _spawn.patternAccuracy > 0.995f;
        }

        public void Summon(float _delay)
        {
            StartCoroutine(SummonRoutine(animationDuration, _delay));
        }

        private IEnumerator SummonRoutine(float _duration, float _delay)
        {
            yield return new WaitForSeconds(_delay);

            Iteration iteration = new Iteration(_duration, summoningCurve);

            while (iteration.isBelowOne)
            {
                if (iteration.fraction > timeSpawn && succeeded)
                {
                    FindObjectOfType<SummoningSpawnSequencer>().OnResurrectionSucceded();
                }

                effectRenderer.transform.localScale = Vector3.one * animationForce * iteration.curveEvaluation;

                yield return iteration.YieldIncrement();
            }

            effectRenderer.transform.localScale = Vector3.zero;
            if (!succeeded) FindObjectOfType<SummoningSpawnSequencer>().OnResurrectionFailed();

        }
    }
}