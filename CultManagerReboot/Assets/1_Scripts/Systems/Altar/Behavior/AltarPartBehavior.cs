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

        private void Start()
        {
            altarPart.Init(0, altarPartData.maxBuildPoints, false);
        }

        [ContextMenu("BuyAltarPart")]
        public void BuyButton()
        {
            if (money.value >= altarPartData.cost)
            {
                altarManager.Buy(altarPartData.cost);
                altarPart.Buy();
            }
        }

        private void Update()
        {
            Debug.Log("Build Points are " + altarPart.currentBuildPoints.value);
            Debug.Log("Max Build Points are " + altarPart.currentBuildPoints.max);
            if (altarPart.isBought)
            {
                if (altarPart.currentBuildPoints.ratio < 1f)
                {
                    AssignCultists();
                    if (!isBuilding)
                    {
                        //Debug.Log("Building");
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
                    altarPart.ResetAssignedCultists();
                }
            }
        }

        void AssignCultists()
        {
            Debug.Log(altarPart.assignedCultists);
            altarPart.IncreaseAssignedCultists(altarManager.AssignWorkers(altarPartData.maxCultists - altarPart.assignedCultists)); 
        }

        IEnumerator SimulateBuilding(int cultistsNum)
        {
            isBuilding = true;
            altarPart.IncrementBuildPoints(cultistsNum);
            yield return new WaitForSecondsRealtime(1);
            isBuilding = false;
        }

    }
}

