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
        [SerializeField] private Color disabledColor = Color.white;
        [SerializeField] private Color enabledColor = Color.white;
        [SerializeField] private Color pressedColor = Color.white;
        [SerializeField] private float buttonPressInterval = default;
        [SerializeField] bool isEnabled = false;
        [SerializeField] private bool startEnabled = false;
        [SerializeField] private bool disableSpriteShifting = false;
        [SerializeField] private bool noDelay = false;
        public bool isPressed = false;

        public UnityEvent buttonPressOnDisabled;
        public UnityEvent buttonPressImmideate;
        public UnityEvent buttonPressDelayed;

        private bool delaying = false;

        private void Awake()
        {
            isEnabled = false;
            
            if(!disableSpriteShifting) buttonImage.sprite = disabledSprite;
            buttonImage.color = disabledColor;
            if (startEnabled) EnableButton();
        }

        public void EnableButton()
        {
            if (delaying)
            {
                StopAllCoroutines();
                isPressed = false;
                if (!disableSpriteShifting) buttonImage.sprite = enabledSprite;
                buttonImage.color = enabledColor;
            }

            if (!isEnabled)
            {
                Debug.Log(gameObject.name + " button enabled.");
                isEnabled = true;
                if (!disableSpriteShifting) buttonImage.sprite = enabledSprite;
                buttonImage.color = enabledColor;
            }
        }

        public void DisableButton()
        {
            if (delaying)
            {
                StopAllCoroutines();
                isPressed = false;
                if (!disableSpriteShifting) buttonImage.sprite = enabledSprite;
                buttonImage.color = enabledColor;
            }

            if (isEnabled)
            {
                Debug.Log(gameObject.name + " button disabled.");
                isEnabled = false;
                if (!disableSpriteShifting) buttonImage.sprite = disabledSprite;
                buttonImage.color = disabledColor;
            }
        }

        public void OnPress()
        {
            if (Gesture.Movement != Vector2.zero) return;

            if (isEnabled && !isPressed)
            {
                isPressed = true;
                if (!disableSpriteShifting) buttonImage.sprite = pressedSprite;
                buttonImage.color = pressedColor;
                buttonPressImmideate.Invoke();

                if(!noDelay) PressEnd();
                else
                {
                    isPressed = false;
                    if (!disableSpriteShifting) buttonImage.sprite = enabledSprite;
                    buttonImage.color = enabledColor;
                }
            }
            else if(!isEnabled)
            {
                buttonPressOnDisabled.Invoke();
            }
        }

        public void PressEnd()
        {
            if (!delaying && gameObject.activeSelf) StartCoroutine(PressEndRoutine());
        }

        private IEnumerator PressEndRoutine()
        {
            delaying = true;

            yield return new WaitForSeconds(buttonPressInterval);

            if (isEnabled)
            {
                buttonPressDelayed.Invoke();
                isPressed = false;
                if (!disableSpriteShifting) buttonImage.sprite = enabledSprite;
                buttonImage.color = enabledColor;
            }

            delaying = false;
        }

    }
}

