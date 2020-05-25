using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class HelperUI : MonoBehaviour
    {
        [SerializeField] private GameObject helperPanel;
        [SerializeField] private GameObject[] indicatorPanels;
        public HelperPanel helper;

        [SerializeField] private float growValue;
        [Range(0, 1), SerializeField] private float growSpeed;
        [SerializeField] private bool toGrow;
        [SerializeField] private bool isOpen;

        public static HelperPanel currentHelperPanel;

        private void Update()
        {
            SetHelperPanel();
            helper = currentHelperPanel;
            if (toGrow)
            {
                LerpGrowth(0, 1);
            }
        }

        public void EnableHelper()
        {
            DisplayIndicators((int)(currentHelperPanel - 1));
        }

        public void DisableHelper()
        {
            //helperPanel.SetActive(false);
            
            isOpen = false;
            toGrow = true;
        }

        public void DisplayIndicators(int index)
        {
            for (int i = 0; i < indicatorPanels.Length; i++)
            {
                if (i == index)
                {
                    if (!toGrow)
                    {
                        helperPanel.SetActive(true);
                        isOpen = true;
                        toGrow = true;
                        growValue = 0;
                    }
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
                growValue += growSpeed * Time.deltaTime;
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
                                }
                                break;
                            case CurrentIsland.All:
                                break;
                            case CurrentIsland.Origin:
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
                                }
                                break;
                            case CurrentIsland.SummonArea:
                                break;
                        }
                    }
                    break;
                case CurrentPanel.NoteTabPanel:
                    break;
                case CurrentPanel.RecruitmentPanel:
                    break;
                case CurrentPanel.AltarPanel:
                    break;
                case CurrentPanel.PuzzlePanel:
                    break;
                case CurrentPanel.DemonBook:
                    {
                        currentHelperPanel = HelperPanel.DemonSummary;
                    }
                    break;
                case CurrentPanel.HotKeys:
                    break;
                case CurrentPanel.PolicePanel:
                    break;
                case CurrentPanel.DemonPage:
                    {
                        currentHelperPanel = HelperPanel.DemonPage;
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

