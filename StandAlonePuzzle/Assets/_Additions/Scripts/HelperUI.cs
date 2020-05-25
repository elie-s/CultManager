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

        public static HelperPanel currentHelperPanel;

        private void Update()
        {
            currentHelperPanel = helper;
        }

        public void EnableHelper()
        {
            DisplayIndicators((int)(currentHelperPanel - 1));
        }

        public void DisableHelper()
        {
            helperPanel.SetActive(false);
            DisableAllIndicators();
        }

        public void DisplayIndicators(int index)
        {
            for (int i = 0; i < indicatorPanels.Length; i++)
            {
                if (i == index)
                {
                    helperPanel.SetActive(true);
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

