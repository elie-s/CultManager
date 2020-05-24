using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class SpawnSummoningBehaviour : MonoBehaviour
    {
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private SpriteRenderer sRenderer = default;
        [SerializeField] private SpriteRenderer effectRenderer = default;
        [SerializeField] private AnimationCurve summoningCurve = default;
        [SerializeField] private AnimationCurve disapearanceCurve = default;
        [SerializeField] private float animationForce = default;
        [SerializeField] private float timeSpawn = 0.5f;
        [SerializeField] private float timeExit = default;
        [SerializeField] private float animationDuration = 1.0f;

        
        private Spawn spawn;
        private System.Action callback;

        public void Init(Spawn _spawn)
        {
            spawn = _spawn;
            callback = FindObjectOfType<SummoningSpawnSequencer>().OnSpawnSummoned;
        }

        public void Summon(float _delay)
        {
            StartCoroutine(SummonRoutine(animationDuration, _delay));
        }

        public void Kill(float _delay)
        {
            StartCoroutine(DieRoutine(animationDuration, _delay));
        }

        private IEnumerator SummonRoutine(float _duration, float _delay)
        {
            yield return new WaitForSeconds(_delay);

            Iteration iteration = new Iteration(_duration, summoningCurve);

            while (iteration.isBelowOne)
            {
                if (iteration.fraction > timeSpawn && !sRenderer.gameObject.activeSelf) sRenderer.gameObject.SetActive(true);

                effectRenderer.transform.localScale = Vector3.one * animationForce * iteration.curveEvaluation;

                yield return iteration.YieldIncrement();
            }

            effectRenderer.transform.localScale = Vector3.zero;
            callback();

            Kill(3);
        }

        private IEnumerator DieRoutine(float _duration, float _delay)
        {
            yield return new WaitForSeconds(_delay);

            Iteration iteration = new Iteration(_duration, disapearanceCurve);

            while (iteration.isBelowOne)
            {
                if (iteration.fraction > timeExit && sRenderer.gameObject.activeSelf) sRenderer.gameObject.SetActive(false);

                effectRenderer.transform.localScale = Vector3.one * animationForce * iteration.curveEvaluation;

                yield return iteration.YieldIncrement();
            }

            effectRenderer.transform.localScale = Vector3.zero;

            Destroy(gameObject);
        }
    }
}