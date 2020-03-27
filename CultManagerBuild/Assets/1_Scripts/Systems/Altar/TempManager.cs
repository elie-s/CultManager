using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class TempManager : MonoBehaviour
    {
        public int currentResource;
        public int currentCultists;
        public bool altarComplete;

        public GameObject altarCenter;
        public List<AltarPartBehavior> AltarParts;

        void Start()
        {
            GatherChildren(gameObject, AltarParts);
            altarCenter.SetActive(altarComplete);
        }

        void Update()
        {

        }

        public void CheckPillarProgress()
        {
            int ctr = 0;
            for (int i = 0; i < AltarParts.Count; i++)
            {
                if (AltarParts[i].buildProgress == 1)
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
    }
}

