using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class AltarManager : MonoBehaviour
    {
        [SerializeField] private CultData cultData = default;
        [SerializeField] private MoneyData moneyData = default;
        [SerializeField] private MoneyManager moneyManager = default;

        public bool altarComplete;

        public GameObject altarCenter;
        public List<AltarPartBehavior> AltarParts;

        public int currentResource => (int)moneyData.value;
        public int currentCultists => cultData.cultists.Count;
        public IntGauge assignedCultists;

        void Start()
        {
            GatherChildren(gameObject, AltarParts);
            altarCenter.SetActive(altarComplete);
            assignedCultists = new IntGauge(0, cultData.cultists.Count, false);
        }

        void Update()
        {
            assignedCultists.SetMax(cultData.cultists.Count);
        }

        public void CheckPillarProgress()
        {
            int ctr = 0;
            for (int i = 0; i < AltarParts.Count; i++)
            {
                if (AltarParts[i].currentBuildPoints.ratio == 1)
                {
                    ctr++;
                }
            }
            if (ctr == AltarParts.Count)
            {
                altarComplete = true;
                altarCenter.SetActive(altarComplete);
            }
        }

        void GatherChildren(GameObject parent, List<AltarPartBehavior> AltarPartList)
        {
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                if (parent.transform.GetChild(i).gameObject.GetComponent<AltarPartBehavior>())
                {
                    AltarPartBehavior current= (parent.transform.GetChild(i).gameObject.GetComponent<AltarPartBehavior>());
                    if (current.isActiveAndEnabled)
                    {
                        AltarPartList.Add(current);
                    }
                } 
            }
        }

        public int AssignWorkers(int _amountAsked)
        {
            int result = 0;

            if (_amountAsked <= assignedCultists.amountLeft) result = _amountAsked;
            else result = assignedCultists.amountLeft;

            assignedCultists.Increment(result);

            return result;
        }

        public void UnassignWorkers(int _amount)
        {
            assignedCultists.Increment(-_amount);
        }

        public void Buy(int _amount)
        {
            moneyManager.Decrease(_amount);
        }
    }
}

