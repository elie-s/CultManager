using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;


namespace CultManager
{
    public class ButtonInteraction : MonoBehaviour
    {
        [SerializeField] private Sprite disabledSprite;
        [SerializeField] private Sprite enabledSprite;
        [SerializeField] private Sprite pressedSprite;
        [SerializeField] private Image buttonImage;
        [SerializeField] private Color disabledColor;
        [SerializeField] private Color enabledColor;
        [SerializeField] private Color pressedColor;
        [SerializeField] private float buttonPressInterval;
        [SerializeField] bool isEnabled = false;
        public bool isPressed = false;

        public UnityEvent buttonPressDelayed;
        public UnityEvent buttonPressImmideate;

        private void Start()
        {
            isEnabled = false;
            buttonImage.sprite = disabledSprite;
            buttonImage.color = disabledColor;
        }

        public void EnableButton()
        {
            if (!isEnabled)
            {
                isEnabled = true;
                buttonImage.sprite = enabledSprite;
                buttonImage.color = enabledColor;
            }
        }

        public void DisableButton()
        {
            if (isEnabled)
            {
                isEnabled = false;
                buttonImage.sprite = disabledSprite;
                buttonImage.color = disabledColor;
            }
        }

        public void OnPress()
        {
            if (isEnabled && !isPressed)
            {
                isPressed = true;
                buttonImage.sprite = pressedSprite;
                buttonImage.color = pressedColor;
                buttonPressImmideate.Invoke();

                Invoke("PressEnd", buttonPressInterval);
            }
        }

        public void PressEnd()
        {
            if (isPressed)
            {
                buttonPressDelayed.Invoke();
                isPressed = false;
                buttonImage.sprite = enabledSprite;
                buttonImage.color = enabledColor;
            }
        }

    }
}

