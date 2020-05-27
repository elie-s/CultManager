using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CultManager
{
    public class StandaloneTransitionSceneHandler : MonoBehaviour
    {
        [SerializeField] private int mainSceneIndex = 0;
        [SerializeField] private SaveSettings save = default;

        public void LoadScene()
        {
            if (File.Exists(save.dataPath)) File.Delete(save.dataPath);

            SceneManager.LoadSceneAsync(0);
        }
    }
}