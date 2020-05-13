using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;


namespace CultManager
{
    public class TitleScreen : MonoBehaviour
    {
        [SerializeField] private GameObject title;
        [SerializeField] private GameObject loading;
        [SerializeField] private GameObject registration = default;

        [SerializeField] private Image loadingBar;
        [SerializeField] private SaveManager saveManager = default;
        [SerializeField] private CultData data = default;

        [SerializeField] private DataRecorderSettings dataRecorderSettings = default;
        [SerializeField] private TMP_InputField inputField = default;

        private void Awake()
        {
            saveManager.Loadgame();
        }

        private void Start()
        {
            if (!SaveManager.saveLoaded || data.currentlevel == 0) registration.SetActive(true);

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

        public void RegisterTester()
        {
            if (inputField.text.Length < 3) return;

            dataRecorderSettings.testerName = inputField.text;
            registration.SetActive(false);
        }
    }
}

