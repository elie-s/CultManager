using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0414
namespace CultManager
{
    public class PoliceManager : MonoBehaviour
    {
        [SerializeField] private CultManager cult = default;
        [SerializeField] private MoneyManager money = default;
        [SerializeField] private InfluenceManager influence = default;
        [SerializeField] private UIGeneralManager uiManager = default;

        [SerializeField] private ModifierReference reference = default;


        public CultData cultData = default;
        public PoliceData data = default;
        public int value => data.value;
        public int investigationLevel = default;

        public int investigatorCount = default;
        public int moneyDeduction = default;
        public int influenceDeduction = default;

        private System.DateTime nextHourTime;


        private void Start()
        {
            nextHourTime = System.DateTime.Now;
        }

        public void InitAysnchValues()
        {
            System.TimeSpan timeSpan = System.DateTime.Now - data.lastHourReference;
            int numberOfHours = (int)(timeSpan.Hours);
            ChargePenalty(numberOfHours);
            DecreasePoliceValue(numberOfHours);
        }

        public void Incerment(int _value)
        {
            float temp = _value /** (1 + reference.storage.PoliceIncrementModifier)*/;
            data.Increment(Mathf.RoundToInt(temp));
            uiManager?.UpdateDisplayer();
        }

        public void Decrement(int _value)
        {
            float temp = _value /** (1 + reference.storage.PoliceDecrementModifier)*/;
            data.Decrement(Mathf.RoundToInt(temp));
            uiManager?.UpdateDisplayer();
        }

        public void Set(int _value)
        {
            data.Set(_value);
            uiManager?.UpdateDisplayer();
        }

        public void ResetData()
        {
            data.Reset(1000);
            uiManager?.UpdateDisplayer();
        }

        public void ResetCult(int level)
        {
            data.ResetPoliceData(level);
            uiManager?.UpdateDisplayer();
        }

        private void Update()
        {
            LevelUpdate();
            HourCheck();
            investigatorCount = GatherInvestigators();
        }

        public void LevelUpdate()
        {
            if (data.ratio >= 0f && data.ratio < 0.3f)
            {
                investigationLevel = 0;
                StopInfiltration();
            }
            else if (data.ratio >= 0.3f && data.ratio < 0.5f)
            {
                investigationLevel = 1;
                StartInfiltration();
            }
            else if (data.ratio >= 0.5f && data.ratio < 0.8f)
            {
                investigationLevel = 2;
                StartInfiltration();
            }
            else if (data.ratio >= 0.8f && data.ratio < 1f)
            {
                investigationLevel = 3;
                StartInfiltration();
            }
            else if (data.ratio >= 1f)
            {
                investigationLevel = 4;
                StartInfiltration();
            }
        }

        public void HourCheck()
        {
            if (System.DateTime.Now > nextHourTime)
            {
                data.lastHourReference = System.DateTime.Now;
                nextHourTime = System.DateTime.Now+System.TimeSpan.FromMinutes(1f);
                ChargePenalty(1);
                uiManager?.UpdateDisplayer();
            }
        }

        public void ChargePenalty(int hours)
        {
            switch (investigationLevel)
            {
                case 2:
                    {
                        DeductMoney(hours);
                    }
                    break;
                case 3:
                    {
                        DeductMoney(hours);
                        DeductInfluence(hours);
                    }
                    break;
                case 4:
                    {
                        DeductMoney(hours);
                        DeductInfluence(hours);
                        CultSiezed();
                    }
                    break;
            }
        }

        public int GatherInvestigators()
        {
            int ctr = 0;
            for (int i = 0; i < cultData.cultists.Count; i++)
            {
                if (cultData.cultists[i].isInvestigator)
                {
                    ctr++;
                }
            }
            return ctr;
        }

        public void StartInfiltration()
        {
            cult.allowInfiltration = true;
        }

        public void StopInfiltration()
        {
            cult.allowInfiltration = false;
        }

        public void DeductMoney(int hours)
        {
            int penalty = moneyDeduction * investigatorCount * hours;
            if (money.value >= penalty)
            {
                money.Decrease(penalty, 0);
            }
            else
            {
                money.ResetValue(0, 0);
            }
        }

        public void DeductInfluence(int hours)
        {
            int penalty = influenceDeduction * investigatorCount * hours;
            if (influence.value >= penalty)
            {
                influence.Decrease(penalty);
            }
            else
            {
                influence.ResetValue(0);
            }
        }

        public void CultSiezed()
        {
            Debug.Log("Cult Siezed");
        }


        public void DecreasePoliceValue(int hours)
        {
            Decrement(hours*8);
        }

    }
}

