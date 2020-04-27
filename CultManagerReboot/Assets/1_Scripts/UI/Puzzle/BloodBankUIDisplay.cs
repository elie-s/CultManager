using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


namespace CultManager
{
    public class BloodBankUIDisplay : MonoBehaviour
    {
        [SerializeField] private BloodBankData data = default;
        [SerializeField] private Image[] BloodBars;


        private void Update()
        {
            Display();
        }
        void Display()
        {
            for (int i = 0; i < data.bloodBanks.Length; i++)
            {
                BloodBars[i].fillAmount = data.bloodBanks[i].gauge.ratio;
            }
        }
    }
}

