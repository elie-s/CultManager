using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

#pragma warning disable CS0414
namespace CultManager
{
    public class DemonBookUI : MonoBehaviour
    {
        [Header("Demon Book Panel")]
        [SerializeField] private GameObject panel = default;
        [SerializeField] private CurrentPanel thisPanelName = default;
        [SerializeField] private int currentDemonIndex = 0;
        [SerializeField] private DemonData data = default;

        [Header("Demon Page Display")]
        [SerializeField] private GameObject demonPage = default;

        [SerializeField] private Image demonImage = default;
        [SerializeField] private Image starImage = default;
        [SerializeField] private PuzzleDisplay demonPagedisplay = default;
        [SerializeField] private  TMP_Text description = default;
        [SerializeField] private int demonPagePuzzleScale = default;


        [Header("Summary Page Display")]
        [SerializeField] private GameObject summaryPage = default;
        [SerializeField] private GameObject puzzleGroupParent = default;
        [SerializeField] private GameObject pageNumberParent = default;
        [SerializeField] private GameObject puzzleGroupPrefab = default;
        [SerializeField] private GameObject pageNumberPrefab = default;
        [SerializeField] private Image[] buttons = default;
        [SerializeField] private Image summaryStarImage = default;
        [SerializeField] private int numberOfItemsPerPage = default;
        [SerializeField] private int summaryPagePuzzleScale = default;


        [Header("Display Sprites")]
        [SerializeField] private Sprite starActive = default;
        [SerializeField] private Sprite starInActive = default;
        [SerializeField] private Sprite buttonActive= default;
        [SerializeField] private Sprite buttonInactive = default;
        [SerializeField] private Demon[] result = default;
        [SerializeField] private List<GameObject> pages = default;

        private bool favouritesActive;



        public void Left()
        {
            if (currentDemonIndex > 0)
            {
                currentDemonIndex--;
            }
            else
            {
                currentDemonIndex = result.Length - 1;
            }
            DisplayDemonPage();
        }

        public void Right()
        {
            if (currentDemonIndex < result.Length - 1)
            {
                currentDemonIndex++;
            }
            else
            {
                currentDemonIndex = 0;
            }
            DisplayDemonPage();
        }

        private void Update()
        {

        }

        public void Start()
        {
            OpenSummaryPage();
        }



        public void OpenDemonPage()
        {
            demonPage.SetActive(true);
            summaryPage.SetActive(false);
        }

        public void DisplayThisDemon(int index)
        {
            if (favouritesActive)
            {
                ReturnFavouriteIndex(index);
            }
            else
            {
                currentDemonIndex = index;
            }

            OpenDemonPage();
            DisplayDemonPage();
        }


        public void DisplayDemonPage()
        {
            description.text = data.demons[currentDemonIndex].description;
            demonPagedisplay.DisplayPuzzle(demonPagePuzzleScale);
            demonPagedisplay.HighlightShape(data.demons[currentDemonIndex].segments);
            if (data.demons[currentDemonIndex].isStarred)
            {
                starImage.sprite = starActive;
            }
            else
            {
                starImage.sprite = starInActive;
            }
        }

        public void OpenSummaryPage()
        {
            demonPage.SetActive(false);
            summaryPage.SetActive(true);
            DemonTimeSort();
        }

        public void PageActive(int pageNum)
        {
            for (int i = 0; i < pages.Count; i++)
            {
                if ((i + 1) == pageNum)
                {
                    pages[i].SetActive(true);
                }
                else
                {
                    pages[i].SetActive(false);
                }
            }
        }

        public void ToggleFavourites()
        {
            favouritesActive = !favouritesActive;
            if (favouritesActive)
            {
                DisplaySummary(GatherFavourites());
                summaryStarImage.sprite = starActive;
                ButtonFeedback(buttons.Length);
            }
            else
            {
                DemonTimeSort();
                summaryStarImage.sprite = starInActive;
            }

        }

        public void LootSort()
        {
            DisplaySummary(SortByLoot());
            ButtonFeedback(0);
            if (favouritesActive)
            {
                favouritesActive = false;
                summaryStarImage.sprite = starInActive;
            }
        }

        public void DemonTimeSort()
        {
            DisplaySummary(SortByTime());
            ButtonFeedback(1);
            if (favouritesActive)
            {
                favouritesActive = false;
                summaryStarImage.sprite = starInActive;
            }
        }

        public void DemonSegmentSort()
        {
            DisplaySummary(SortBySegments());
            ButtonFeedback(2);
            if (favouritesActive)
            {
                favouritesActive = false;
                summaryStarImage.sprite = starInActive;
            }
        }

        public void ButtonFeedback(int index)
        {
            if (index < buttons.Length)
            {
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (i == index)
                    {
                        buttons[i].sprite = buttonActive;
                    }
                    else
                    {
                        buttons[i].sprite = buttonInactive;
                    }
                }
            }
            else
            {
                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].sprite = buttonInactive;
                }
            }

        }

        public Demon[] GatherFavourites()
        {
            result = data.demons.ToArray();
            List<Demon> temp = new List<Demon>();
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i].isStarred)
                {
                    temp.Add(result[i]);
                }
            }
            return temp.ToArray();
        }

        public void ReturnFavouriteIndex(int index)
        {
            result = data.demons.ToArray();
            Demon[] temp = GatherFavourites();
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] == temp[index])
                {
                    currentDemonIndex = i;
                    break;
                }
            }
        }


        public Demon[] SortByLoot()
        {
            result = data.demons.ToArray();
            Demon temp;
            for (int i = 0; i < result.Length; i++)
            {
                for (int j = i + 1; j < result.Length; j++)
                {
                    if (result[i].lootBonus > result[j].lootBonus)
                    {
                        temp = result[i];
                        result[i] = result[j];
                        result[j] = temp;
                    }
                }
            }
            return result;
        }

        public Demon[] SortByTime()
        {
            result = data.demons.ToArray();
            Demon temp;
            for (int i = 0; i < result.Length; i++)
            {
                for (int j = i + 1; j < result.Length; j++)
                {
                    if (result[i].deathTime > result[j].deathTime)
                    {
                        temp = result[i];
                        result[i] = result[j];
                        result[j] = temp;
                    }
                }
            }
            return result;
        }

        public Demon[] SortBySegments()
        {
            result = data.demons.ToArray();
            Demon temp;
            for (int i = 0; i < result.Length; i++)
            {
                for (int j = i + 1; j < result.Length; j++)
                {
                    if (result[i].segments.Length > result[j].segments.Length)
                    {
                        temp = result[i];
                        result[i] = result[j];
                        result[j] = temp;
                    }
                }
            }
            return result;
        }

        public void DisplaySummary(Demon[] demons)
        {
            ClearChildren(puzzleGroupParent.transform);
            ClearChildren(pageNumberParent.transform);
            List<Demon> spawnDemons = new List<Demon>();
            pages.Clear();

            int numberOfPages = 0;

            for (int i = demons.Length - 1; i >= 0; i--)
            {
                spawnDemons.Add(demons[i]);
                if (i % numberOfItemsPerPage == 0)
                {
                    numberOfPages++;
                    GameObject instance = Instantiate(puzzleGroupPrefab, puzzleGroupParent.transform.position, Quaternion.identity, puzzleGroupParent.transform);
                    instance.transform.SetAsFirstSibling();
                    PuzzleDisplayGroup group = instance.GetComponent<PuzzleDisplayGroup>();
                    group.SpawnDisplay(spawnDemons.ToArray(), summaryPagePuzzleScale);
                    pages.Insert(0, instance);
                    spawnDemons.Clear();

                    GameObject pageButton = Instantiate(pageNumberPrefab, pageNumberParent.transform.position, Quaternion.identity, pageNumberParent.transform);
                    pageButton.GetComponent<PageBehavior>().InitText(numberOfPages);

                }
            }
            if (spawnDemons.Count > 0)
            {
                GameObject instance = Instantiate(puzzleGroupPrefab, puzzleGroupParent.transform.position, Quaternion.identity, puzzleGroupParent.transform);
                instance.transform.SetAsFirstSibling();
                PuzzleDisplayGroup group = instance.GetComponent<PuzzleDisplayGroup>();
                group.SpawnDisplay(spawnDemons.ToArray(), summaryPagePuzzleScale);
                pages.Insert(0, instance);
                spawnDemons.Clear();

                GameObject pageButton = Instantiate(pageNumberPrefab, pageNumberParent.transform.position, Quaternion.identity, pageNumberParent.transform);
                pageButton.GetComponent<PageBehavior>().InitText(++numberOfPages);
            }
            PageActive(1);
        }


        public void ClearChildren(Transform parent)
        {
            if (parent.childCount > 0)
            {
                for (int i = 0; i < parent.childCount; i++)
                {
                    Destroy(parent.GetChild(i).gameObject);
                }
            }
        }

        public void ToggleStar()
        {
            data.demons[currentDemonIndex].ToggleStar();
            DisplayDemonPage();
        }
        [ContextMenu("Open")]
        public void Open()
        {
            if (GameManager.currentPanel == CurrentPanel.None)
            {
                GameManager.currentPanel = thisPanelName;
                panel.SetActive(true);
                DemonTimeSort();
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

