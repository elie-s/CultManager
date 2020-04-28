using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


namespace CultManager
{
    public class BloodBankUIDisplay : MonoBehaviour
    {
        [SerializeField] private BloodBankData data = default;
        [SerializeField] private PuzzeManager puzzle;
        [SerializeField] private Image[] BloodBars;
        [SerializeField] private GameObject hud;


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

        public void Open()
        {
            hud.SetActive(true);
        }

        public void Close()
        {
            hud.SetActive(false);
        }

        public void Summon()
        {
            Debug.Log("Willsummon");
            puzzle.SummonIt();
        }
    }
}

