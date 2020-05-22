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
        [SerializeField] private CameraController camControl;
        [SerializeField] private Image[] BloodBars;
        [SerializeField] private GameObject hud;
        [SerializeField] private GameObject summonButton;

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

            summonButton.SetActive((puzzle.ValidatePattern()));
        }
        void Display()
        {
            for (int i = 0; i < data.bloodBanks.Length; i++)
            {
                BloodBars[i].fillAmount = (float)data.bloodBanks[i].gauge.value / (float)data.bloodBanks[i].gauge.max;
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
            camControl.Transition(5);
            hud.SetActive(false);

            if (!toLerp)
            {
                transition.gameObject.SetActive(false);
            }
        }


        public void Summon()
        {
            puzzle.SummonIt();
            Close();
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

