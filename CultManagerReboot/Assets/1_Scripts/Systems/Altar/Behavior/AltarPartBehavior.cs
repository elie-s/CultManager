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
        public AltarPartData altarPartData;
        public AltarData altarData;
        public AltarPart altarPart => altarData.altarParts[altarPartData.altarPartIndex];
            

        [Header("Cult Parameters")]
        [SerializeField] private CultData cult = default;
        [SerializeField] private MoneyData money = default;

        private bool isBuilding;

        private void Start()
        {
            if (!SaveManager.saveLoaded)
            {
                altarPart.Init(0, altarPartData.maxBuildPoints, false);
            }
            else
            {
                UpdateBuildProgress();
            }
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

        public void UpdateBuildProgress()
        {
            System.TimeSpan timeSpan = System.DateTime.Now - altarData.lastBuildTimeReference;
            int buildPointsToAdd = Mathf.FloorToInt((int)timeSpan.TotalSeconds *altarPart.assignedCultists);
            altarPart.IncrementBuildPoints(buildPointsToAdd);
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
                        //Debug.Log("Building");
                        if (altarPart.currentBuildPoints.ratio < 1f)
                        {
                            StartCoroutine(SimulateBuilding(altarPart.assignedCultists));
                            altarData.ResetBuildTimeReference();
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
            altarPart.IncreaseAssignedCultists(altarManager.AssignWorkers(altarPartData.maxCultists - altarPart.assignedCultists));
            Debug.Log((altarPartData.maxCultists - altarPart.assignedCultists));
        }

        IEnumerator SimulateBuilding(int cultistsNum)
        {
            isBuilding = true;
            altarPart.IncrementBuildPoints(cultistsNum);
            yield return new WaitForSecondsRealtime(1);
            isBuilding = false;
        }

        public void Test()
        {
            Debug.Log("SOXCESS");
        }
    }
}

