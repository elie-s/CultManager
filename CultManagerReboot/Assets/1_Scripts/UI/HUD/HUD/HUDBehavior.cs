using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace CultManager
{
    public class HUDBehavior : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] private InfluenceData influence = default;
        [SerializeField] private CultData cult = default;
        [SerializeField] private PoliceData police = default;
        [SerializeField] private MoneyData money = default;

        [Header("Display")]
        [SerializeField] private Image influenceBar = default;
        [SerializeField] private Image policeBar = default;
        [SerializeField] private TMP_Text cultistsText = default;
        [SerializeField] private TMP_Text moneyText = default;

        private void Update()
        {
            Display();
        }

        public void Display()
        {
            influenceBar.fillAmount = Mathf.Lerp(influenceBar.fillAmount,influence.ratio,Time.deltaTime);
            policeBar.fillAmount = Mathf.Lerp(policeBar.fillAmount,police.ratio,Time.deltaTime);
            cultistsText.text = cult.cultists.Count.ToString();
            moneyText.text = money.money.ToString();
        }
    }
}

