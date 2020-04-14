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
        [SerializeField] private RecruitmentManager recruitmentManager;

        [Header("Display")]
        public Image image;
        public TMP_Text nameAndAgeText;
        public TMP_Text moneyText;
        public TMP_Text policeText;
        public GameObject[] traits= new GameObject[6];

        public void Display(Sprite _image, string _cultistName, int _cultistAge, int _policeValue, int _moneyValue, CultistTraits _cultistTraits)
        {
            image.sprite = _image;
            nameAndAgeText.text = _cultistName + " " + _cultistAge.ToString();
            moneyText.text = _moneyValue.ToString();
            policeText.text = _policeValue.ToString();
            for (int i = 0; i < traits.Length; i++)
            {
                if (_cultistTraits.HasFlag((CultistTraits)Mathf.Pow(2, i)))
                {
                    traits[i].SetActive(true);
                }
                else
                {
                    traits[i].SetActive(false);
                }
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

