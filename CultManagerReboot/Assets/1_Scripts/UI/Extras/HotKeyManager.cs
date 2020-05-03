﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class HotKeyManager : MonoBehaviour
    {
        [SerializeField] private GameObject hotKeyPanel;
        [SerializeField] private GameObject openButton;
        [SerializeField] private CurrentPanel thisPanelName;

        [SerializeField] private BloodBankManager bloodManager;
        [SerializeField] private MoneyManager moneyManager;
        [SerializeField] private CultManager cultManager;


        private void Start()
        {
            hotKeyPanel.SetActive(false);
            openButton.SetActive(true);
        }

        public void Open()
        {
            if (GameManager.currentPanel == CurrentPanel.None)
            {
                GameManager.currentPanel = thisPanelName;
                hotKeyPanel.SetActive(true);
                openButton.SetActive(false);
            }
        }

        public void Close()
        {
            if (GameManager.currentPanel == thisPanelName)
            {
                GameManager.currentPanel = CurrentPanel.None;
                hotKeyPanel.SetActive(false);
                openButton.SetActive(true);
            }
        }

        public void AddMoney()
        {
            moneyManager.Increase(50);
        }

        public void AddBlood(int a)
        {
            BloodType blood = (BloodType)(a);
            if (bloodManager.CanIncrease(blood, 10))
            {
                bloodManager.IncreaseBloodOfType(blood, 10);
            }
                
        }

        public void AddCultist()
        {
            cultManager.IncreaseCandidates();
        }
    }
}

