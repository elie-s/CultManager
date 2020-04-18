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

        public static CurrentIsland currentIsland;
        public static CurrentPanel currentPanel;

        [SerializeField] private CurrentIsland island;
        [SerializeField] private CurrentPanel panel;

        private bool isHome = true;

        private void Awake()
        {
            currentIsland = CurrentIsland.Origin;
            saveManager?.Loadgame();
            cultManager?.InitalizeData();
            influenceManager?.InitializeData();
            policeManager?.InitializeData();
            moneyManager?.InitializeData();
        }

        void Update()
        {
            island = currentIsland;
            panel = currentPanel;
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

