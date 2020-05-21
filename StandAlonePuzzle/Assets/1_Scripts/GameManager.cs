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
        [SerializeField] private BloodBankManager bloodManager = default;
        [SerializeField] private PuzzeManager puzzeManager = default;
        [SerializeField] private DemonManager demonManager = default;
        [SerializeField] private NoteTabPanelBehavior noteTabManager = default;

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

            if (!SaveManager.saveLoaded)
            {
                puzzeManager.ResetData();

                bloodManager.ResetData();
                demonManager.ResetData();
                noteTabManager.SetNoteTabSegments();
            }

            else
            {
                puzzeManager.LoadData();
            }
        }

        void Update()
        {
            if (Gesture.QuickTouch) Debug.Log("QuickTouch");
            if (Gesture.LongTouch) Debug.Log("Longtouch");

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
            reset.ActivateReset("Congratulations" + "\n" + "You invoked the demon!" + "\n" + "Your new Cult awaits you");

            puzzeManager.ResetCult(level);

            bloodManager.ResetCult(level);
            demonManager.ResetCult(level);

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

