using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class HelperUI : MonoBehaviour
    {
        [SerializeField] private GameObject helperButton = default;
        [SerializeField] private GameObject helperPanel = default;
        [SerializeField] private GameObject[] indicatorPanels = default;
        public HelperPanel helper;

        [SerializeField] private float growValue = default;
        [Range(0, 1), SerializeField] private float growSpeed = default;
        [SerializeField] private bool toGrow = default;
        [SerializeField] private bool isOpen = default;

        public static HelperPanel currentHelperPanel;

        private void Update()
        {
            SetHelperPanel();
            DisplayIndicators((int)(currentHelperPanel - 1));
            helper = currentHelperPanel;
            if (toGrow)
            {
                LerpGrowth(0, 1);
            }
        }

        public void ToggleHelper()
        {
            if (!isOpen)
            {
                if (!toGrow)
                {
                    helperPanel.SetActive(true);
                    isOpen = true;
                    toGrow = true;
                }
            }
            else
            {
                isOpen = false;
                toGrow = true;
            }
        }

        public void DisplayIndicators(int index)
        {
            for (int i = 0; i < indicatorPanels.Length; i++)
            {
                if (i == index && helperPanel.activeSelf)
                {
                    indicatorPanels[i].SetActive(true);
                }
                else
                {
                    indicatorPanels[i].SetActive(false);
                }
            }
        }

        public void DisableAllIndicators()
        {
            for (int i = 0; i < indicatorPanels.Length; i++)
            {
                indicatorPanels[i].SetActive(false);
            }
        }

        public void LerpGrowth(float initial, float target)
        {
            if (growValue < target)
            {
                growValue += growSpeed;
                float currentScale = Mathf.Lerp(initial, target, growValue);
                if (!isOpen)
                {
                    helperPanel.transform.localScale = new Vector3(1 - currentScale, 1 - currentScale, 1);
                }
                else
                {
                    helperPanel.transform.localScale = new Vector3(currentScale, currentScale, 1);
                }
                
            }
            else
            {
                toGrow = false;
                growValue = 0;
                if (!isOpen)
                {
                    DisableAllIndicators();
                }
            }
        }

        public void SetHelperPanel()
        {
            

            switch (GameManager.currentPanel)
            {
                case CurrentPanel.None:
                    {
                        switch (GameManager.currentIsland)
                        {
                            case CurrentIsland.Transition:
                                {
                                    currentHelperPanel = HelperPanel.None;
                                    helperButton.SetActive(false);
                                }
                                break;
                            case CurrentIsland.All:
                                break;
                            case CurrentIsland.Origin:
                                {
                                    helperButton.SetActive(false);
                                }
                                break;
                            case CurrentIsland.RecruitmentIsland:
                                break;
                            case CurrentIsland.SacrificeIsland:
                                break;
                            case CurrentIsland.AltarIsland:
                                break;
                            case CurrentIsland.PuzzleIsland:
                                {
                                    currentHelperPanel = HelperPanel.ExperimentIsland;
                                    helperButton.SetActive(true);
                                }
                                break;
                            case CurrentIsland.SummonArea:
                                break;
                        }
                    }
                    break;
                case CurrentPanel.NoteTabPanel:
                    {
                        helperButton.SetActive(false);
                    }
                    break;
                case CurrentPanel.RecruitmentPanel:
                    {
                        helperButton.SetActive(false);
                    }
                    break;
                case CurrentPanel.AltarPanel:
                    {
                        helperButton.SetActive(false);
                    }
                    break;
                case CurrentPanel.PuzzlePanel:
                    {
                        currentHelperPanel = HelperPanel.ExperimentArea;
                        helperButton.SetActive(true);
                    }
                    break;
                case CurrentPanel.DemonBook:
                    {
                        currentHelperPanel = HelperPanel.DemonSummary;
                        helperButton.SetActive(true);
                    }
                    break;
                case CurrentPanel.HotKeys:
                    {
                        helperButton.SetActive(true);
                    }
                    break;
                case CurrentPanel.PolicePanel:
                    {
                        helperButton.SetActive(true);
                    }
                    break;
                case CurrentPanel.DemonPage:
                    {
                        currentHelperPanel = HelperPanel.DemonPage;
                        helperButton.SetActive(true);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    

    public enum HelperPanel
    {
        None=0,
        ExperimentIsland=1,
        DemonPage=2,
        DemonSummary=3,
        ExperimentArea=4,
    }
}

