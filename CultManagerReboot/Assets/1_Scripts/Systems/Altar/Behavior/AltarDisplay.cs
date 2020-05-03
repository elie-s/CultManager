using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class AltarDisplay : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] private AltarData altarData;
        [Header("Display")]
        [SerializeField] private SpriteRenderer[] altarParts;

        private void Start()
        {
            
        }

        private void Update()
        {
            for (int i = 0; i < altarParts.Length; i++)
            {
                if (altarData.altarParts[i].isBought)
                {
                    if (altarData.altarParts[i].currentBuildPoints.value>0 && altarData.altarParts[i].currentBuildPoints.ratio<1f)
                    {
                        altarParts[i].color = new Color(1, 1, 1, 0.5f);
                    }
                    else
                    {
                        altarParts[i].color = new Color(1, 1, 1, 1f);
                    }
                }
                else
                {
                    altarParts[i].color = new Color(0, 0, 0, 0f);
                }
            }
        }
    }
}

