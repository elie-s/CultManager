using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;


namespace CultManager
{
    public class ButtonInteraction : MonoBehaviour
    {
        [SerializeField] private Sprite disabledSprite = default;
        [SerializeField] private Sprite enabledSprite = default;
        [SerializeField] private Sprite pressedSprite = default;
        [SerializeField] private Image buttonImage = default;
        [SerializeField] private Color disabledColor = default;
        [SerializeField] private Color enabledColor = default;
        [SerializeField] private Color pressedColor = default;
        [SerializeField] private float buttonPressInterval = default;
        [SerializeField] bool isEnabled = false;
        public bool isPressed = false;

        public UnityEvent buttonPressOnDisabled;
        public UnityEvent buttonPressImmideate;
        public UnityEvent buttonPressDelayed;

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
            else if(!isEnabled)
            {
                buttonPressOnDisabled.Invoke();
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

