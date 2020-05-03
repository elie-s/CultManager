using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class AltarManager : MonoBehaviour
    {
        [Header("Altar Data")]
        [SerializeField] private AltarData altarData = default;
        [SerializeField] private AltarPartData[] altarPartDatas = default;
        

        [Header("Cult Parameters")]
        [SerializeField] private CultData cult = default;
        [SerializeField] private MoneyManager moneyManager;
        [SerializeField] private PuzzeManager puzzleManager;

        [Header("New Addition")]
        [SerializeField] public GameObject altarPartPrefab;

        public IntGauge assignedCultists;


        public void ResetCult(int level)
        {
            altarData.ResetAltarData();
            CreateNewAltarParts(altarPartDatas);
        }

        public void ResetData()
        {
            altarData.ResetAltarData();
            CreateNewAltarParts(altarPartDatas);
        }

        void Start()
        {
            altarData.SetAvailableCultists(cult.cultists.Count);
            assignedCultists = new IntGauge(0, altarData.availableCultists,false);
        }

        void Update()
        {
            altarData.SetAvailableCultists(cult.cultists.Count);
            assignedCultists.SetMax(altarData.availableCultists);
        }

        public void CreateNewAltarParts(AltarPartData[] _altarPartDatas)
        {
            for (int i = 0; i < _altarPartDatas.Length; i++)
            {
                AltarPart current = altarData.CreateNewAltarPart(_altarPartDatas[i].name);
                GameObject instance = Instantiate(altarPartPrefab, transform.position, Quaternion.identity, transform);
                AltarPartBehavior behavior = instance.GetComponent<AltarPartBehavior>();
                behavior.Spawn(current, this, _altarPartDatas[i].maxCultists, _altarPartDatas[i].maxBuildPoints);
                altarData.AddAltarPart(current);
            } 
        }

        public void InitAltarParts()
        {
            for (int i = 0; i < altarData.altarParts.Count; i++)
            {
                AltarPart current = altarData.altarParts[i];
                GameObject instance = Instantiate(altarPartPrefab, transform.position, Quaternion.identity, transform);
                instance.GetComponent<AltarPartBehavior>().Init(current, this);
            }
        }

        public System.DateTime ReturnLastTimeReference()
        {
            return altarData.lastBuildTimeReference;
        }

        public void ResetTimeReference()
        {
            altarData.ResetBuildTimeReference();
        }


        public bool isComplete()
        {
            return (altarData.altarCompletion);
        }

        public void Buy(AltarPart _altar)
        {
            int cost = 0;
            for (int i = 0; i < altarPartDatas.Length; i++)
            {
                if (_altar.altarPartName.Equals(altarPartDatas[i].name))
                {
                    cost = altarPartDatas[i].cost;
                }
            }
            if (cost > 0)
            {
                if (moneyManager.value >= cost)
                {
                    moneyManager.Decrease(cost);
                    _altar.Buy();
                }
            }
        }

        public AltarPartData ReturnAltarPartData(AltarPart _altar)
        {
            AltarPartData result = ScriptableObject.CreateInstance<AltarPartData>();
            for (int i = 0; i < altarPartDatas.Length; i++)
            {
                if (_altar.altarPartName.Equals(altarPartDatas[i].name))
                {
                    result = altarPartDatas[i];
                }
            }
            return result;
        }

        public void AltarCompletion()
        {
            int ctr = 0;
            for (int i = 0; i < altarData.altarParts.Count; i++)
            {
                if (altarData.altarParts[i].currentBuildPoints.ratio == 1)
                {
                    ctr++;
                }
            }
            altarData.altarCompletion = (ctr == altarData.altarParts.Count);

            if (altarData.altarCompletion)
            {
                puzzleManager.CompletedAltar();
            }

        }

        public int AssignWorkers(int _amountAsked)
        {
            int result = 0;
            
            for (int i = 0; i < altarData.availableCultists; i++)
            {
                if (result < _amountAsked)
                {
                    if (!cult.cultists[i].occupied)
                    {
                        cult.cultists[i].ToggleOccupy();
                        assignedCultists.Increment(1);
                        result++;
                    }
                }
            }
            
            return result;
            
        }

        public void UnassignWorkers(int _amount)
        {
            int ctr = 0;
            for (int i = 0; i < altarData.availableCultists; i++)
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
            }
        }



    }
}

