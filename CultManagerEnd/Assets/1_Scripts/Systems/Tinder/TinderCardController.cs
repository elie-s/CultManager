using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CultManager
{
    public class TinderCardController : MonoBehaviour
    {
        [SerializeField] private CultData data = default;
        [SerializeField] private TinderManager manager = default;
        [SerializeField] private TinderCardDisplayer displayer = default;
        [SerializeField] private GameObject uiObject = default;
        [Header("Movement Settings")]
        [SerializeField] private float swipeForce = 1.0f;
        [SerializeField] private float validationThreshold = 0.5f;
        [SerializeField] private float exitDuration = 0.5f;
        [SerializeField] private float backDuration = 0.1f;
        [Header("Events")]
        [SerializeField] private UnityEvent onOpen = default;
        [SerializeField] private UnityEvent onClose = default;
        [SerializeField] private UnityEvent onCandidateAccepted = default;
        [SerializeField] private UnityEvent onCandidateRejected = default;
        [SerializeField] private UnityEvent onNewCandidate = default;
        [SerializeField] private UnityEvent onExitAccepted = default;
        [SerializeField] private UnityEvent onExitRejected = default;


        private bool onMove = false;

        private float lastCandidates = 0;

        private void Update()
        {
            if (data.candidatesCount > 0 && !onMove && Gesture.Movement != Vector2.zero) StartCoroutine(GettingInputRoutine());
            if (uiObject.activeSelf && lastCandidates == 0) CheckForNewCandidates();
        }

        #region Display
        private void DisplayCandidate()
        {
            displayer?.SetCandidate(manager?.TryGetCandidate());
            displayer?.Display();
            lastCandidates = data.candidatesCount;
        }

        private void CheckForNewCandidates()
        {
            if(data.candidatesCount != lastCandidates)
            {
                DisplayCandidate();
                onNewCandidate.Invoke();
            }
        }

        public void Open()
        {
            uiObject?.SetActive(true);
            GameManager.currentPanel = CurrentPanel.RecruitmentPanel;
            DisplayCandidate();

            onOpen.Invoke();
        }

        public void Close()
        {
            uiObject?.SetActive(false);
            GameManager.currentPanel = CurrentPanel.None;
            onClose.Invoke();
        }
        #endregion

        #region Validation
        public void Accept()
        {
            if(!onMove) StartCoroutine(ExitRoutine(true, 0.0f));
        }

        public void Reject()
        {
            if (!onMove) StartCoroutine(ExitRoutine(false, 0.0f));
        }

        public void SendResult(bool _accepted)
        {
            if(_accepted)
            {
                onCandidateAccepted.Invoke();
                Debug.Log("Candidate accepted.");
                manager?.AcceptCurrent();
            }
            else
            {
                onCandidateRejected.Invoke();
                Debug.Log("Candidate rejected.");
                manager?.Reject();
            }

            DisplayCandidate();
        }
        #endregion

        #region Swipe

        private IEnumerator GettingInputRoutine()
        {
            onMove = true;
            float lerpValue = 0.0f;

            while (Gesture.Touching)
            {
                lerpValue = Gesture.Movement.x * swipeForce;

                yield return null;
                displayer.SetLerp(lerpValue);
            }

            onMove = false;

            if (Mathf.Abs(lerpValue) > validationThreshold) StartCoroutine(ExitRoutine(lerpValue > 0, lerpValue));
            else StartCoroutine(BackToOriginRoutine(lerpValue));
        }

        private IEnumerator ExitRoutine(bool _accepted, float _lerp)
        {
            onMove = true;
            if (_accepted) onExitAccepted.Invoke();
            else onExitRejected.Invoke();

            float lerpValue = 0.0f;
            Iteration iteration = new Iteration(exitDuration);

            iteration.Increment(_accepted ? Mathf.Lerp(0.0f, 1.0f, _lerp) * exitDuration : Mathf.InverseLerp(0.0f, -1.0f, _lerp) * exitDuration);

            while (iteration.isBelowOne)
            {
                lerpValue = _accepted ? Mathf.Lerp(0.0f, 1.0f, iteration.fraction) : Mathf.Lerp(0.0f, -1.0f, iteration.fraction);
                displayer.SetLerp(lerpValue);

                yield return iteration.YieldIncrement();
            }

            SendResult(_accepted);
            displayer.SetLerp(0.0f);

            onMove = false;
        }

        private IEnumerator BackToOriginRoutine(float _lerp)
        {
            onMove = true;

            float lerpValue = 0.0f;
            Iteration iteration = new Iteration(backDuration);

            while (iteration.isBelowOne)
            {
                lerpValue = Mathf.Lerp(_lerp, 0.0f, iteration.fraction);
                displayer.SetLerp(lerpValue);

                yield return iteration.YieldIncrement();
            }

            displayer.SetLerp(0.0f);

            onMove = false;
        }

        #endregion
    }
}