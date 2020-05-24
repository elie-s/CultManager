using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CultManager
{
    public class SummoningSpawnSequencer : MonoBehaviour
    {
        [SerializeField] private PuzzleDisplay puzzle = default;
        [SerializeField] private SpriteRenderer opaqueMask = default;
        [Header("Callbacks")]
        [SerializeField] private UnityEvent onHighlightDone = default;
        [SerializeField] private UnityEvent onDelayBeforeCamTransitionDone = default;
        [SerializeField] private UnityEvent onSummoningStarts = default;
        [SerializeField] private UnityEvent onSpawnSummoned = default;
        [SerializeField] private UnityEvent onSequenceEnd = default;
        [SerializeField, DrawScriptable] private SummoningSpawnSequencerSettings settings = default;

        public void HighlightShape() { StartCoroutine(HighlightShapeRoutine()); }

        private IEnumerator HighlightShapeRoutine()
        {
            foreach (WorldSegmentBehaviour segment in puzzle.segments)
            {
                segment.FadeOutUnselected(settings.fadeShapeDuration, settings.fadeShapeAlphaLimit, settings.fadeCurve);
            }

            yield return new WaitForSeconds(settings.fadeShapeDuration);
            onHighlightDone.Invoke();
        }

        public void DelayCam() { StartCoroutine(DelayCamRoutine()); }

        private IEnumerator DelayCamRoutine()
        {
            yield return new WaitForSeconds(settings.delayBeforeCamTransition);
            onDelayBeforeCamTransitionDone.Invoke();
        }

        public void StartSummoning()
        {
            onSummoningStarts.Invoke();
        }

        public void InitOpaqueMask()
        {
            opaqueMask.color = settings.spawnAppearingGradient.Evaluate(0.0f);
        }

        public void FadeOpaqueMask(float _duration)
        {
            StartCoroutine(FadeOpaqueMaskRoutine(_duration));
        }

        private IEnumerator FadeOpaqueMaskRoutine(float _duration)
        {
            Iteration iteration = new Iteration(_duration, settings.spawnAppearingCurve);

            while (iteration.isBelowOne)
            {
                opaqueMask.color = settings.spawnAppearingGradient.Evaluate(iteration.curveEvaluation);

                yield return iteration.YieldIncrement();
            }

            opaqueMask.color = settings.spawnAppearingGradient.Evaluate(1.0f);
        }

        public void OnSpawnSummoned()
        {
            onSpawnSummoned.Invoke();
        }

        public void OnSequenceEnd()
        {
            onSequenceEnd.Invoke();
        }
    }
}