﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class InvestigatiorBehavior : MonoBehaviour
    {
        [SerializeField] private GameObject photographyPop;
        [SerializeField] private GameObject notesPop;
        [SerializeField] private CultistAnimationBehaviour cultistBehavior;
        [SerializeField] private SpriteRenderer sr;

        [SerializeField] private int frequency;
        [SerializeField] private int chanceOfInspection=25;
        [SerializeField] private int durationOfInspection=3;
        [SerializeField]private float timeToInspect;
        [SerializeField]bool isInspecting;
        [SerializeField] bool detected;

        private void Update()
        {
            detected = cultistBehavior.cultist.detected;
            if (cultistBehavior.cultist.isInvestigator)
            {
                if (!cultistBehavior.cultist.detected)
                {
                    FrequencyCheck();
                    sr.color = Color.white;
                }
                else
                {
                    photographyPop.SetActive(false);
                    notesPop.SetActive(false);
                    sr.color = Color.red;
                }
            }
            
        }


        public void FrequencyCheck()
        {
            if (Time.time > timeToInspect)
            {
                timeToInspect += frequency;
                StartCoroutine(InspectElements());
            }
        }

        public bool CalculateChance(int chanceRate)
        {
            return ((int)Random.Range(1, 100) <= chanceRate);
        }

        IEnumerator InspectElements()
        {
            isInspecting = true;
            if (CalculateChance(50))
            {
                photographyPop.SetActive(true);
            }
            else
            {
                notesPop.SetActive(true);
            }
            yield return new WaitForSeconds(durationOfInspection);
            isInspecting = false;
            TurnOffPopUps();
            StopCoroutine(InspectElements());
        }

        void TurnOffPopUps()
        {
            photographyPop.SetActive(false);
            notesPop.SetActive(false);
        }

        public void Detected()
        {
            if (isInspecting)
            {
                cultistBehavior.cultist.detected = true;
            }
        }
    }
}

