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
        public AltarPart altarPart;
        public int workPower;

        private bool isBuilding;

        private void Start()
        {
        }


        public void Spawn(AltarPart _altar, AltarManager _manager, int _maxCultists, int _maxBuildPoints)
        {
            altarPart = _altar;
            altarManager = _manager;
            altarPart.InitBuildPoints(0, _maxBuildPoints, false);
            altarPart.InitAssignedCultists(0, _maxCultists, false);
        }


        public void Init(AltarPart _altar, AltarManager _manager)
        {
            altarPart = _altar;
            altarManager = _manager;
            if (altarPart.isBought)
                UpdateBuildProgress();
        }

        public void UpdateBuildProgress()
        {
            System.TimeSpan timeSpan = System.DateTime.Now - altarManager.ReturnLastTimeReference();
            int buildPointsToAdd = Mathf.FloorToInt((int)timeSpan.TotalSeconds * altarPart.assignedCultists.value);
            altarPart.IncrementBuildPoints(buildPointsToAdd);
        }

        private void Update()
        {
            if (altarPart.isBought)
            {
                UpdateWorkPower();
                if (altarPart.currentBuildPoints.ratio < 1f)
                {
                    if (altarPart.assignedCultists.value >= 1)
                    {
                        if (!isBuilding)
                        {
                            StartCoroutine(SimulateBuilding(workPower));
                            altarManager.ResetTimeReference();
                        }
                    }
                    /*AssignCultists();
                    if (!isBuilding)
                    {
                        //Debug.Log("Building");
                        if (altarPart.currentBuildPoints.ratio < 1f)
                        {
                            StartCoroutine(SimulateBuilding(altarPart.assignedCultists.value));
                            altarManager.ResetTimeReference();
                        }
                    }*/
                }
                else
                {
                    altarManager.AltarCompletion();
                    altarManager.UnassignWorkers(altarPart.assignedCultists.value);
                    altarPart.SetAssignedCultists();
                }
            }
        }

        void UpdateWorkPower()
        {
            if (altarPart.assignedCultists.ratio == 1)
            {
                workPower = (int)(altarPart.assignedCultists.value * 1.5f);
            }
            else
            {
                workPower = altarPart.assignedCultists.value;
            }

            if (altarManager.assignedCultists.value < altarPart.assignedCultists.value)
            {
                int difference = altarPart.assignedCultists.value - altarManager.assignedCultists.value;
                altarManager.UnassignWorkers(difference);
                altarPart.assignedCultists.Decrement(difference);
            }

            altarManager.UpdateWorkPower(transform.GetSiblingIndex(), workPower);
        }

        void AssignCultists()
        {
            altarPart.IncrementAssignedCultists(altarManager.AssignWorkers(altarPart.assignedCultists.amountLeft));
            //Debug.Log(altarPart.assignedCultists.amountLeft);
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

