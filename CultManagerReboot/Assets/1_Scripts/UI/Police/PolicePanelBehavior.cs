using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


namespace CultManager
{
    public class PolicePanelBehavior : MonoBehaviour
    {
        [Header("System Reference")]
        [SerializeField] private PoliceManager police;
        [SerializeField] private MoneyManager money;
        [SerializeField] private PoliceData data;
        [SerializeField] private CurrentPanel thisPanelName;

        [Header("System Parameters")]
        public int moneyDisplay;
        public int moneyPerBribe;
        [SerializeField] private int moneyIncrementValue;
        [SerializeField] private IntGauge bribeChange;
        private bool isPressed;

        [Header("Display Reference")]
        [SerializeField] private GameObject panel;
        [SerializeField] private Image policeBar;
        [SerializeField] private Image inflitrationImage;
        [SerializeField] private Image moneyLossImage;
        [SerializeField] private Image influenceLossImage;
        [SerializeField] private Image cultistArrestImage;
        [SerializeField] private Image giveButtonImage;

        [SerializeField] private Transform rotationArrow;
        [SerializeField] private TMP_Text moneyText;

        [Header("Sprite Reference")]
        [SerializeField] private PoliceUISettings settings;

        private void Start()
        {
            bribeChange = new IntGauge(0, data.max, false);
            isPressed = false;
            giveButtonImage.sprite = settings.giveButtonDisabled;
        }

        [ContextMenu("Open Police UI")]
        public void Open()
        {
            if (GameManager.currentPanel == CurrentPanel.None)
            {
                GameManager.currentPanel = thisPanelName;
                ResetValues();
                panel.SetActive(true);
            }
        }

        public void Close()
        {
            if (GameManager.currentPanel == thisPanelName)
            {
                GameManager.currentPanel = CurrentPanel.None;
                ResetValues();
                panel.SetActive(false);
            }
        }

        void IncreaseBribe(int value)
        {
            value = value / moneyPerBribe;
            bribeChange.Increment(value);
            Debug.Log(value);
        }

        void DecreaseBribe(int value)
        {
            value = value / moneyPerBribe;
            bribeChange.Decrement(value);
            Debug.Log(value);
        }

        public void IncreaseMoney()
        {
            if (money.value >= (moneyDisplay + moneyIncrementValue))
            {
                moneyDisplay += moneyIncrementValue;
                IncreaseBribe(moneyIncrementValue);
            }
        }

        public void DecreaseMoney()
        {
            if (moneyDisplay >= (moneyIncrementValue))
            {
                moneyDisplay -= moneyIncrementValue;
                DecreaseBribe(moneyIncrementValue);
            }    
        }

        public void GiveButton()
        {
            if (!isPressed)
            {
                police.DecreasePoliceValue(bribeChange.value);
                ResetValues();
                giveButtonImage.sprite = settings.giveButtonEnabled;
                Invoke("ButtonPressed", 0.5f);
            }
            
        }

        void ButtonPressed()
        {
            giveButtonImage.sprite = settings.giveButtonDisabled;
            isPressed = false;
        }

        public void ResetValues()
        {
            bribeChange = new IntGauge(0, data.max, false);
            moneyDisplay = 0;
        }

        public void Update()
        {
            Display();
        }

        public void Display()
        {
            policeBar.fillAmount = (data.ratio - bribeChange.ratio);
            rotationArrow.rotation = Quaternion.Euler(0, 0, 90 - ((data.ratio-bribeChange.ratio) * 180f));
            moneyText.text = moneyDisplay.ToString();
            UpdateInvestigationDisplay();
        }

        void UpdateInvestigationDisplay()
        {
            switch (police.investigationLevel)
            {
                case 0:
                    {
                        inflitrationImage.sprite = settings.inflitrationDisabled;
                        moneyLossImage.sprite = settings.moneyLossDisabled;
                        influenceLossImage.sprite = settings.influenceDisabled;
                        cultistArrestImage.sprite = settings.cultistArrestDisabled; ;
                    }
                    break;
                case 1:
                    {
                        inflitrationImage.sprite = settings.inflitrationEnabled;
                        moneyLossImage.sprite = settings.moneyLossDisabled;
                        influenceLossImage.sprite = settings.influenceDisabled;
                        cultistArrestImage.sprite = settings.cultistArrestDisabled;
                    }
                    break;
                case 2:
                    {
                        inflitrationImage.sprite = settings.inflitrationEnabled;
                        moneyLossImage.sprite = settings.moneyLossEnabled;
                        influenceLossImage.sprite = settings.influenceDisabled;
                        cultistArrestImage.sprite = settings.cultistArrestDisabled;
                    }
                    break;
                case 3:
                    {
                        inflitrationImage.sprite = settings.inflitrationEnabled;
                        moneyLossImage.sprite = settings.moneyLossEnabled;
                        influenceLossImage.sprite = settings.influenceEnabled;
                        cultistArrestImage.sprite = settings.cultistArrestDisabled;
                    }
                    break;
                case 4:
                    {
                        inflitrationImage.sprite = settings.inflitrationEnabled;
                        moneyLossImage.sprite = settings.moneyLossEnabled;
                        influenceLossImage.sprite = settings.influenceEnabled;
                        cultistArrestImage.sprite = settings.cultistArrestEnabled;
                    }
                    break;
            }
        }

    }
}

