using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CultManager
{
    public class SummoningSpawnSequencer : MonoBehaviour
    {
        [SerializeField] private PuzzleDisplay puzzle = default;
        [Header("Callbacks")]
        [SerializeField] private UnityEvent onHighlightDone = default;
        [SerializeField] private UnityEvent onDelayBeforeCamTransitionDone = default;
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

    }
}