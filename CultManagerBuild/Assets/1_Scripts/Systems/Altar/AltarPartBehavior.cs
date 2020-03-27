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
        AltarPartBehavior altarPartBehavior;

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
            objectInteraction = gameObject.GetComponent<ObjectInteraction>();
            SetReferences();
        }

        void SetReferences()
        {
            altarPartBehavior = gameObject.GetComponent<AltarPartBehavior>();
            altarPartUI.altarPartBehavior = altarPartBehavior;
            altarPartUI.DisplayData();
        }

        void Update()
        {
            UpdateObjectState();
            if (isBought)
            {
                AssignCultists();
                if (currentAssignedCultists > 0/* && !isBuilding*/)
                {
                    if (currentBuildPoints < currentAltarPartData.maxBuildPoints)
                    {
                        StartCoroutine(SimulateBuilding(currentAssignedCultists));
                    }
                    else
                    {
                        tempManager.CheckPillarProgress();
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
                altarPartUI.gameObject.SetActive(currentObjectState == ObjectState.Selected ? true : false);
                SetReferences();
                altarPartUI.DisplayData();
            }
            else
            {
                altarPartUI.gameObject.SetActive(ObjectInteraction.isSelected);
            }
        }

        public void BuyBuilding(AltarPartBehavior instance)
        {
            if (instance == altarPartBehavior)
            {
                if (tempManager.currentResource >= currentAltarPartData.requiredResource)
                {
                    tempManager.currentResource -= currentAltarPartData.requiredResource;
                    isBought = true;
                }
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
            yield return new WaitForSecondsRealtime( 1 / (currentAltarPartData.rateOfBuildingPerUnit*a));
            isBuilding = false;
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

