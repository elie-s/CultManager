using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CultManager
{
    public class InvestigationUIDisplayer : MonoBehaviour
    {
        [SerializeField] private MoneyData moneyData = default;
        [SerializeField] private PoliceData policeData = default;
        [SerializeField] private ButtonInteraction bribeButton = default;
        [SerializeField] private RadialGaugeSmoothener mainGauge = default;
        [SerializeField] private RadialGaugeSmoothener previsualization = default;
        [SerializeField] private TextMeshProUGUI moneyValue = default;

        public void Display(int _money, float _gaugeValue)
        {
            moneyValue.text = _money.Format();
            mainGauge.SetValue(policeData.ratio);
            Debug.Log("gauge: " + _gaugeValue);
            previsualization.SetValue(_gaugeValue);

            if (_money <= moneyData.money) bribeButton.EnableButton();
            else bribeButton.DisableButton();
        }
    }
}