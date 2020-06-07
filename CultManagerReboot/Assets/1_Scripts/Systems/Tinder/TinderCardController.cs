using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class TinderCardController : MonoBehaviour
    {
        [SerializeField] private TinderManager manager = default;
        [SerializeField] private TinderCardDisplayer displayer = default;
        [SerializeField] private GameObject uiObject = default;

        private void DisplayCandidate()
        {
            displayer?.SetCandidate(manager?.TryGetCandidate());
            displayer?.Display();
        }

        public void Open()
        {
            uiObject?.SetActive(true);
            GameManager.currentPanel = CurrentPanel.RecruitmentPanel;
            DisplayCandidate();
        }

        public void Close()
        {
            uiObject?.SetActive(false);
            GameManager.currentPanel = CurrentPanel.None;
        }

        public void Accept()
        {
            Debug.Log("Candidate accepted.");
            manager?.AcceptCurrent();
            DisplayCandidate();
        }

        public void Reject()
        {
            Debug.Log("Candidate rejected.");
            manager?.Reject();
            DisplayCandidate();
        }
    }
}