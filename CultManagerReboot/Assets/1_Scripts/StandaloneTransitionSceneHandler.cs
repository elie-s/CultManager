using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CultManager
{
    public class StandaloneTransitionSceneHandler : MonoBehaviour
    {
        [SerializeField] private string mainSceneIndex = "PuzzleSAScene";
        [SerializeField] private SaveSettings save = default;

        public void LoadScene()
        {
            SceneManager.LoadSceneAsync(mainSceneIndex);
        }
    }
}