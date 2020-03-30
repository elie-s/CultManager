using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class AltarPartBehavior : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] AltarManager altarManager = default;
        [SerializeField] AltarPartUI altarPartUI = default;

        [Header("Active state")]
        public ObjectState currentObjectState;
        public bool isBought;

        [Header("Altar Part Stats")]
        public AltarPartData currentAltarPartData;
        public Gauge currentBuildPoints;
        public int currentAssignedCultists;
        ObjectInteraction objectInteraction;

        private bool isBuilding;

        void Start()
        {
            objectInteraction = gameObject.GetComponent<ObjectInteraction>();
            SetReferences();
            currentBuildPoints = new Gauge(0, currentAltarPartData.maxBuildPoints, false);
        }

        void SetReferences()
        {
            altarPartUI.altarPartBehavior = this;
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
                    if (currentBuildPoints.ratio < 1f)
                    {
                        StartCoroutine(SimulateBuilding(currentAssignedCultists));
                    }
                    else
                    {
                        altarManager.CheckPillarProgress();
                        altarManager.UnassignWorkers(currentAssignedCultists);
                        currentAssignedCultists = 0;
                    }
                }
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
            if (instance == this)
            {
                if (altarManager.currentResource >= currentAltarPartData.requiredResource)
                {
                    altarManager.Buy(currentAltarPartData.requiredResource);
                    isBought = true;
                }
            }
        }

        void AssignCultists()
        {
            currentAssignedCultists += altarManager.AssignWorkers(currentAltarPartData.maxUnitsForBuilding - currentAssignedCultists);
        }

        IEnumerator SimulateBuilding(int a)
        {
            isBuilding = true;
            currentBuildPoints.Increment((currentAltarPartData.rateOfBuildingPerUnit * a));
            yield return new WaitForSecondsRealtime( 1 );
            Debug.Log("Increment "+1 / (currentAltarPartData.rateOfBuildingPerUnit * a));
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

    }
}

