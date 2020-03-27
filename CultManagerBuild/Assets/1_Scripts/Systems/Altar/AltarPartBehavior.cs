using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class AltarPartBehavior : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] TempManager tempManager;
        [SerializeField] AltarPartUI altarPartUI;

        [Header("Active state")]
        public ObjectState currentObjectState;
        public bool isBought;
        public float buildProgress;

        [Header("Altar Part Stats")]
        public AltarPartData currentAltarPartData;
        public float currentBuildPoints;
        public int currentAssignedCultists;
        ObjectInteraction objectInteraction;

        private bool isBuilding;

        void Start()
        {
            SetReferences();
        }

        void SetReferences()
        {
            objectInteraction = gameObject.GetComponent<ObjectInteraction>();
            altarPartUI.altarPartBehavior = gameObject.GetComponent<AltarPartBehavior>();
            altarPartUI.DisplayData();
        }

        void Update()
        {
            UpdateObjectState();
            if (isBought)
            {
                AssignCultists();
                if (currentAssignedCultists > 0 && !isBuilding)
                {
                    if (currentBuildPoints < currentAltarPartData.maxBuildPoints)
                    {
                        StartCoroutine(SimulateBuilding(currentAssignedCultists));
                    }
                }
                ComputeRateOfBuilding();
            }
        }

        void UpdateObjectState()
        {
            currentObjectState = objectInteraction.selectionState;
            if (currentObjectState == ObjectState.Selected)
            {
                altarPartUI.gameObject.SetActive(true);
                altarPartUI.DisplayData();
            }
            else
            {
                altarPartUI.gameObject.SetActive(false);
            }
        }

        public void BuyBuilding()
        {
            if (tempManager.currentResource >= currentAltarPartData.requiredResource)
            {
                tempManager.currentResource -= currentAltarPartData.requiredResource;
                isBought = true;
            }

        }

        void AssignCultists()
        {
            for (int i = 0; i < (currentAltarPartData.maxUnitsForBuilding-currentAssignedCultists); i++)
            {
                if (tempManager.currentCultists > 0)
                {
                    tempManager.currentCultists--;
                    currentAssignedCultists++;
                }
                else
                {
                    break;
                }
            }
        }

        IEnumerator SimulateBuilding(int a)
        {
            isBuilding = true;
            currentBuildPoints += 1;
            yield return new WaitForSeconds(1 / (currentAltarPartData.rateOfBuildingPerUnit * a));
            isBuilding = false;
            StopCoroutine(SimulateBuilding(0));
        }

        /*IEnumerator SimulateBuilding2(int a)
        {
            isBuilding = true;
            currentBuildPoints += 1;
            yield return new WaitForSeconds(1 / (currentAltarPartData.rateOfBuildingPerUnit * a));
            isBuilding = false;
            StopCoroutine(SimulateBuilding(0));
        }*/

        void ComputeRateOfBuilding()
        {
            buildProgress = currentBuildPoints / currentAltarPartData.maxBuildPoints;
        }
    }
}

