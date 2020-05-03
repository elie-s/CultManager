using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


namespace CultManager
{
    public class BloodBankUIDisplay : MonoBehaviour
    {
        [SerializeField] private CurrentPanel thisPanelName;
        [SerializeField] private BloodBankData data = default;
        [SerializeField] private PuzzeManager puzzle;
        [SerializeField] private CameraController camControl;
        [SerializeField] private Image[] BloodBars;
        [SerializeField] private GameObject hud;

        [SerializeField] private Image transition;
        [SerializeField] private float lerpValue;
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
        }
        void Display()
        {
            for (int i = 0; i < data.bloodBanks.Length; i++)
            {
                BloodBars[i].fillAmount = (float)data.bloodBanks[i].gauge.value/ (float)data.bloodBanks[i].gauge.max;
            }
        }

        public void Open()
        {
            if (GameManager.currentPanel == CurrentPanel.None)
            {
                GameManager.currentPanel = thisPanelName;
                transition.gameObject.SetActive(true);
                transition.color = new Color(0, 0, 0, 1);
                lerpValue = 0;
                toLerp = true;
                hud.SetActive(true);
            }
        }

        public void Close()
        {
            if (GameManager.currentPanel == thisPanelName)
            {
                GameManager.currentPanel = CurrentPanel.None;
                transition.color = new Color(0, 0, 0, 1);
                lerpValue = 0;
                toLerp = true;
                puzzle.ClearSelection();
                puzzle.FailedPattern();
                camControl.TransitionToOrigin();
                hud.SetActive(false);

                if (!toLerp)
                {
                    transition.gameObject.SetActive(false);
                }
            }
        }

        public void Summon()
        {
            puzzle.SummonIt();
        }


        public void TransitionScreen()
        {
            if (lerpValue < 1)
            {
                transition.raycastTarget = true;
                lerpValue += 0.6f * Time.deltaTime;
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

