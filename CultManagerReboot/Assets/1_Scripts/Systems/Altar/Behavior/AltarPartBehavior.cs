using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class AltarPartBehavior : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] AltarManager altarManager = default;

        [Header("Altar Data")]
        public int index;
        public AltarPartData altarPartData;
        public AltarData altarData;
        public AltarPart altarPart => altarData.altarParts[index];
            

        [Header("Cult Parameters")]
        [SerializeField] private CultData cult = default;
        [SerializeField] private MoneyData money = default;

        private bool isBuilding;


        [ContextMenu("BuyAltarPart")]
        public void BuyButton()
        {
            if (money.value >= altarPartData.cost)
            {
                altarManager.Buy(altarPartData.cost);
                altarPart.isBought=true;
            }
        }

        private void Update()
        {
            if (altarPart.isBought)
            {
                if (altarPart.currentBuildPoints.ratio < 1f)
                {
                    AssignCultists();
                    if (!isBuilding)
                    {
                        if (altarPart.currentBuildPoints.ratio < 1f)
                        {
                            StartCoroutine(SimulateBuilding(altarPart.assignedCultists));
                        }
                    }
                }
                else
                {
                    altarManager.AltarCompletion();
                    altarManager.UnassignWorkers(altarPart.assignedCultists);
                    altarPart.Reset();
                }
            }
        }

        void AssignCultists()
        {
            Debug.Log(altarPart.assignedCultists);
            altarPart.Reset(altarManager.AssignWorkers(altarPartData.maxCultists - altarPart.assignedCultists)); 
        }

        IEnumerator SimulateBuilding(int a)
        {
            isBuilding = true;
            altarPart.currentBuildPoints.Increment(1);
            yield return new WaitForSecondsRealtime(1);
            isBuilding = false;
        }

    }
}

