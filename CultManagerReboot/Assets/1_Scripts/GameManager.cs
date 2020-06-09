using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



namespace CultManager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private bool standalonePuzzleMode = false;
        [Header("Managers")]

        [SerializeField] private SaveManager saveManager = default;
        [SerializeField] private CultManager cultManager = default;
        [SerializeField] private InfluenceManager influenceManager = default;
        //[SerializeField] private PoliceManager policeManager = default;
        [SerializeField] private InvestigationManager investigationManager = default;
        [SerializeField] private MoneyManager moneyManager = default;
        [SerializeField] private UIGeneralManager uiManager = default;

        //[SerializeField] private AltarManager altarManager = default;
        [SerializeField] private BloodBankManager bloodManager = default;
        [SerializeField] private PuzzeManager puzzeManager = default;
        [SerializeField] private DemonManager demonManager = default;
        [SerializeField] private NoteTabPanelBehavior noteTabManager = default;
        [SerializeField] private StatueManager statueManager = default;

        [Header("Data")]
        [SerializeField] private CultData cult = default;

        [SerializeField] private ResetScreen reset = default;


        public static CurrentIsland currentIsland;
        public static CurrentPanel currentPanel;
        public static int currentLevel;

        [SerializeField] private CurrentIsland island;
        [SerializeField] private CurrentPanel panel;

        private bool isHome = true;

        private void Awake()
        {
            currentIsland = CurrentIsland.Origin;
            currentPanel = CurrentPanel.None;
            saveManager?.Loadgame();

            if (standalonePuzzleMode) StandaloneAwake();
            else NormalAwake();
        }

        void Update()
        {

            if(!standalonePuzzleMode) currentLevel = cult.currentlevel;
            island = currentIsland;
            panel = currentPanel;

            if (isHome && Input.GetKeyDown(KeyCode.Escape))
            {
                Quit();
            } 
        }

        private void StandaloneAwake()
        {
            if (!SaveManager.saveLoaded)
            {
                puzzeManager.SAResetData();
                bloodManager.ResetData();
                demonManager.ResetData();
                noteTabManager.SetNoteTabSegments();
            }
            else
            {
                puzzeManager.LoadData();
            }

            uiManager?.UpdateDisplayer();
        }

        private void NormalAwake()
        {
            if (!SaveManager.saveLoaded)
            {
                cultManager.ResetData();
                investigationManager.ResetData();
                moneyManager.ResetData();
                puzzeManager.ResetData();

                bloodManager.ResetData();
                demonManager.ResetData();
                noteTabManager.SetNoteTabSegments();
                statueManager.ResetData();
            }
            else
            {
                currentLevel = cult.currentlevel;
                //altarManager.InitAltarParts();
                puzzeManager.LoadData();
                investigationManager.InitAysnchValues();
            }

            uiManager?.UpdateDisplayer();
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
            ////reset.ActivateReset();

            //cultManager.ResetCult(level);
            //investigationManager.ResetCult(level);
            //moneyManager.ResetCult(level);
            //puzzeManager.ResetCult(level);

            //bloodManager.ResetCult(level);
            //demonManager.ResetCult(level);
            ////altarManager.ResetCult(level);

            //saveManager.SaveGame();
            //StartCoroutine(GetToloadingScene());
        }

        public void StandaloneReset()
        {
            puzzeManager.SAResetCult();
            demonManager.ResetCult(0);
            bloodManager.ResetCult(0);
            noteTabManager.SetNoteTabSegments();
        }

        public IEnumerator GetToloadingScene()
        {
            yield return new WaitForSeconds(2.0f);

            SceneManager.LoadScene(3);
        }
    }
}

