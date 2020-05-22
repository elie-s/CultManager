﻿using System.Collections;
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


        [Header("Display")]
        public Image image = default;
        public TMP_Text nameAndAgeText = default;
        public TMP_Text moneyText = default;
        public TMP_Text policeText = default;
        public TMP_Text bloodGroupText = default;
        public GameObject[] traits= new GameObject[6];

        public void Display(Sprite _image, string _cultistName, int _cultistAge, int _policeValue, int _moneyValue, BloodType _blood)
        {
            image.sprite = _image;
            nameAndAgeText.text = _cultistName + " " + _cultistAge.ToString();
            moneyText.text = _moneyValue.ToString();
            bloodGroupText.text = _blood.ToString();
            policeText.text = _policeValue.ToString();
            /*for (int i = 0; i < traits.Length; i++)
            {
                if (_cultistTraits.HasFlag((CultistTraits)Mathf.Pow(2, i)))
                {
                    traits[i].SetActive(true);
                }
                else
                {
                    traits[i].SetActive(false);
                }
            }*/
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

