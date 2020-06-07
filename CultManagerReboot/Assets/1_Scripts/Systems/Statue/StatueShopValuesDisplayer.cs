using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CultManager
{
    public class StatueShopValuesDisplayer : MonoBehaviour
    {
        [SerializeField] private StatuesData data = default;
        [SerializeField] private MoneyData moneyData = default;
        [Header("ToBuy")]
        [SerializeField] private GameObject contentTB = default;
        [SerializeField] private TextMeshProUGUI demonNameTB = default;
        [SerializeField] private TextMeshProUGUI partsTB = default;
        [SerializeField] private TextMeshProUGUI puzzleSegmentsTB = default;
        [SerializeField] private TextMeshProUGUI demonSegmentsTB = default;
        [SerializeField] private TextMeshProUGUI effectTB = default;
        [SerializeField] private TextMeshProUGUI costTB = default;
        [SerializeField] private Image statueTB = default;
        [SerializeField] private ButtonInteraction buyButton = default;

        [Header("Bought")]
        [SerializeField] private GameObject contentB = default;
        [SerializeField] private TextMeshProUGUI demonNameB = default;
        [SerializeField] private TextMeshProUGUI puzzleSegmentsB = default;
        [SerializeField] private TextMeshProUGUI demonSegmentsB = default;
        [SerializeField] private TextMeshProUGUI progressTextB = default;
        [SerializeField] private TextMeshProUGUI effectB = default;
        [SerializeField] private Image progressBarB = default;
        [SerializeField] private Image statueB = default;

        [Header("Completed")]
        [SerializeField] private GameObject contentC = default;
        [SerializeField] private TextMeshProUGUI demonNameC = default;
        [SerializeField] private TextMeshProUGUI effectC = default;
        [SerializeField] private Image demonThumbnailC = default;
        [SerializeField] private Image statueC = default;

        private int index = 0;
        private StatueSet currentSet => data.GetStatueSet(index);

        private void OnEnable()
        {
            UpdateDisplay();
        }

        public void Update()
        {
            UpdateProgress();
        }

        [ContextMenu("Update Display")]
        public void UpdateDisplay()
        {
            if (!currentSet.bought) UpdateToBuy();
            else if (!currentSet.completed) UpdateBought();
            else UpdateCompleted();
        }

        public void UpdateToBuy()
        {
            ActivatePanel();

            demonNameTB.text = currentSet.demon.ToString();
            partsTB.text = currentSet.parts.Length+" parts";
            puzzleSegmentsTB.text = currentSet.puzzleSegments.ToString();
            demonSegmentsTB.text = currentSet.demonSegments.ToString();
            effectTB.text = currentSet.effectText;
            costTB.text = FormattingCost(currentSet.cost);
            statueTB.sprite = currentSet.statue[0];

            if (currentSet.cost <= moneyData.money) buyButton.EnableButton();
            else buyButton.DisableButton();
        }

        public void UpdateBought()
        {
            ActivatePanel();

            demonNameB.text = currentSet.demon.ToString();
            puzzleSegmentsB.text = currentSet.puzzleSegments.ToString();
            demonSegmentsB.text = currentSet.demonSegments.ToString();
            effectB.text = currentSet.effectText;
            statueB.sprite = currentSet.statue[1];
        }

        public void UpdateCompleted()
        {
            ActivatePanel();

            demonNameC.text = currentSet.demon.ToString();
            effectC.text = currentSet.effectText;
            statueC.sprite = currentSet.statue[2];
            demonThumbnailC.sprite = currentSet.statue[3];
        }

        private void UpdateProgress()
        {
            (float percentage, float ratio) progress = GetProgress();

            progressBarB.fillAmount = progress.ratio;
            progressTextB.text = progress.percentage + "%";
        }

        private void ActivatePanel()
        {
            contentTB?.SetActive(!currentSet.bought);
            contentB?.SetActive(currentSet.bought && !currentSet.completed);
            contentC?.SetActive(currentSet.bought && currentSet.completed);
        }

        private (float percentage, float ratio) GetProgress()
        {
            if (currentSet.parts.Length == 0) return (0.0f, 0.0f);

            float result = 0.0f;

            foreach (StatuePart part in currentSet.parts)
            {
                result += part.completion.ratio;
            }

            result /= currentSet.parts.Length;

            return ((float)System.Math.Round(result * 100.0f, 0), result);
        }

        private string FormattingCost(int _value)
        {
            return _value.ToString();
        }
        
        public void SetIndex(int _value)
        {
            index = _value;
        }
    }
}