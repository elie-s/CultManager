using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class BloodBankManager : MonoBehaviour
    {
        [SerializeField] private BloodBankUIDisplay display;
        [SerializeField] private BloodBankData data = default;
        [SerializeField] private BloodBank[] tempBanks;

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
                tempBanks[i].gauge = new IntGauge(0, data.bloodBanks[i].gauge.max, false);
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

        public void ResetTempBanks()
        {
            SetUpTempBloodBanks();
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



        [ContextMenu("Increase Blood")]
        public void AddBlood()
        {
            data.Increase(10);
        }

        public void FillUp()
        {
            data.FillAllBloodBanks();
        }

        public void InAdequateBloodOfType(BloodType blood)
        {
            display.InadequateBloodAnim(blood);
        }

        public void UseOfBloodOfType(BloodType blood)
        {
            display.BloodUtilizeAnim(blood);
        }


        public void ResetCult(int level)
        {
            data.ResetBloodBankData(level);
        }

        public void ResetData()
        {
            data.Reset();
        }

    }
}

