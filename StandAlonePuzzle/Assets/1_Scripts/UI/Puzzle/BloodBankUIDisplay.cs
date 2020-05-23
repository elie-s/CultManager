﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


namespace CultManager
{
    public class BloodBankUIDisplay : MonoBehaviour
    {
        [SerializeField] private BloodBankData data = default;
        [SerializeField] private PuzzeManager puzzle;
        [SerializeField] private CameraController camControl;
        [SerializeField] private Image BloodBarA;
        [SerializeField] private Image BloodBarB;
        [SerializeField] private Image BloodBarO;
        [SerializeField] private GameObject hud;
        [SerializeField] private ButtonInteraction summonButton;

        [SerializeField] private Image transition;
        [SerializeField] private float lerpValue;
        [SerializeField] private float lerpInterval;
        [SerializeField] private bool toLerp;

        private void Start()
        {
            transition.gameObject.SetActive(false);
            if (!SaveManager.saveLoaded)
            {
                data.Reset();
            }
        }

        private void Update()
        {
            Display();

            if (toLerp)
            {
                TransitionScreen();
            }

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

        public void Open()
        {
            transition.gameObject.SetActive(true);
            transition.color = new Color(0, 0, 0, 1);
            lerpValue = 0;
            lerpInterval = 0.6f;
            toLerp = true;
            hud.SetActive(true);
            data.FillAllBloodBanks();
        }

        public void Close()
        {
            transition.color = new Color(0, 0, 0, 1);
            lerpValue = 0;
            lerpInterval = 0.6f;
            toLerp = true;
            puzzle.ClearSelection();
            camControl.TransitionToOrigin();
            hud.SetActive(false);

            if (!toLerp)
            {
                transition.gameObject.SetActive(false);
            }
        }


        public void Summon()
        {
            //puzzle.SummonIt();
            puzzle.EraseSymbol();
            transition.color = new Color(0, 0, 0, 1);
            lerpValue = 0;
            lerpInterval = 0.6f;
            toLerp = true;
            puzzle.ClearSelection();
            camControl.Transition(5);
            hud.SetActive(false);

            if (!toLerp)
            {
                transition.gameObject.SetActive(false);
            }
        }


        public void TransitionScreen()
        {
            if (lerpValue < 1)
            {
                transition.raycastTarget = true;
                lerpValue += lerpInterval * Time.deltaTime;
                float a = Mathf.SmoothStep(1, 0, lerpValue);
                transition.color = new Color(0, 0, 0, a);
            }
            else
            {
                transition.raycastTarget = false;
                toLerp = false;
            }

        }
    }
}

