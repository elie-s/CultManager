using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace CultManager
{
    public class TitlescreenManager : MonoBehaviour
    {
        [SerializeField] private string gameScene = default;
        [SerializeField] private string godScene = default;
        [SerializeField] private string puzzleScene = default;
        [SerializeField] private GameObject title = default;
        [SerializeField] private GameObject loading = default;
        [SerializeField] private Image loadingBar = default;
        [Header("Navigation")]
        [SerializeField] private CanvasGroup main = default;
        [SerializeField] private CanvasGroup jury = default;
        [SerializeField] private float switchDuration = 1.0f;

        private bool switching = false;
        private bool isJury = false;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) Quit();
        }

        public void Quit()
        {
            if (isJury) ToMain();
            else Application.Quit();
        }

        public void ToJury()
        {
            if (!switching)
            {
                StartCoroutine(Switch(main, jury));
                isJury = true;
            }
        }

        public void ToMain()
        {
            if (!switching)
            {
                StartCoroutine(Switch(jury, main));
                isJury = false;
            }
        }

        private IEnumerator Switch(CanvasGroup _current, CanvasGroup _new)
        {
            switching = true;

            Iteration iteration = new Iteration(switchDuration * 0.5f);

            while (iteration.isBelowOne)
            {
                _current.alpha = 1.0f - iteration.fraction;

                yield return iteration.YieldIncrement();
            }

            _current.gameObject.SetActive(false);
            _new.gameObject.SetActive(true);
            iteration = new Iteration(switchDuration * 0.5f);

            while (iteration.isBelowOne)
            {
                _new.alpha = iteration.fraction;

                yield return iteration.YieldIncrement();
            }

            _new.alpha = 1.0f;
            switching = false;
        }

        public void LoadGameScene()
        {
            LoadScene(gameScene);
        }

        public void LoadPuzzle()
        {
            LoadScene(puzzleScene);
        }

        public void LoadGodmode()
        {
            LoadScene(godScene);
        }

        public void LoadScene(string _scene)
        {
            title.SetActive(false);
            loading.SetActive(true);
            StartCoroutine(LoadLevel(_scene));
        }

        IEnumerator LoadLevel(string _scene)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(_scene);
            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                loadingBar.fillAmount = progress;

                yield return null;
            }
        }
    }
}