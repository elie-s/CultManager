using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class BloodBankManager : MonoBehaviour
    {
        [SerializeField] private BloodBankData data = default;
        [SerializeField] private BloodBank[] tempBanks;
        [SerializeField] private int tempA, tempB, tempO;

        private void Start()
        {
            SetUpTempBloodBanks();
        }

        public void SetUpTempBloodBanks()
        {
            tempBanks = new BloodBank[3];
            IntGauge[] intGauge = new IntGauge[3];
            for (int i = 0; i < tempBanks.Length; i++)
            {
                tempBanks[i] = new BloodBank((BloodType)i, intGauge[i]);
                tempBanks[i].gauge = new IntGauge(0, 100, false);
            }
        }

        public void IncreaseTempBanks(BloodType bloodType, int amount)
        {
            for (int i = 0; i < tempBanks.Length; i++)
            {
                if (bloodType != BloodType.AB)
                {
                    if (tempBanks[i].bloodGroup == bloodType)
                    {
                        tempBanks[i].IncrementGauge(amount);
                    }
                }
                else
                {
                    if (tempBanks[i].bloodGroup == BloodType.A || tempBanks[i].bloodGroup == BloodType.B)
                    {
                        tempBanks[i].IncrementGauge(amount / 2);
                    }
                }
            }
        }

        public void DecreaseTempBanks(BloodType bloodType, int amount)
        {
            for (int i = 0; i < tempBanks.Length; i++)
            {
                if (bloodType != BloodType.AB)
                {
                    if (tempBanks[i].bloodGroup == bloodType)
                    {
                        tempBanks[i].DecrementGauge(amount);
                    }
                }
                else
                {
                    if (tempBanks[i].bloodGroup == BloodType.A || tempBanks[i].bloodGroup == BloodType.B)
                    {
                        tempBanks[i].DecrementGauge(amount / 2);
                    }
                }
            }
        }

        public void EmptyTempBanks()
        {
            for (int i = 0; i < tempBanks.Length; i++)
            {
                IncreaseBloodOfType(tempBanks[i].bloodGroup, tempBanks[i].gauge.value);
            }
        }

        

        public void IncreaseBloodOfType(BloodType blood,int amount)
        {
            data.Increase(blood, amount);
            DecreaseTempBanks(blood, amount);
        }

        public void DecreaseBloodOfType(BloodType blood, int amount)
        {
            data.Decrease(blood, amount);
            IncreaseTempBanks(blood, amount);
        }

        public bool CanDecrease(BloodType blood, int amount)
        {
            return data.CanDecrease(blood, amount);
        }

        public bool CanIncrease(BloodType blood, int amount)
        {
            return data.CanIncrease(blood, amount);
        }

        public void FailedPattern()
        {
            EmptyTempBanks();
        }

        void AddTempBlood(BloodType type, int amt)
        {
            switch (type)
            {
                case BloodType.O:
                    {
                        tempO += amt;
                    }
                    break;
                case BloodType.A:
                    {
                        tempA += amt;
                    }
                    break;
                case BloodType.B:
                    {
                        tempB += amt;
                    }
                    break;
                case BloodType.AB:
                    {
                        tempA += amt/2;
                        tempB += amt/2;
                    }
                    break;
                default:
                    break;
            }
        }


    }
}

