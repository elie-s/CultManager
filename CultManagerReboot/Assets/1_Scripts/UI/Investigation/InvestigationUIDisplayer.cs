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
        [SerializeField] private UISwitch[] icons = default;
        [SerializeField] private UISwitch[] descriptions = default;
        [SerializeField] private GameObject comeTomorrowObject = default;
        [SerializeField] private TextMeshProUGUI moneyValue = default;

        public void Display(int _money, float _gaugeValue, bool _canBribe)
        {
            moneyValue.text = _money.Format();
            mainGauge.SetValue(policeData.ratio);
            Debug.Log("gauge: " + _gaugeValue);
            previsualization.SetValue(_gaugeValue);

            HandleIcons(_gaugeValue);

            if (_money > moneyData.money || _money == 0) bribeButton.DisableButton();
            else bribeButton.EnableButton();
        }

        private void HandleIcons(float _gaugeValue)
        {
            for (int i = 0; i < icons.Length; i++)
            {
                if (_gaugeValue > (float)i / (float)icons.Length) icons[i].SetB();
                else icons[i].SetA();

                if (_gaugeValue > (float)i / (float)icons.Length) descriptions[i].SetB();
                else descriptions[i].SetA();
            }
        }
    }
}