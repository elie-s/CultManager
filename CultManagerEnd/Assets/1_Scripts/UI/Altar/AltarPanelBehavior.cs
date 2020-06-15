﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


namespace CultManager
{
    public class AltarPanelBehavior : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private AltarData altarData = default;
        [SerializeField] private AltarManager altarManager = default;
        [SerializeField] private CurrentPanel thisPanelName = default;

        [Header("Display")]
        [SerializeField] private GameObject Panel = default;
        [SerializeField] private GameObject demonPanel = default;
        [SerializeField] private GameObject buyButton = default;
        [SerializeField] private GameObject costPanel = default;
        [SerializeField] private GameObject progressPanel = default;
        [SerializeField] private GameObject completedPanel = default;
        [SerializeField] private Image altarPartImage = default;
        [SerializeField] private Image altarPartBar = default;
        [SerializeField] private TMP_Text costText = default;
        [SerializeField] private TMP_Text nameText = default;
        [SerializeField] private TMP_Text descriptionText = default;
        [SerializeField] private TMP_Text cultistsText = default;
        [SerializeField] private TMP_Text progressText = default;
        [SerializeField] private TMP_Text workPowerText = default;
        [SerializeField] private TMP_Text elapsedTimeText = default;
        [SerializeField] private TMP_Text nameCompletedText = default;
        [SerializeField] private TMP_Text descriptionCompletedText = default;

        private AltarPart altarPart=> altarData.altarParts[currentId];
        private AltarPartData currentAltarPartData => altarManager.ReturnAltarPartData(altarPart);
        private int currentId;

        private void Start()
        {
            currentId = 0;
            if (GameManager.currentPanel == thisPanelName)
                Display();
        }


        private void Update()
        {

            if (GameManager.currentPanel == thisPanelName)
                Display();
        }

        public void Reset()
        {
            
        }

        public void Display()
        {
            demonPanel.SetActive(altarData.altarCompletion);
            if (altarPart.isBought)
            {
                costPanel.SetActive(false);
                buyButton.SetActive(false);
                if (altarPart.currentBuildPoints.ratio == 1)
                {
                    completedPanel.SetActive(true);
                    progressPanel.SetActive(false);
                    altarPartImage.color = new Color(1, 1, 1, 1f);
                    nameCompletedText.text = currentAltarPartData.altarPartName;
                    descriptionCompletedText.text = currentAltarPartData.description;
                }
                else
                {
                    progressPanel.SetActive(true);
                    completedPanel.SetActive(false);
                    altarPartImage.color = new Color(1, 1, 1, 0.25f);
                    /*altarPartBar.fillAmount = Mathf.Lerp(altarPartBar.fillAmount, altarPart.currentBuildPoints.ratio,Time.deltaTime) ;*/
                    cultistsText.text = altarPart.assignedCultists.value.ToString() + "/" + altarPart.assignedCultists.max.ToString();
                    progressText.text = altarPart.currentBuildPoints.value.ToString() + "/" + altarPart.currentBuildPoints.max.ToString();
                    workPowerText.text = altarManager.workPower[currentId].ToString() + "/s";
                    if (altarManager.workPower[currentId] > 0)
                    {
                        elapsedTimeText.text = ((float)(altarPart.currentBuildPoints.amountLeft / altarManager.workPower[currentId])).ToString() + "s";
                    }
                    else
                    {
                        elapsedTimeText.text = "0s";
                    }
                }
                
                
                    
            }
            else
            {
                costPanel.SetActive(true);
                completedPanel.SetActive(false);
                progressPanel.SetActive(false);
                buyButton.SetActive(true);
                nameText.text = currentAltarPartData.altarPartName;
                descriptionText.text = currentAltarPartData.description;
                altarPartImage.color = new Color(0, 0, 0, 1f);
                altarPartBar.fillAmount = 0f;
            }
        }


        public void SetCurrentAltarPart()
        {
            altarPartImage.sprite = currentAltarPartData.altarSprite;
            altarPartBar.sprite = currentAltarPartData.altarSprite;
            costText.text = currentAltarPartData.cost.ToString();
            cultistsText.text = altarPart.assignedCultists.value.ToString();
        }

        public void BuyButton()
        {
            altarManager.Buy(altarPart);
        }

        public void CloseButton()
        {
            if (GameManager.currentPanel == thisPanelName)
            {
                GameManager.currentPanel = CurrentPanel.None;
                Panel.SetActive(false);
            }
        }

        public void OpenButton()
        {
            if (GameManager.currentPanel == CurrentPanel.None)
            {
                GameManager.currentPanel = thisPanelName;
                demonPanel.SetActive(altarData.altarCompletion);
                Panel.SetActive(true);
                SetCurrentAltarPart();
            }
        }

        public void Next()
        {
            if (currentId < (altarData.altarParts.Count - 1))
            {
                currentId += 1;
            }
            else
            {
                currentId = 0;
            }
            SetCurrentAltarPart();
        }

        public void Previous()
        {
            if (currentId > 0)
            {
                currentId -= 1;
            }
            else
            {
                currentId = altarData.altarParts.Count-1;
            }
            SetCurrentAltarPart();
        }

        public void IncreaseCultists()
        {
            altarManager.AddCultistsToAltar(altarPart,1);
        }

        public void DecreaseCultists()
        {
            altarManager.RemoveCultistsFromAltar(altarPart,1);
        }

        public void DemonSummon()
        {
            CloseButton();
            altarManager.DemonSummon();
        }

    }
}
