using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


namespace CultManager
{
    public class DemonBookUI : MonoBehaviour
    {
        [Header("Demon Book Panel")]
        [SerializeField] private GameObject panel;
        [SerializeField] private CurrentPanel thisPanelName;
        [SerializeField] private DemonData data;

        [Header("Demon Page Display")]
        [SerializeField] private GameObject demonPage;
        [SerializeField] private GameObject puzzleGroupParent;
        [SerializeField] private Image demonImage;
        [SerializeField] private Image starImage;
        [SerializeField] private PuzzleDisplay demonPagedisplay;
        [SerializeField] private int currentIndex = 0;
        [SerializeField] private int demonPagePuzzleScale;


        [Header("Summary Page Display")]
        [SerializeField] private GameObject summaryPage;
        [SerializeField] private GameObject puzzleGroupPrefab;
        [SerializeField] private int numberOfItemsPerPage;
        [SerializeField] private int summaryPagePuzzleScale;


        [Header("Display Sprites")]
        [SerializeField] private Sprite starActive;
        [SerializeField] private Sprite starInActive;
        [SerializeField] private Sprite buttonActive;
        [SerializeField] private Sprite buttonInactive;



        public void Left()
        {
            if (currentIndex > 0)
            {
                currentIndex--;
            }
            else
            {
                currentIndex = data.demons.Count;
            }
        }

        public void Right()
        {
            if (currentIndex < data.demons.Count - 1)
            {
                currentIndex++;
            }
            else
            {
                currentIndex = 0;
            }
        }

        private void Update()
        {

        }

        public void Start()
        {
            
        }

        public void SortByLoot()
        {

        }

        public void SortByTime()
        {

        }

        public void DisplayDemonPage()
        {
            demonPagedisplay.DisplayPuzzle(demonPagePuzzleScale);
            demonPagedisplay.HighlightShape(data.demons[currentIndex].segments);
            if (data.demons[currentIndex].isStarred)
            {
                starImage.sprite = starActive;
            }
            else
            {
                starImage.sprite = starInActive;
            }
        }

        public void DisplaySummary(Demon[] demons)
        {
            List<Demon> spawnDemons = new List<Demon>();
            int numberOfPages = (demons.Length / numberOfItemsPerPage);
            Debug.Log("Pages " + numberOfPages);
            for (int i = 0; i < demons.Length; i++)
            {
                spawnDemons.Add(demons[i]);
                if (i % numberOfItemsPerPage == 0)
                {
                    Debug.Log("New Page " + i);
                    GameObject instance = Instantiate(puzzleGroupPrefab, puzzleGroupParent.transform.position, Quaternion.identity, puzzleGroupParent.transform);
                    PuzzleDisplayGroup group = instance.GetComponent<PuzzleDisplayGroup>();
                    group.SpawnDisplay(spawnDemons.ToArray(), summaryPagePuzzleScale);
                    spawnDemons.Clear();
                }
            }
            if (spawnDemons.Count > 0)
            {
                GameObject instance = Instantiate(puzzleGroupPrefab, puzzleGroupParent.transform.position, Quaternion.identity, puzzleGroupParent.transform);
                PuzzleDisplayGroup group = instance.GetComponent<PuzzleDisplayGroup>();
                group.SpawnDisplay(spawnDemons.ToArray(), summaryPagePuzzleScale);
                spawnDemons.Clear();
            }
        }


        public void ToggleStar()
        {
            data.demons[currentIndex].ToggleStar();
        }
        [ContextMenu("Open")]
        public void Open()
        {
            if (GameManager.currentPanel == CurrentPanel.None)
            {
                GameManager.currentPanel = thisPanelName;
                panel.SetActive(true);
                DisplaySummary(data.demons.ToArray());
            }
        }

        [ContextMenu("Close")]
        public void Close()
        {
            if (GameManager.currentPanel == thisPanelName)
            {
                GameManager.currentPanel = CurrentPanel.None;
                panel.SetActive(false);
            }
        }
    }
}

