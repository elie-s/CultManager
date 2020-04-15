using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class GameManager : MonoBehaviour
    {
        [Header("Managers")]
        [SerializeField] private SaveManager saveManager = default;
        [SerializeField] private CultManager cultManager = default;
        [SerializeField] private InfluenceManager influenceManager = default;
        [SerializeField] private PoliceManager policeManager = default;
        [SerializeField] private MoneyManager moneyManager = default;

        private bool isHome = true;

        private void Awake()
        {
            saveManager?.Loadgame();
            cultManager?.InitalizeData();
            influenceManager?.InitializeData();
            policeManager?.InitializeData();
            moneyManager?.InitializeData();
        }

        void Update()
        {
            if (isHome && Input.GetKeyDown(KeyCode.Escape))
            {
                Quit();
            } 
        }

        public void SaveGame()
        {
            Debug.Log("SAVED BOI");
            saveManager.SaveGame();
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }

        public void OnApplicationFocus(bool focus)
        {
            if (!focus)
            {
                SaveGame();
            }
            
        }

        public void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                SaveGame();
            }
                
        }

        public void Quit()
        {
            SaveGame();
            Application.Quit();
        }

    }
}

