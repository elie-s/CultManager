using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



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

        [SerializeField] private AltarManager altarManager = default;
        [SerializeField] private BloodBankManager bloodManager = default;
        [SerializeField] private PuzzeManager puzzeManager = default;
        [SerializeField] private DemonManager demonManager = default;
        [SerializeField] private NoteTabPanelBehavior noteTabManager = default;

        [Header("Data")]
        [SerializeField] private CultData cult;

        [SerializeField] private ResetScreen reset = default;


        public static CurrentIsland currentIsland;
        public static CurrentPanel currentPanel;
        public static int currentLevel;

        [SerializeField] private CurrentIsland island;
        [SerializeField] private CurrentPanel panel;

        private bool isHome = true;

        private void Awake()
        {
            currentLevel = cult.currentlevel;
            currentIsland = CurrentIsland.Origin;
            saveManager?.Loadgame();
            influenceManager?.InitializeData();

            if (!SaveManager.saveLoaded || cult.currentlevel == 0)
            {
                cultManager.ResetData();
                policeManager.ResetData();
                moneyManager.ResetData();
                puzzeManager.ResetData();

                bloodManager.ResetData();
                demonManager.ResetData();
                altarManager.ResetData();
                noteTabManager.SetNoteTabSegments();
            }

            else
            {
                altarManager.InitAltarParts();
                puzzeManager.LoadData();
                policeManager.InitAysnchValues();
            }
        }

        void Update()
        {
            if (Gesture.QuickTouch) Debug.Log("QuickTouch");
            if (Gesture.LongTouch) Debug.Log("Longtouch");

            currentLevel = cult.currentlevel;
            island = currentIsland;
            panel = currentPanel;
            if (isHome && Input.GetKeyDown(KeyCode.Escape))
            {
                Quit();
            } 
        }

        public void SaveGame()
        {
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

        [ContextMenu("Reset Cult")]

        public void ResetThisCult()
        {
            ResetCult(0);
        }

        public void ResetCult(int level)
        {
            reset.ActivateReset();

            cultManager.ResetCult(level);
            policeManager.ResetCult(level);
            moneyManager.ResetCult(level);
            puzzeManager.ResetCult(level);

            bloodManager.ResetCult(level);
            demonManager.ResetCult(level);
            altarManager.ResetCult(level);

            saveManager.SaveGame();
            StartCoroutine(GetToloadingScene());
        }

        public IEnumerator GetToloadingScene()
        {
            yield return new WaitForSeconds(2.0f);

            SceneManager.LoadScene(3);
        }
    }
}

