using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class OpenSceneFade : MonoBehaviour
    {
        [SerializeField] CanvasGroup canvasGroup = default;
        [SerializeField] private float duration = 1.0f;

        private void Awake()
        {
            canvasGroup.gameObject.SetActive(true);
            StartCoroutine(FadeOut());
        }

        private IEnumerator FadeOut()
        {
            Iteration iteration = new Iteration(duration);

            yield return new WaitForSeconds(0.1f);

            while (iteration.isBelowOne)
            {
                canvasGroup.alpha = 1.0f - iteration.fraction;

                yield return iteration.YieldIncrement();
            }

            Destroy(gameObject);
        }
    }
}