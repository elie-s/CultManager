using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CultManager
{
    public class InvestigatorBehaviour : MonoBehaviour
    {
        [SerializeField] private CultistAnimationBehaviour animationBehaviour = default;
        [SerializeField] private SpriteRenderer popup = default;
        [Header("Settings")]
        [SerializeField] private float popupDuration = 4.0f;
        [SerializeField] private Gradient fade = default;
        [SerializeField] private AnimationCurve upShiftCurve = default;
        [SerializeField] private float suicideDuration = default;
        [SerializeField] private float upShift = 1.0f;

        [ContextMenu("Suspicious")]
        public void SuspiciousBehaviour()
        {
            StartCoroutine(SuspiciousBehaviourRoutine());
        }

        private IEnumerator SuspiciousBehaviourRoutine()
        {
            Iteration iteration = new Iteration(popupDuration);
            popup.gameObject.SetActive(true);


            while (iteration.isBelowOne)
            {
                popup.color = fade.Evaluate(iteration.fraction);

                yield return iteration.YieldIncrement();
            }

            popup.gameObject.SetActive(false);
        }

        public void Suicide()
        {
            StartCoroutine(DieRoutine());
        }

        private IEnumerator DieRoutine()
        {
            animationBehaviour.Stop();
            popup.gameObject.SetActive(false);

            Iteration iteration = new Iteration(suicideDuration);
            Vector2 startPos = transform.position;
            Vector2 endPos = new Vector2(CameraController.CurrentCam.transform.position.x, CameraController.CurrentCam.transform.position.y - CameraController.CurrentCam.orthographicSize - 1);

            float x = default;
            float y = default;
            float yDiff = startPos.y - endPos.y;

            while (iteration.isBelowOne)
            {
                x = Mathf.Lerp(startPos.x, endPos.x, iteration.fraction);
                y = Mathf.Lerp(startPos.y, endPos.y, iteration.fraction) + upShiftCurve.Evaluate(iteration.fraction) * yDiff * upShift;

                transform.position = new Vector2(x, y);

                yield return iteration.YieldIncrement();
            }

            FindObjectOfType<CultManager>().RemoveCutlist(animationBehaviour.cultist);
        }
    }
}