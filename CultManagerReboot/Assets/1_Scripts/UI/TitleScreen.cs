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

        private void Start()
        {
            title.SetActive(true);
            loading.SetActive(false);
        }

        public void StartGame()
        {
            StartCoroutine(LoadLevel(1));
            title.SetActive(false);
            loading.SetActive(true);
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
    }
}

