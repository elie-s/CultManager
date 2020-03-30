using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private DebugInstance debug = default;
        [Header("Managers")]
        [SerializeField] private SaveManager saveManager = default;
        [SerializeField] private CultManager cultManager = default;
        [SerializeField] private InfluenceManager influenceManager = default;
        [SerializeField] private PoliceManager policeManager = default;
        [SerializeField] private MoneyManager moneyManager = default;
        [Header("Controllers")]
        [SerializeField] private CameraController camController = default;

        private bool isHome = true;

        private void Awake()
        {
            saveManager?.Loadgame();
            cultManager?.InitalizeData();
            influenceManager?.InitializeData();
            policeManager?.InitializeData();
            moneyManager?.InitializeData();
        }

        private void Update()
        {
            if (isHome && Input.GetKeyDown(KeyCode.Escape)) Quit();
        }

        public void SaveGame()
        {
            saveManager.SaveGame();
        }

        public void DisableCamController()
        {
            camController.Disable();
        }

        public void EnableCamController()
        {
            camController.Enable();
        }

        public void SetCameraState(CameraController.CameraState _state)
        {
            camController.SetState(_state);
            isHome = _state == CameraController.CameraState.Default;
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }

        public void Quit()
        {
            SaveGame();
            Application.Quit();
        }
    }
}