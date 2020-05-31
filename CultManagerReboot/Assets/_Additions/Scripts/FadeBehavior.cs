using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;


namespace CultManager
{
    public class FadeBehavior : MonoBehaviour
    {
        public bool inverseFade;
        [SerializeField] private SpriteRenderer[] spriteRender = default;
        [SerializeField] private float lerpValue = default;
        [Range(0, 1), SerializeField] private float lerpSpeed = default;
        [SerializeField] private bool toFade = default;

        public UnityEvent fadeOver;

        private void Update()
        {
            if (toFade)
            {
                Fade();
            }
            /*else
            {
                gameObject.SetActive(false);
            }*/
        }

        [ContextMenu("ActivateFade")]
        public void ActivateFade()
        {
            if (!toFade)
            {
                toFade = true;
                for (int i = 0; i < spriteRender.Length; i++)
                {
                    spriteRender[i].color = new Color(spriteRender[i].color.r, spriteRender[i].color.g, spriteRender[i].color.b, 1);
                }
                lerpValue = 0;
            }

        }

        public void ActivateDelayedFade(int delay)
        {
            if (!toFade)
            {
                for (int i = 0; i < spriteRender.Length; i++)
                {
                    spriteRender[i].color = new Color(spriteRender[i].color.r, spriteRender[i].color.g, spriteRender[i].color.b, 1);
                }
                lerpValue = 0;
                Invoke("EnableFade", delay);
            }
        }

        public void EnableFade()
        {
            toFade = true;
        }

        public void Fade()
        {
            if (lerpValue < 1)
            {
                lerpValue += lerpSpeed * Time.deltaTime;
                float a = Mathf.SmoothStep(1, 0, lerpValue);
                for (int i = 0; i < spriteRender.Length; i++)
                {
                    if (inverseFade)
                    {
                        spriteRender[i].color = new Color(spriteRender[i].color.r, spriteRender[i].color.g, spriteRender[i].color.b, 1 - a);
                    }
                    else
                    {
                        spriteRender[i].color = new Color(spriteRender[i].color.r, spriteRender[i].color.g, spriteRender[i].color.b, a);
                    }
                    
                }
                
            }
            else
            {
                toFade = false;
                fadeOver.Invoke();
            }

        }
    }
}

