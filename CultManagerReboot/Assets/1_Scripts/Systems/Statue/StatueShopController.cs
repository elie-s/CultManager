using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using UnityEngine;
using UnityEngine.Events;

namespace CultManager
{
    public class StatueShopController : MonoBehaviour
    {
        [SerializeField] private StatuesData data = default;
        [SerializeField] private GameObject menuGO = default;
        [SerializeField] private GameObject demonTreeGO = default;
        [SerializeField] private GameObject statueMenuGO = default;
        [SerializeField] private StatueShopValuesDisplayer displayer = default;
        [SerializeField] private DemonShopCapsuleBehaviour[] capsules = default;
        [SerializeField] private StatueManager manager = default;
        [Header("Events")]
        [SerializeField] private UnityEvent onBuyFailed = default;
        [SerializeField] private UnityEvent onBuySucceeded = default;
        [SerializeField] private UnityEvent onNextStatue = default;
        [SerializeField] private UnityEvent onPreviousStatue = default;
        [SerializeField] private UnityEvent onDemonTreeOpen = default;
        [SerializeField] private UnityEvent onStatueMenuOpen = default;
        [SerializeField] private UnityEvent onDemonTreeClose = default;
        [SerializeField] private UnityEvent onStatueMenuClose = default;

        private int statueIndex = 0;
        private CurrentPanel lastPanel = CurrentPanel.DemonTreePanel;

        private DemonName currentDemonToDisplay => data.demonsAvailable[statueIndex];

        private void Start()
        {
            UpdateMenu();
        }

        #region Updates
        private void UpdateDisplayer()
        {
            displayer.SetIndex(statueIndex);
            displayer.UpdateDisplay();
        }

        private void UpdateDemonTree()
        {
            foreach (DemonShopCapsuleBehaviour capsule in capsules)
            {
                capsule.UpdateDisplay();
            }
        }

        public void UpdateMenu()
        {
            UpdateDisplayer();
            UpdateDemonTree();
        }
        #endregion

        #region Open/Close
        public void OpenDemonTree(bool _main = true)
        {
            if (_main) menuGO?.SetActive(true);
            demonTreeGO?.SetActive(true);
            GameManager.currentPanel = CurrentPanel.DemonTreePanel;
            onDemonTreeOpen.Invoke();
        }

        public void OpenStatueMenu(bool _main = true)
        {
            if (_main) menuGO?.SetActive(true);
            statueMenuGO?.SetActive(true);
            GameManager.currentPanel = CurrentPanel.DemonStatuePanel;
            onStatueMenuOpen.Invoke();
        }

        public void CloseDemonTree(bool _main = true)
        {
            if (_main) menuGO?.SetActive(false);
            demonTreeGO?.SetActive(false);
            GameManager.currentPanel = CurrentPanel.None;
            lastPanel = CurrentPanel.DemonTreePanel;
            onDemonTreeClose.Invoke();
        }

        public void CloseStatueMenu(bool _main = true)
        {
            if(_main) menuGO?.SetActive(false);
            statueMenuGO?.SetActive(false);
            GameManager.currentPanel = CurrentPanel.None;
            lastPanel = CurrentPanel.DemonStatuePanel;
            onStatueMenuClose.Invoke();
        }

        [ContextMenu("Open")]
        public void Open()
        {
            if (lastPanel == CurrentPanel.DemonTreePanel) OpenDemonTree();
            else if (lastPanel == CurrentPanel.DemonStatuePanel) OpenStatueMenu();
        }

        [ContextMenu("Close")]
        public void Close()
        {
            if (lastPanel == CurrentPanel.DemonTreePanel) CloseDemonTree();
            else if (lastPanel == CurrentPanel.DemonStatuePanel) CloseStatueMenu();
        }

        #endregion

        #region Navigation
        public void GetToStatue(int _demonId)
        {
            statueIndex = FindDemonIndex((DemonName)_demonId);
            OpenStatueMenu(false);
            CloseDemonTree(false);
        }

        public void GoBackToTree()
        {
            CloseStatueMenu(false);
            OpenDemonTree(false);
        }

        public void NextStatue()
        {
            onNextStatue.Invoke();
            IncreaseIndex();
            UpdateDisplayer();
        }

        public void PreviousPart()
        {
            onPreviousStatue.Invoke();
            DecreaseIndex();
            UpdateDisplayer();
        }
        #endregion

        #region Actions
        public void Buy()
        {
            if (manager.TryBuyStatue(currentDemonToDisplay)) onBuySucceeded.Invoke();
            else onBuyFailed.Invoke();
        }
        #endregion

        #region Utilities
        public void IncreaseIndex()
        {
            statueIndex = (statueIndex + 1) % data.demonsAvailable.Length;
        }

        public void DecreaseIndex()
        {
            statueIndex--;
            statueIndex = statueIndex < 0 ? data.demonsAvailable.Length - 1 : statueIndex;
        }

        private int FindDemonIndex(DemonName _demon)
        {
            for (int i = 0; i < data.demonsAvailable.Length; i++)
            {
                if (data.demonsAvailable[i] == _demon) return i;
            }

            return 0;
        }
        #endregion
    }
}