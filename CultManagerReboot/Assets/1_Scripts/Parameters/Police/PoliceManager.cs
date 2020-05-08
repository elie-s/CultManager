using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class PoliceManager : MonoBehaviour
    {
        [SerializeField] private CultManager cult;
        [SerializeField] private MoneyManager money;
        [SerializeField] private InfluenceManager influence;

        public CultData cultData;
        public PoliceData data;
        public int investigationLevel;

        public int invetigatorCount;
        public int moneyDeduction;
        public int influenceDeduction;

        private System.DateTime nextHourTime;

        private void Start()
        {
            nextHourTime = System.DateTime.Now;
        }

        public void InitAysnchValues()
        {
            System.TimeSpan timeSpan = System.DateTime.Now - data.lastHourReference;
            int numberOfHours = (int)(timeSpan.Minutes);
            Debug.Log(numberOfHours);
            ChargePenalty(numberOfHours);
        }

        public void Incerment(int _value)
        {
            data.Increment(_value);
        }

        public void Decrement(int _value)
        {
            data.Decrement(_value);
        }

        public void Set(int _value)
        {
            data.Set(_value);
        }

        public void ResetData()
        {
            data.Reset(1000);
        }

        public void ResetCult(int level)
        {
            int max = 1000;
            data.ResetPoliceData(max);
        }

        private void Update()
        {
            LevelUpdate();
            HourCheck();
            invetigatorCount = GatherInvestigators();
            Debug.Log(data.ratio);
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
                Debug.Log(nextHourTime);
                ChargePenalty(1);
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
            int penalty = moneyDeduction * invetigatorCount * hours;
            if (money.value >= penalty)
            {
                money.Decrease(penalty);
            }
            else
            {
                money.ResetValue(0);
            }
        }

        public void DeductInfluence(int hours)
        {
            int penalty = influenceDeduction * invetigatorCount * hours;
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

    }
}

