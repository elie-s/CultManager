using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CultManager
{
    public class UIGeneralDisplayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI cultistsAmount = default;
        [SerializeField] private TextMeshProUGUI moneyAmount = default;
        [SerializeField] private TextMeshProUGUI influenceAmount = default;
        [SerializeField] private TextMeshProUGUI hardCurrencyAmount = default;
        [SerializeField] private RadialGaugeSmoothener gauge = default;

        public void UpdateDisplay(string _cultists, string _money, string _influence, string _currency, float _gauge)
        {
            cultistsAmount.text = _cultists;
            moneyAmount.text = _money;
            influenceAmount.text = _influence;
            hardCurrencyAmount.text = _currency;

            gauge.SetValue(_gauge);
        }

    }
}