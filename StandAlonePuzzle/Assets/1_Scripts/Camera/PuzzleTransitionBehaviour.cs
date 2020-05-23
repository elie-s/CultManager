using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CultManager
{
    public class PuzzleTransitionBehaviour : MonoBehaviour
    {
        [SerializeField] private CameraController controller = default;
        [SerializeField] private Transform puzzleLocation = default;
        [Header("Events")]
        [SerializeField] private UnityEvent onMidTransitionToPuzzle = default;
        [SerializeField] private UnityEvent onPuzzleReached = default;
        [SerializeField] private UnityEvent onMidTransitionToIsland = default;
        [SerializeField] private UnityEvent onBackToIsland = default;

        [SerializeField, DrawScriptable] private PuzzleTransitionSettings settings = default;

        private bool isTransitionning;

        public void GoToPuzzle()
        {
            if (isTransitionning) return;

            StartCoroutine(GoToPuzzleRoutine());
        }

        public void GoToIsland()
        {
            if (isTransitionning) return;

            StartCoroutine(GoToIslandRoutine());
        }

        private IEnumerator GoToPuzzleRoutine()
        {
            GameManager.currentIsland = CurrentIsland.Transition;
            isTransitionning = true;
            controller.AllowControl(false);

            Vector2 startPos = CameraController.CurrentCam.transform.localPosition;
            float startZoom = controller.panningArea.zoom;
            Iteration iteration = new Iteration(settings.transitionDuration / 2.0f, settings.movementCurve);

            while (iteration.isBelowOne)
            {
                CameraController.CurrentCam.transform.localPosition = Vector2.Lerp(startPos, puzzleLocation.transform.position, iteration.curveEvaluation);
                controller.SetZoom(Mathf.Lerp(startZoom, 1.0f, iteration.fraction));
                controller.SetScreen(settings.fade.Evaluate(settings.fadeCurve.Evaluate(iteration.fraction)));

                yield return iteration.YieldIncrement();
            }

            controller.SwitchCam();
            onMidTransitionToPuzzle.Invoke();
            iteration = new Iteration(settings.transitionDuration / 2.0f);

            while (iteration.isBelowOne)
            {
                controller.SetScreen(settings.fade.Evaluate(settings.fadeCurve.Evaluate(1.0f - iteration.fraction)));

                yield return iteration.YieldIncrement();
            }

            onPuzzleReached.Invoke();

            GameManager.currentIsland = CurrentIsland.PuzzleIsland;
            isTransitionning = false;
        }

        private IEnumerator GoToIslandRoutine()
        {
            GameManager.currentIsland = CurrentIsland.Transition;
            isTransitionning = true;

            Iteration iteration = new Iteration(settings.transitionDuration / 2.0f);

            while (iteration.isBelowOne)
            {
                controller.SetScreen(settings.fade.Evaluate(settings.fadeCurve.Evaluate(iteration.fraction)));

                yield return iteration.YieldIncrement();
            }

            controller.SwitchCam();
            onMidTransitionToIsland.Invoke();
            iteration = new Iteration(settings.transitionDuration / 2.0f);

            while (iteration.isBelowOne)
            {
                controller.SetScreen(settings.fade.Evaluate(settings.fadeCurve.Evaluate(1.0f - iteration.fraction)));

                yield return iteration.YieldIncrement();
            }

            onBackToIsland.Invoke();

            GameManager.currentIsland = CurrentIsland.PuzzleIsland;
            isTransitionning = false;
            controller.AllowControl(true);
        }
    }
}