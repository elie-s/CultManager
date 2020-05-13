using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;


namespace CultManager
{
    public class TitleScreen : MonoBehaviour
    {
        [SerializeField] private GameObject title;
        [SerializeField] private GameObject loading;

        [SerializeField] private Image loadingBar;
        [SerializeField] private SaveManager saveManager = default;
        [SerializeField] private CultData data = default;

        private void Awake()
        {
            saveManager.Loadgame();
        }

        private void Start()
        {
            title.SetActive(true);
            loading.SetActive(false);
        }

        public void StartGame()
        {
            title.SetActive(false);
            loading.SetActive(true);
            StartCoroutine(Delay(0.5f));
        }

        IEnumerator LoadLevel(int index)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(index);
            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                loadingBar.fillAmount = progress;

                yield return null;
            }
        }

        private IEnumerator Delay(float _delay)
        {
            yield return new WaitForSeconds(_delay);

            StartCoroutine(LoadLevel(!SaveManager.saveLoaded || data.currentlevel == 0 ? 1 : 2));
        }
    }
}

