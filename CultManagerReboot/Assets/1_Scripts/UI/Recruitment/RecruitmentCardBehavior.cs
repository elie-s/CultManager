using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


namespace CultManager
{
    public class RecruitmentCardBehavior : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] private RecruitmentManager recruitmentManager = default;
        [SerializeField] private CultData data = default;


        [Header("Display")]
        [SerializeField] private Image image = default;
        [SerializeField] private TMP_Text nameText = default;
        [SerializeField] private TMP_Text moneyText = default;
        [SerializeField] private TMP_Text policeText = default;
        [SerializeField] private TextMeshProUGUI remainingCandidates = default;
        [SerializeField] private UISwitch[] bloodSwitch = default;

        public void Display(Sprite _image, string _cultistName, int _policeValue, int _moneyValue, BloodType _blood)
        {
            image.sprite = _image;
            nameText.text = _cultistName;
            moneyText.text = _moneyValue.ToString();
            policeText.text = _policeValue.ToString();
            UpdateBlood(_blood);
            remainingCandidates.text = data.candidatesCount.ToString();
        }

        private void UpdateBlood(BloodType _blood)
        {
            switch (_blood)
            {
                case BloodType.none:
                    break;

                case BloodType.O:
                    bloodSwitch[0].SetC();
                    bloodSwitch[1].SetC();
                    break;

                case BloodType.A:
                    bloodSwitch[0].SetA();
                    bloodSwitch[1].SetA();
                    break;

                case BloodType.B:
                    bloodSwitch[0].SetB();
                    bloodSwitch[1].SetB();
                    break;

                case BloodType.AB:
                    break;

                default:
                    break;
            }
        }

        public void Close()
        {
            recruitmentManager.StopRecruitment();
        }

        public void Kept()
        {
            recruitmentManager.Kept();
        }

        public void Left()
        {
            recruitmentManager.Left();
        }

    }
}

