using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class BackgroundDisplay : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] backgrounds;
        [Range(3, 20)] [SerializeField] float timeInterval;
        private float timeToChange;
        private int backgroundId;

        private void Start()
        {
            
        }

        private void Update()
        {
            if (Time.time > timeToChange)
            {
                NextBackground();
                timeToChange += timeInterval;
            }
        }

        void NextBackground()
        {
            for (int i = 0; i < backgrounds.Length; i++)
            {
                if (i == backgroundId)
                {
                    backgrounds[i].color = new Color(1, 1, 1, 1);
                }
                else
                {
                    backgrounds[i].color = new Color(1, 1, 1, 0);
                }
            }
            if (backgroundId < backgrounds.Length - 1)
            {
                backgroundId++;
            }
            else
            {
                backgroundId = 0;
            }
                
        }
    }
}

