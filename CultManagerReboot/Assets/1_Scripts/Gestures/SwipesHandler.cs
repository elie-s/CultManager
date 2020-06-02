using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CultManager
{
    public class SwipesHandler : MonoBehaviour
    {
        [SerializeField] private SwipeTrigger swipeRight = default;
        [SerializeField] private SwipeTrigger swipeLeft = default;
        [SerializeField] private UnityEvent onSwipeRight = default;
        [SerializeField] private UnityEvent onSwipeLeft = default;

        void OnEnable()
        {
            if (onSwipeRight != null)
            {
                swipeRight.Play(this, onSwipeRight.Invoke);
            }
            if (onSwipeLeft != null)
            {
                swipeLeft.Play(this, onSwipeLeft.Invoke);
            }
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }
    }
}