using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CultManager
{
    public class InvestigationUIController : MonoBehaviour
    {
        [SerializeField] private PoliceData data = default;
        [SerializeField] private InvestigationManager manager = default;
        [SerializeField] private InvestigationUIDisplayer displayer = default;
        [SerializeField] private GameObject panelObject = default;
        [SerializeField] private ButtonInteraction rightArrow = default;
        [SerializeField] private ButtonInteraction leftArrow = default;

        [Header("Events")]
        [SerializeField] private UnityEvent onIncreaseBribe = default;
        [SerializeField] private UnityEvent onDecreaseBribe = default;
        [SerializeField] private UnityEvent onOpen = default;
        [SerializeField] private UnityEvent onClose = default;
        [SerializeField] private UnityEvent onBribeSucceded = default;
        [SerializeField] private UnityEvent onBribeFailed = default;

        #region Display
        public void Display()
        {
            displayer.Display(manager.GetBribe(), manager.PredictGauge());
        }

        public void Open()
        {
            panelObject?.SetActive(true);
            GameManager.currentPanel = CurrentPanel.PolicePanel;
            Display();
        }

        public void Close()
        {
            panelObject?.SetActive(false);
            GameManager.currentPanel = CurrentPanel.None;
        }
        #endregion

        #region Actions
        public void IncreaseBribe()
        {
            if(manager.IncreaseBribe()) onIncreaseBribe.Invoke();
            CheckButtons();
            Display();
        }

        public void DecreaseBribe()
        {
            if(manager.DecreaseBribe()) onDecreaseBribe.Invoke();
            CheckButtons();
            Display();
        }

        public void Buy()
        {
            if (manager.TryBuy()) onBribeSucceded.Invoke();
            else onBribeFailed.Invoke();

            Display();
        }
        #endregion

        #region Utilities
        private void CheckButtons()
        {
            if (manager.totalRatio < data.ratio) rightArrow.EnableButton();
            else rightArrow.DisableButton();

            if (manager.totalRatio > 0) leftArrow.EnableButton();
            else leftArrow.DisableButton();
        }
        #endregion
    }
}