using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class UIAnmationController : MonoBehaviour
    {
        [SerializeField] UIAnimator positiveAnimation = default;
        [SerializeField] UIAnimator negativeAnimation = default;

        public void PositiveAnimation()
        {
            positiveAnimation.Play();
        }

        public void NegativeAnimation()
        {
            negativeAnimation.Play();
        }
    }
}

