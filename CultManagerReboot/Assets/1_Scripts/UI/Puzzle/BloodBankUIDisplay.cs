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
                hud.SetActive(true);
            }
        }

        public void Close()
        {
            if (GameManager.currentPanel == thisPanelName)
            {
                puzzle.ClearSelection();
                puzzle.FailedPattern();
                GameManager.currentPanel = CurrentPanel.None;
                camControl.TransitionToOrigin();
                hud.SetActive(false);
            }
        }

        public void Summon()
        {
            puzzle.SummonIt();
        }

        
    }
}

