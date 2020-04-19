using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


namespace CultManager
{
    public class AltarPanelBehavior : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private AltarManager altarManager;
        [SerializeField] private AltarData altarData;
        [SerializeField] private AltarPartData[] altarPartDatas;
        [SerializeField] private CurrentPanel thisPanelName;

        [Header("Display")]
        [SerializeField] private GameObject Panel;
        [SerializeField] private GameObject buyButton;
        [SerializeField] private Image altarPartImage;
        [SerializeField] private Image altarPartBar;
        [SerializeField] private Image cultistsBar;
        [SerializeField] private TMP_Text costText;

        private AltarPart altarPart;
        private int currentId;
        private AltarPartData currentAltarPartData => altarPartDatas[currentId];

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

        public void Display()
        {
            if (altarPart.isBought)
            {
                buyButton.SetActive(false);
                altarPartImage.color = new Color(1, 1, 1, 0.25f);
                altarPartBar.fillAmount = Mathf.Lerp(altarPartBar.fillAmount, altarPart.currentBuildPoints.ratio,Time.deltaTime) ;
                cultistsBar.fillAmount = Mathf.Lerp(cultistsBar.fillAmount,(float)(altarPart.assignedCultists/ currentAltarPartData.maxCultists),Time.deltaTime);
            }
            else
            {
                buyButton.SetActive(true);
                altarPartImage.color = new Color(0, 0, 0, 1f);
                altarPartBar.fillAmount = 0f;
            }
        }


        public void SetCurrentAltarPart()
        {
            altarPartImage.sprite = currentAltarPartData.altarSprite;
            altarPartBar.sprite = currentAltarPartData.altarSprite;
            costText.text = currentAltarPartData.cost.ToString();
            altarPart = currentAltarPartData.Init(altarData);

            altarPartBar.fillAmount = altarPart.currentBuildPoints.ratio;
            cultistsBar.fillAmount = (float)(altarPart.assignedCultists / currentAltarPartData.maxCultists);
        }

        public void BuyButton()
        {
            altarManager.altarPartBehaviors[currentId].BuyButton();
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
                Panel.SetActive(true);
                SetCurrentAltarPart();
            }
            
        }

        public void Next()
        {
            if (currentId < (altarPartDatas.Length - 1))
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
                currentId =altarPartDatas.Length;
            }
            SetCurrentAltarPart();
        }

    }
}

