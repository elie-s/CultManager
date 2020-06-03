using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CultManager
{
    public class StatueMenuValuesDisplayer : MonoBehaviour
    {
        [SerializeField] private StatuesData data = default;
        [SerializeField] private MoneyData moneyData = default;
        [Header("Components")]
        [SerializeField] private Image partDisplayMissing = default;
        [SerializeField] private Image partDisplayCompleted = default;
        [SerializeField] private TextMeshProUGUI percentage = default;
        [SerializeField] private TextMeshProUGUI partNameDisplay = default;
        [SerializeField] private TextMeshProUGUI timeLeftDisplay = default;
        [SerializeField] private TextMeshProUGUI moneyCostDisplay = default;
        [SerializeField] private UISwitch moneySwitch = default;
        [SerializeField] private TextMeshProUGUI relicCostDisplay = default;
        [SerializeField] private UISwitch relicSwitch = default;
        [SerializeField] private TextMeshProUGUI workersDisplay = default;
        [SerializeField] private ButtonInteraction buyButton = default;
        [Header("GameObjects")]
        [SerializeField] private GameObject toBuyUI = default;
        [SerializeField] private GameObject bought = default;
        [SerializeField] private GameObject completed = default;

        private StatuePart currentPart => data.currentStatueSet.currentPart;

        void Update()
        {
            UpdateTimeLeft();
            UpdateSprite();
            UpdatePercentage();
        }

        [ContextMenu("Update Display")]
        public void UpdateDisplay()
        {
            DisplayBottomPart(currentPart.bought, currentPart.completed);
            partNameDisplay.text = currentPart.partName;
            moneyCostDisplay.text = DisplayNumber(currentPart.cost.money);
            relicCostDisplay.text = DisplayNumber(currentPart.cost.relic);
            workersDisplay.text = currentPart.workers.value.ToString() + "/" + currentPart.workers.max.ToString();

            moneySwitch.Set(currentPart.cost.money <= moneyData.money);
            relicSwitch.Set(currentPart.cost.relic <= moneyData.relics);

            if (currentPart.cost.money <= moneyData.money && currentPart.cost.relic <= moneyData.relics) buyButton.EnableButton();
            else buyButton.DisableButton();

            partDisplayMissing.sprite = currentPart.uiUncompletedSprite;
            partDisplayCompleted.sprite = currentPart.uiCompletedSprite;
        }

        public void UpdateTimeLeft()
        {
            timeLeftDisplay.text = DisplayDuration(currentPart.completion.toBeFilled / currentPart.workersForce);
        }

        public void UpdateSprite()
        {
            partDisplayMissing.color = new Color(1.0f, 1.0f, 1.0f, 1.0f - currentPart.completion.ratio);
            partDisplayCompleted.color = new Color(1.0f, 1.0f, 1.0f,currentPart.completion.ratio);
        }

        public void UpdatePercentage()
        {
            percentage.text = Mathf.RoundToInt(currentPart.completion.percentage)+"%";
        }

        private string DisplayNumber(int _value)
        {
            return _value.ToString();
        }

        private string DisplayDuration(float _value)
        {
            if (currentPart.workers.value < 1) return "Waiting for workers...";

            int days = Mathf.FloorToInt(_value / (float)86400);
            int hours = Mathf.FloorToInt(_value / (float)3600);
            int minutes = Mathf.FloorToInt(_value / (float)60);
            int seconds = Mathf.FloorToInt(_value);

            if (seconds < 0) return "Waiting for workers...";

            seconds -= minutes * 60;
            minutes -= hours * 60;
            days -= hours * 24;

            if (days > 0) return days + (days == 1 ? " day, " : " days, ") + hours + "h";
            else if (hours > 0) return hours + "h " + minutes + "m";
            else if (minutes > 0) return minutes + "m " + seconds + "s";
            else return seconds + "s";
        }

        private void DisplayBottomPart(bool _bought, bool _completed)
        {
            toBuyUI.SetActive(!_bought);
            bought.SetActive(_bought && !_completed);
            completed.SetActive(_bought && _completed);
            percentage.gameObject.SetActive(_bought && !_completed);
        }

    }
}