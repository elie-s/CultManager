using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;


namespace CultManager
{
    public class ResetScreen : MonoBehaviour
    {
        [SerializeField] private Image transition = default;
        [SerializeField] private GameObject displayText = default;
        [SerializeField] private float lerpValue = default;
        [Range(0,1),SerializeField] private float lerpSpeed = default;
        [SerializeField] private bool toLerp = default;


        private void Update()
        {
            if (toLerp)
            {
                TransitionScreen();
            }
            else
            {
                transition.gameObject.SetActive(false);
            }
        }

        [ContextMenu("ResetActivated")]
        public void ActivateReset()
        {
            if (!toLerp)
            {
                toLerp = true;
                transition.gameObject.SetActive(true);
                transition.color = new Color(0, 0, 0, 1);
                lerpValue = 0;
                displayText.SetActive(true);
            }
            
        }

        public void TransitionScreen()
        {
            if (lerpValue < 1)
            {
                transition.raycastTarget = true;
                lerpValue += lerpSpeed * Time.deltaTime;
                float a = Mathf.SmoothStep(1, 0, lerpValue);
                transition.color = new Color(0, 0, 0, a);
                if (lerpValue > 0.15f)
                {
                    displayText.SetActive(false);
                }
            }
            else
            {
                transition.raycastTarget = false;
                toLerp = false;
            }

        }
    }
}

