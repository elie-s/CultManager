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
        [SerializeField] private InfluenceData influence;
        [SerializeField] private CultData cult;
        [SerializeField] private PoliceData police;
        [SerializeField] private MoneyData money;
        
        [Header("Display")]
        [SerializeField] private Image influenceBar;
        [SerializeField] private Image policeBar;
        [SerializeField] private TMP_Text cultistsText;
        [SerializeField] private TMP_Text moneyText;

        private void Update()
        {
            Display();
        }

        public void Display()
        {
            influenceBar.fillAmount = Mathf.Lerp(influenceBar.fillAmount,influence.ratio,Time.deltaTime);
            policeBar.fillAmount = Mathf.Lerp(policeBar.fillAmount,police.ratio,Time.deltaTime);
            cultistsText.text = cult.cultists.Count.ToString();
            moneyText.text = money.value.ToString();
        }
    }
}

