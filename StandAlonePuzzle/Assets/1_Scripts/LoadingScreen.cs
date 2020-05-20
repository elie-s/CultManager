using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace CultManager
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private Image loadingBar = default;

        public void OnEnable()
        {
            StartCoroutine(Delay(2.0f));
        }

        private IEnumerator Delay(float _delay)
        {
            yield return new WaitForSeconds(_delay);

            StartCoroutine(LoadLevel(2));
        }

        IEnumerator LoadLevel(int index)
        {
            Debug.Log("Start Loading");

            AsyncOperation operation = SceneManager.LoadSceneAsync(index);
            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                loadingBar.fillAmount = progress;

                yield return null;
            }
        }
    }
}
