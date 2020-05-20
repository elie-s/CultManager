using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CultManager
{
    public class SwipesHandler : MonoBehaviour
    {
        [SerializeField] private SwipeTrigger swipeRight = default;
        [SerializeField] private UnityEvent onSwipeRight = default;

        void Start()
        {
            swipeRight.Play(this, onSwipeRight.Invoke);
        }
    }
}