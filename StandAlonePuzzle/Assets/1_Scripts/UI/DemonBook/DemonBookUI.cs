using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;


namespace CultManager
{
    public class DemonBookUI : MonoBehaviour
    {
        [Header("Demon Book Panel")]
        [SerializeField] private GameObject panel;
        [SerializeField] private CurrentPanel thisPanelName;
        [SerializeField] private int currentDemonIndex = 0;
        [SerializeField] private DemonData data;
        [SerializeField] private DemonsSet demonsSprites = default;

        [Header("Demon Page Display")]
        [SerializeField] private GameObject demonPage;
        [SerializeField] private GameObject confirmPage;
        [SerializeField] private SpawnColor spawnColor;
        [SerializeField] private Image demonImage;
        [SerializeField] private Image starImage;
        [SerializeField] private PuzzleDisplay demonPagedisplay;
        [SerializeField] private  TMP_Text exptNumberText;
        [SerializeField] private  TMP_Text goodLinksText;
        [SerializeField] private  TMP_Text spawnLinksText;
        [SerializeField] private  TMP_Text totalGoodLinksText;
        [SerializeField] private int demonPagePuzzleScale;


        [Header("Summary Page Display")]
        [SerializeField] private GameObject summaryPage;
        [SerializeField] private GameObject puzzleGroupParent;
        [SerializeField] private GameObject pageNumberParent;
        [SerializeField] private GameObject puzzleGroupPrefab;
        [SerializeField] private GameObject pageNumberPrefab;
        [SerializeField] private Image[] buttons;
        [SerializeField] private Image summaryStarImage;
        [SerializeField] private int numberOfItemsPerPage;
        [SerializeField] private int summaryPagePuzzleScale;
        private int currentPageNumber;


        [Header("Display Sprites")]
        [SerializeField] private Sprite starActive;
        [SerializeField] private Sprite starInActive;
        [SerializeField] private Sprite buttonActive;
        [SerializeField] private Sprite buttonInactive;

        [SerializeField] private Demon[] result;
        [SerializeField] private List<GameObject> pages;
        [SerializeField] private List<PageBehavior> pageBehaviors;

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
            GameManager.currentPanel = CurrentPanel.DemonPage;
            OpenDemonPage();
            DisplayDemonPage();
            
        }

        public void DisplayLastDemon()
        {
            Open();
            DisplayThisDemon(result.Length - 1);
        }


        public void DisplayDemonPage()
        {
            exptNumberText.text = "Experiment #" + (result[currentDemonIndex].id+1).ToString();
            goodLinksText.text = result[currentDemonIndex].patternSegments.ToString();
            spawnLinksText.text = result[currentDemonIndex].segments.Length.ToString();
            totalGoodLinksText.text = result[currentDemonIndex].totalPatternSegments.ToString();
            demonPagedisplay.DisplayPuzzle(demonPagePuzzleScale);
            demonPagedisplay.HighlightShape(result[currentDemonIndex].segments);
            demonImage.sprite = demonsSprites.GetSpawnID(result[currentDemonIndex].demon, result[currentDemonIndex].accuracy);
            if (result[currentDemonIndex].isStarred)
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
            currentPageNumber = pageNum;
            for (int i = 0; i < pages.Count; i++)
            {
                if ((i + 1) == pageNum)
                {
                    pages[i].SetActive(true);
                    pageBehaviors[i].HighlightText();
                }
                else
                {
                    pages[i].SetActive(false);
                    pageBehaviors[i].UnHighlightText();
                }
            }
        }

        public void LeftSwipe()
        {
            Debug.Log("Left Swipe");
            if (demonPage.activeSelf)
            {
                Right();
            }
            else if (summaryPage.activeSelf)
            {
                if (currentPageNumber < pages.Count-1)
                {
                    currentPageNumber++;
                }
                else
                {
                    currentPageNumber = 0;
                }
                PageActive(currentPageNumber);
            }
        }

        public void RightSwipe()
        {
            Debug.Log("Right Swipe");
            if (demonPage.activeSelf)
            {
                Left();
            }
            else if (summaryPage.activeSelf)
            {
                if (currentPageNumber > 0)
                {
                    currentPageNumber--;
                }
                else
                {
                    currentPageNumber = pages.Count - 1;
                }
                PageActive(currentPageNumber);
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
                        if(Mathf.Abs(buttons[i].transform.localScale.x)!=1.25f)
                        buttons[i].transform.localScale = new Vector3(buttons[i].transform.localScale.x*1.25f, 1.25f,1.25f);
                    }
                    else
                    {
                        buttons[i].sprite = buttonInactive;
                        buttons[i].transform.localScale = new Vector3(1, 1, 1);
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
                    if (result[i].patternSegments > result[j].patternSegments)
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
            if(summaryPage.activeSelf && panel.activeSelf) GameManager.currentPanel = thisPanelName;
            ClearChildren(puzzleGroupParent.transform);
            ClearChildren(pageNumberParent.transform);
            pageBehaviors = new List<PageBehavior>();
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
                    group.SpawnDisplay(spawnDemons.ToArray(), summaryPagePuzzleScale, i);
                    pages.Insert(0, instance);
                    spawnDemons.Clear();

                    GameObject pageButton = Instantiate(pageNumberPrefab, pageNumberParent.transform.position, Quaternion.identity, pageNumberParent.transform);
                    pageButton.GetComponent<PageBehavior>().InitText(numberOfPages);
                    pageBehaviors.Add(pageButton.GetComponent<PageBehavior>());
                    pageButton.GetComponent<PageBehavior>().UnHighlightText();

                }
            }
            if (spawnDemons.Count > 0)
            {
                GameObject instance = Instantiate(puzzleGroupPrefab, puzzleGroupParent.transform.position, Quaternion.identity, puzzleGroupParent.transform);
                instance.transform.SetAsFirstSibling();
                PuzzleDisplayGroup group = instance.GetComponent<PuzzleDisplayGroup>();
                group.SpawnDisplay(spawnDemons.ToArray(), summaryPagePuzzleScale, numberOfItemsPerPage);
                pages.Insert(0, instance);
                spawnDemons.Clear();

                GameObject pageButton = Instantiate(pageNumberPrefab, pageNumberParent.transform.position, Quaternion.identity, pageNumberParent.transform);
                pageButton.GetComponent<PageBehavior>().InitText(++numberOfPages);
                pageBehaviors.Add(pageButton.GetComponent<PageBehavior>());
                pageButton.GetComponent<PageBehavior>().UnHighlightText();
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

        public void OpenDeletePanel()
        {
            confirmPage.SetActive(true);
        }

        public void CloseDeletePanel()
        {
            confirmPage.SetActive(false); 
        }

        public void RemoveThisDemon()
        {
            data.RemoveDemon(result[currentDemonIndex]);
            CloseDeletePanel();
            OpenSummaryPage();
        }


        [ContextMenu("Open")]
        public void Open()
        {
            if (GameManager.currentPanel == CurrentPanel.None)
            {
                GameManager.currentPanel = thisPanelName;
                panel.SetActive(true);
                DemonTimeSort();
                CloseDeletePanel();
            }
        }

        [ContextMenu("Close")]
        public void Close()
        {
            if (GameManager.currentPanel == thisPanelName || GameManager.currentPanel == CurrentPanel.DemonPage)
            {
                GameManager.currentPanel = CurrentPanel.None;
                panel.SetActive(false);
            }
        }
    }
}

