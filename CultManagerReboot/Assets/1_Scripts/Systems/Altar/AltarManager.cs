using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;


namespace CultManager
{
    public class AltarManager : MonoBehaviour
    {
        [Header("Altar Data")]
        [SerializeField] private AltarData altarData = default;
        [SerializeField] private AltarPartSet[] altarPartSets = default;
        [SerializeField] private AltarDisplay display;
        private AltarPartSet currentAltarPartSet;
        

        [Header("Cult Parameters")]
        [SerializeField] private CultData cult = default;
        [SerializeField] private MoneyManager moneyManager;
        [SerializeField] private PuzzeManager puzzleManager;
        [SerializeField] private BloodBankUIDisplay bloodBankUI;
        [SerializeField] private CameraController controller;

        [Header("New Addition")]
        [SerializeField] public GameObject altarPartPrefab;
        

        public IntGauge assignedCultists;
        public int[] workPower;

        public UnityEvent OnCompletion;



        public void ResetCult(int level)
        {
            altarData.ResetAltarData();
            currentAltarPartSet = altarPartSets[level - 1];
            CreateNewAltarParts(altarPartSets[level-1].altarPartDatas);
        }

        public void ResetData()
        {
            altarData.ResetAltarData();
            currentAltarPartSet = altarPartSets[0];
            CreateNewAltarParts(currentAltarPartSet.altarPartDatas);
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
            if (altarData.altarCompletion)
            {
                OnCompletion.Invoke();
            }
        }

        public void CreateNewAltarParts(AltarPartData[] _altarPartDatas)
        {
            workPower = new int[_altarPartDatas.Length];
            for (int i = 0; i < _altarPartDatas.Length; i++)
            {
                AltarPart current = altarData.CreateNewAltarPart(_altarPartDatas[i].name);
                GameObject instance = Instantiate(altarPartPrefab, transform.position, Quaternion.identity, transform);
                AltarPartBehavior behavior = instance.GetComponent<AltarPartBehavior>();
                behavior.Spawn(current, this, _altarPartDatas[i].maxCultists, _altarPartDatas[i].maxBuildPoints);
                altarData.AddAltarPart(current);
            }
            display.Spawn(_altarPartDatas);
        }
        [ContextMenu("Break Random Altar Part")]
        public void BreakAltarPart()
        {
            int i = Random.Range(0, altarData.altarParts.Count);
            altarData.BreakAltarPart(altarData.altarParts[i]);
        }

        public void BreakAltarPart(AltarPart part)
        {
            altarData.BreakAltarPart(part);
        }

        public void DemonSummon()
        {
            puzzleManager.CompletedAltar();
            controller.Transition(4);
            bloodBankUI.Open();
        }

        public void InitAltarParts()
        {
            workPower = new int[altarData.altarParts.Count];
            for (int i = 0; i < altarData.altarParts.Count; i++)
            {
                AltarPart current = altarData.altarParts[i];
                GameObject instance = Instantiate(altarPartPrefab, transform.position, Quaternion.identity, transform);
                instance.GetComponent<AltarPartBehavior>().Init(current, this);
            }
            currentAltarPartSet = altarPartSets[GameManager.currentLevel-1];
            display.Spawn(currentAltarPartSet.altarPartDatas);
        }

        public void UpdateWorkPower(int i,int value)
        {
            workPower[i] = value;
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
            for (int i = 0; i < currentAltarPartSet.altarPartDatas.Length; i++)
            {
                if (_altar.altarPartName.Equals(currentAltarPartSet.altarPartDatas[i].name))
                {
                    cost = currentAltarPartSet.altarPartDatas[i].cost;
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
            for (int i = 0; i < currentAltarPartSet.altarPartDatas.Length; i++)
            {
                if (_altar.altarPartName.Equals(currentAltarPartSet.altarPartDatas[i].name))
                {
                    result = currentAltarPartSet.altarPartDatas[i];
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

        }

        public void AddCultistsToAltar(AltarPart part)
        {
            if (assignedCultists.amountLeft >= 1 && part.assignedCultists.amountLeft >= 1)
            {
                AssignWorkers(1);
                part.assignedCultists.Increment(1);
            }
        }

        public void RemoveCultistsFromAltar(AltarPart part)
        {
            Debug.Log(part);
            if (assignedCultists.max > assignedCultists.value && part.assignedCultists.value >= 1)
            {
                UnassignWorkers(1);
                part.assignedCultists.Decrement(1);
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

