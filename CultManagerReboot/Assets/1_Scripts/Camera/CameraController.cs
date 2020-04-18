﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CultManager
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera cam = default;
        [SerializeField] private UnityEvent onTransitionStart = default;
        [SerializeField] private UnityEvent onTransitionEnd = default;
        [SerializeField] private CameraTarget[] targets = default;
        [SerializeField] private CameraControllerSettings settings = default;

        public static bool isAtOrigin;

        private CameraTarget origin;
        private bool locked;

        private void OnEnable()
        {
            InitCam();
            isAtOrigin = true;
        }

        private void InitCam()
        {
            if(cam) origin = new CameraTarget(cam);
            else origin = new CameraTarget(Camera.main);
        }

        public void Transition(CameraTarget _target)
        {
            if (locked) return;

            StartCoroutine(TransitionRoutine(_target));
        }

        public void Transition(int _index)
        {
            if (GameManager.currentPanel == CurrentPanel.None)
            {
                Transition(targets[_index]);
                GameManager.currentIsland = (CurrentIsland)(_index + 1);
                isAtOrigin = false;
            }
        }

        public void TransitionToOrigin()
        {
            if (GameManager.currentPanel == CurrentPanel.None)
            {
                Transition(origin);
                GameManager.currentIsland = (CurrentIsland)(0);
                isAtOrigin = true;
            }
            
        }

        private IEnumerator TransitionRoutine(CameraTarget _target)
        {
            locked = true;
            Iteration iteration = new Iteration(settings.transitionDuration, settings.transitionCurve);
            Vector2 startPosition = cam.transform.localPosition;
            float startSize = cam.orthographicSize;

            onTransitionStart.Invoke();

            while (iteration.isBelowOne)
            {
                cam.transform.localPosition = Vector2.Lerp(startPosition, _target.position, iteration.curveEvaluation);
                cam.orthographicSize = Mathf.Lerp(startSize, _target.size, iteration.curveEvaluation);

                yield return iteration.YieldIncrement();
            }

            cam.transform.localPosition = _target.position;
            cam.orthographicSize = _target.size;
            locked = false;
            onTransitionEnd.Invoke();
        }
    }
}