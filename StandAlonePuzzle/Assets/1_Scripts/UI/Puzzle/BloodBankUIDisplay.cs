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
        [SerializeField] private Image BloodBarA;
        [SerializeField] private Image BloodBarB;
        [SerializeField] private Image BloodBarO;
        [SerializeField] private GameObject hud;
        [SerializeField] private ButtonInteraction summonButton;


        private void Start()
        {
            if (!SaveManager.saveLoaded)
            {
                data.Reset();
            }
        }

        private void Update()
        {
            Display();

            if (puzzle.ValidatePattern())
            {
                summonButton.EnableButton();
            }
            else
            {
                summonButton.DisableButton();
            }
        }
        void Display()
        {
            for (int i = 0; i < data.bloodBanks.Length; i++)
            {
                switch (data.bloodBanks[i].bloodGroup)
                {
                    case BloodType.O:
                        {
                            BloodBarO.fillAmount = (float)data.bloodBanks[i].gauge.value / (float)data.bloodBanks[i].gauge.max;
                        }
                        break;
                    case BloodType.A:
                        {
                            BloodBarA.fillAmount = (float)data.bloodBanks[i].gauge.value / (float)data.bloodBanks[i].gauge.max;
                        }
                        break;
                    case BloodType.B:
                        {
                            BloodBarB.fillAmount = (float)data.bloodBanks[i].gauge.value / (float)data.bloodBanks[i].gauge.max;
                        }
                        break;
                }
                
            }
        }

        public void InadequateBloodAnim(BloodType blood)
        {
            switch (blood)
            {
                case BloodType.O:
                    {
                        BloodBarO.GetComponent<UIAnmationController>().NegativeAnimation();
                    }
                    break;
                case BloodType.A:
                    {
                        BloodBarA.GetComponent<UIAnmationController>().NegativeAnimation();
                    }
                    break;
                case BloodType.B:
                    {
                        BloodBarB.GetComponent<UIAnmationController>().NegativeAnimation();
                    }
                    break;
            }
        }

        public void BloodUtilizeAnim(BloodType blood)
        {
            switch (blood)
            {
                case BloodType.O:
                    {
                        BloodBarO.GetComponent<UIAnmationController>().PositiveAnimation();
                    }
                    break;
                case BloodType.A:
                    {
                        BloodBarA.GetComponent<UIAnmationController>().PositiveAnimation();
                    }
                    break;
                case BloodType.B:
                    {
                        BloodBarB.GetComponent<UIAnmationController>().PositiveAnimation();
                    }
                    break;
            }
        }

        public void Open()
        {
            puzzle.ClearSelection();
            hud.SetActive(true);
            data.FillAllBloodBanks();
        }

        public void Close()
        {
            //puzzle.ClearSelection();
            hud.SetActive(false);
        }
    }
}

