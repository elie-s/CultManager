using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


namespace CultManager
{
    public class PolicePanelBehavior : MonoBehaviour
    {
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private TMP_Text amountText;
        [SerializeField] private int amount = 0;

        public void Display()
        {
            amountText.text = amount.ToString();
        }
    }
}

