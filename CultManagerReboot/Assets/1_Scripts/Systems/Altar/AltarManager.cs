using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class AltarManager : MonoBehaviour
    {
        [Header("Altar Data")]
        [SerializeField] private AltarData altarData = default;
        [SerializeField] private AltarPartBehavior[] altarPartBehaviors;

        [Header("Cult Parameters")]
        [SerializeField] private CultData cult = default;
        [SerializeField] private MoneyManager moneyManager;

        public IntGauge assignedCultists;

        List<GameObject> altarParts;

        void Start()
        {
            altarParts = gameObject.GatherAllChildrenInList();
            altarPartBehaviors = gameObject.GatherBehaviorInArray<AltarPartBehavior>();
            altarData.SetAvailableCultists(cult.cultists.Count);
            assignedCultists = new IntGauge(0, altarData.availableCultists);
        }

        void Update()
        {
            altarData.SetAvailableCultists(cult.cultists.Count);
            assignedCultists.SetMax(altarData.availableCultists);
        }


        public void Buy(int _amount)
        {
            moneyManager.Decrease(_amount);
        }

        public void AltarCompletion()
        {
            int ctr = 0;
            for (int i = 0; i < altarPartBehaviors.Length; i++)
            {
                if (altarPartBehaviors[i].altarPart.currentBuildPoints.ratio == 1)
                {
                    ctr++;
                }
            }
            altarData.altarCompletion = (ctr == altarPartBehaviors.Length);

        }

        public int AssignWorkers(int _amountAsked)
        {
            int result = 0;
            
            /*if (_amountAsked <= assignedCultists.amountLeft) result = _amountAsked;
            else result = assignedCultists.amountLeft;*/

            for (int i = 0; i < assignedCultists.amountLeft; i++)
            {
                if (result < _amountAsked)
                {
                    if (!cult.cultists[i].occupied)
                    {
                        Debug.Log("Result " + result);
                        cult.cultists[i].occupied = true;
                        assignedCultists.Increment(1);
                        result++;
                    }
                }
                else
                {
                    break;
                }
            }
            //assignedCultists.Increment(result);
            //Debug.Log("Result "+result);
            return result;
        }

        public void UnassignWorkers(int _amount)
        {
            //assignedCultists.Decrement(_amount);
            int ctr = 0;
            for (int i = 0; i < assignedCultists.amountLeft; i++)
            {
                if (ctr < _amount)
                {
                    if (cult.cultists[i].occupied)
                    {
                        cult.cultists[i].occupied = false;
                        assignedCultists.Decrement(1);
                        ctr++;
                    }
                }
                else
                {
                    break;
                }
            }
        }



    }
}

