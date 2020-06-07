using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;


namespace CultManager
{
    public class ResetScreen : MonoBehaviour
    {
        [SerializeField] private Image transition;
        [SerializeField] private Image sizeableImage;
        [SerializeField] private TMP_Text displayText;
        [SerializeField] private float lerpValue;
        [Range(0,1),SerializeField] private float lerpSpeed;
        [SerializeField] private bool toLerp;

        [SerializeField] private Sprite spawn;
        [SerializeField] private Sprite demon;

        private float currentLerpSpeed;
        private bool isFade;

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
        public void ActivateReset(string display)
        {
            if (!toLerp)
            {
                isFade = true;
                currentLerpSpeed = lerpSpeed;
                toLerp = true;
                transition.gameObject.SetActive(true);
                sizeableImage.gameObject.SetActive(false);
                transition.color = new Color(transition.color.r, transition.color.g, transition.color.b, 1);
                lerpValue = 0;
                displayText.text = display;
                displayText.gameObject.SetActive(true);
            }
            
        }

        public void ActivateSpawn()
        {
            if (!toLerp)
            {
                isFade = false;
                currentLerpSpeed = lerpSpeed;
                toLerp = true;
                transition.gameObject.SetActive(true);
                transition.color = new Color(transition.color.r, transition.color.g, transition.color.b, 1);
                sizeableImage.gameObject.SetActive(true);
                sizeableImage.sprite = spawn;
                sizeableImage.rectTransform.localScale = new Vector3(1, 1, 1);
                lerpValue = 0;
                displayText.gameObject.SetActive(false);
            }

        }

        public void ActivateDemon()
        {
            if (!toLerp)
            {
                isFade = false;
                currentLerpSpeed = lerpSpeed;
                toLerp = true;
                transition.gameObject.SetActive(true);
                transition.color = new Color(transition.color.r, transition.color.g, transition.color.b, 1);
                sizeableImage.gameObject.SetActive(true);
                sizeableImage.sprite = demon;
                sizeableImage.rectTransform.localScale = new Vector3(1, 1, 1);
                lerpValue = 0;
                displayText.gameObject.SetActive(false);
            }

        }

        public void TransitionScreen()
        {
            if (lerpValue < 1)
            {
                transition.raycastTarget = true;
                lerpValue += currentLerpSpeed * Time.deltaTime;
                float a = Mathf.SmoothStep(1, 0, lerpValue);
                if (isFade)
                {
                    transition.color = new Color(transition.color.r, transition.color.g, transition.color.b, a);
                }
                sizeableImage.rectTransform.localScale = new Vector3(4 * (1 - a), 4 * (1 - a), 1);
                if (lerpValue > 0.15f)
                {
                    displayText.gameObject.SetActive(false);
                    //sizeableImage.gameObject.SetActive(false);
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

