using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CultManager
{
    public class StatueMenuController : MonoBehaviour
    {
        [SerializeField] private StatuesData data = default;
        [SerializeField] private GameObject menuGO = default;
        [SerializeField] private StatueMenuValuesDisplayer displayer = default;
        [SerializeField] private StatueManager manager = default;
        [SerializeField] private PuzzleTransitionBehaviour puzzleTransition = default;
        [Header("Events")]
        [SerializeField] private UnityEvent onBuyFailed = default;
        [SerializeField] private UnityEvent onBuySucceded = default;
        [SerializeField] private UnityEvent onNextPart = default;
        [SerializeField] private UnityEvent onPreviousPart = default;
        [SerializeField] private UnityEvent onCultistAssignementFailed = default;
        [SerializeField] private UnityEvent onCultistAssignementSucceded = default;
        [SerializeField] private UnityEvent onWorkerRemovedFailed = default;
        [SerializeField] private UnityEvent onWorkerRemovedSucceded = default;
        [SerializeField] private UnityEvent onOpen = default;
        [SerializeField] private UnityEvent onClose = default;

        private StatueSet currentSet => data.currentStatueSet;

        private void Start()
        {
            displayer.UpdateDisplay();
        }

        [ContextMenu("Open")]
        public void Open()
        {
            menuGO.SetActive(true);
            GameManager.currentPanel = CurrentPanel.AltarPanel;
            onOpen.Invoke();
        }

        [ContextMenu("Close")]
        public void Close()
        {
            menuGO.SetActive(false);
            GameManager.currentPanel = CurrentPanel.None;
            onClose.Invoke();
        }

        public void UpdateDisplay()
        {
            displayer.UpdateDisplay();
        }

        public void NextPart()
        {
            onNextPart.Invoke();
            currentSet.IncreaseIndex();
            displayer.UpdateDisplay();
        }

        public void PreviousPart()
        {
            onPreviousPart.Invoke();
            currentSet.DecreaseIndex();
            displayer.UpdateDisplay();
        }

        public void AssignAWorker()
        {
            int assigned = manager.AssignWorkers(1);

            if (assigned == 0) onCultistAssignementFailed.Invoke();
            else onCultistAssignementSucceded.Invoke();

            displayer.UpdateDisplay();
        }

        public void FillWorkers()
        {
            int assigned = manager.AssignWorkers(currentSet.currentPart.workers.amountLeft);

            if (assigned == 0) onCultistAssignementFailed.Invoke();
            else onCultistAssignementSucceded.Invoke();

            displayer.UpdateDisplay();
        }

        public void RemoveAWorker()
        {
            int removed = manager.RemoveWorkers(1);

            if (removed == 0) onWorkerRemovedFailed.Invoke();
            else onWorkerRemovedSucceded.Invoke();

            displayer.UpdateDisplay();
        }

        public void EmptyWorkers()
        {
            int removed = manager.RemoveWorkers(currentSet.currentPart.workers.value);

            if (removed == 0) onWorkerRemovedFailed.Invoke();
            else onWorkerRemovedSucceded.Invoke();

            displayer.UpdateDisplay();
        }

        public void Buy()
        {
            if (manager.TryBuyPart()) onBuySucceded.Invoke();
            else onBuyFailed.Invoke();

            displayer.UpdateDisplay();
        }

        public void Resurrect()
        {
            puzzleTransition.GoToPuzzleFromStatue();
        }
    }
}