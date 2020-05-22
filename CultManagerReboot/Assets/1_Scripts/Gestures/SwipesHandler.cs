using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CultManager
{
    public class SwipesHandler : MonoBehaviour
    {
        [SerializeField] private SwipeTrigger swipe = default;
        [SerializeField] private UnityEvent onSwipe = default;

        void Start()
        {
            swipe.Play(this, onSwipe.Invoke);
        }
    }
}