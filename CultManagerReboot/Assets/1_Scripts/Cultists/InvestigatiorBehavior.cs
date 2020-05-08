using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class InvestigatiorBehavior : MonoBehaviour
    {
        [SerializeField] private int frequency;
        [SerializeField] private int chanceOfInspection=25;
        [SerializeField] private int durationOfInspection=3;
        [SerializeField]private float timeToInspect;
        [SerializeField]bool isInspecting;

        private void Update()
        {
            FrequencyCheck();
        }


        public void FrequencyCheck()
        {
            if (Time.time > timeToInspect)
            {
                timeToInspect += frequency;
                if (CalculateChance())
                {
                    if (!isInspecting)
                    {
                        StartCoroutine(InspectElements());
                    } 
                }
            }
        }

        public bool CalculateChance()
        {
            return ((int)Random.Range(1, 100) <= chanceOfInspection);
        }

        IEnumerator InspectElements()
        {
            isInspecting = true;
            yield return new WaitForSeconds(durationOfInspection);
            isInspecting = false;
            StopCoroutine(InspectElements());
        }
    }
}

