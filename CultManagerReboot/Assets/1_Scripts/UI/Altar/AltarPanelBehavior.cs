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

        [Header("Display")]
        [SerializeField] private GameObject Panel;
        [SerializeField] private GameObject buyButton;
        [SerializeField] private Image altarPartImage;
        [SerializeField] private Image altarPartBar;
        [SerializeField] private Image cultistsBar;
        [SerializeField] private TMP_Text costText;

        private AltarPart altarPart;
        private int currentId;

        private void Start()
        {
            currentId = 0;
            Display(currentId);
        }

        private void Update()
        {

            
        }

        public void Display(int id)
        {
            AltarPartData altarPartData = altarPartDatas[id];
            altarPartImage.sprite = altarPartData.altarSprite;
            altarPartBar.sprite = altarPartData.altarSprite;
            costText.text = altarPartData.cost.ToString();
            altarPart = altarPartData.Init(altarData);
            if (altarPart.isBought)
            {
                buyButton.SetActive(false);
                altarPartImage.color = new Color(1, 1, 1, 0.25f);
                altarPartBar.fillAmount = altarPart.currentBuildPoints.ratio;
                cultistsBar.fillAmount = (float)(altarPart.assignedCultists/altarPartData.maxCultists);
            }
            else
            {
                buyButton.SetActive(true);
                altarPartImage.color = new Color(0, 0, 0, 1f);
            }
        }

        public void BuyButton()
        {
            altarManager.altarPartBehaviors[currentId].BuyButton();
        }

        public void CloseButton()
        {
            Panel.SetActive(true);
        }

        public void OpenButton()
        {
            Panel.SetActive(false);
        }

        public void Next()
        {
            if (currentId < altarPartDatas.Length - 1)
            {
                currentId += 1;
            }
            else
            {
                currentId = 0;
            }
            Display(currentId);
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
            Display(currentId);
        }

    }
}

