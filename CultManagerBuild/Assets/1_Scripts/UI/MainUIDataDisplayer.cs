using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace CultManager
{
    public class MainUIDataDisplayer : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private InfluenceData influence = default;
        [SerializeField] private MoneyData money = default;
        [Header("Fields")]
        [SerializeField] private TextMeshProUGUI moneyField = default;
        [SerializeField] private TextMeshProUGUI influenceField = default;

        // Update is called once per frame
        void Update()
        {
            SetFields();
        }

        private void SetFields()
        {
            moneyField.text = "$"+money.value.ToString();
            influenceField.text = influence.value.ToString() + " F";
        }
    }
}