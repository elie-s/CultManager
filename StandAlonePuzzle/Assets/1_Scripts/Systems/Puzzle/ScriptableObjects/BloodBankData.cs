using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Puzzle/BloodBankData")]
    public class BloodBankData : ScriptableObject
    {
        public BloodBank[] bloodBanks = new BloodBank[3];
        public int level;

        public void SetBloodBanks(BloodBank[] _bloodBanks)
        {
            bloodBanks = _bloodBanks;
        }

        public void Reset()
        {
            bloodBanks = new BloodBank[3];
            IntGauge[] intGauge = new IntGauge[3];
            for (int i = 0; i < bloodBanks.Length; i++)
            {
                bloodBanks[i] = new BloodBank((BloodType)i, intGauge[i]);
                bloodBanks[i].gauge = new IntGauge(0, 100, false);
            }
        }

        public void FillAllBloodBanks()
        {
            for (int i = 0; i < bloodBanks.Length; i++)
            {
                bloodBanks[i].IncrementGauge(bloodBanks[i].gauge.max);
            }
        }

        public void ResetBloodBankData(int level)
        {
            int max = 0;
            switch (level)
            {
                case 1:
                    {
                        max = 100;
                    }
                    break;
                case 2:
                    {
                        max = 100;
                    }
                    break;
                case 3:
                    {
                        max = 100;
                    }
                    break;
                case 4:
                    {
                        max = 100;
                    }
                    break;
                case 5:
                    {
                        max = 100;
                    }
                    break;
                default:
                    {
                        max = 100;
                    }
                    break;
            }
            bloodBanks = new BloodBank[3];
            IntGauge[] intGauge = new IntGauge[3];
            for (int i = 0; i < bloodBanks.Length; i++)
            {
                bloodBanks[i] = new BloodBank((BloodType)i, intGauge[i]);
                bloodBanks[i].gauge = new IntGauge(0, max, false);
            }
        }

        public bool CanDecrease(BloodType bloodType, int amount)
        {
            int usage = 0;
            for (int i = 0; i < bloodBanks.Length; i++)
            {
                if (bloodType != BloodType.AB)
                {
                    if (bloodBanks[i].bloodGroup == bloodType)
                    {
                        if (bloodBanks[i].gauge.value >= amount)
                        {
                            usage += amount;
                        }
                        
                    }
                }
                else
                {
                    if (bloodBanks[i].bloodGroup == BloodType.A|| bloodBanks[i].bloodGroup == BloodType.B)
                    {
                        if (bloodBanks[i].gauge.value >= amount / 2)
                        {
                            usage += amount / 2;
                        }
                            
                    }
                }
                
            }
            return (usage == amount);
        }

        public bool CanIncrease(BloodType bloodType, int amount)
        {
            int usage = 0;
            for (int i = 0; i < bloodBanks.Length; i++)
            {
                if (bloodType != BloodType.AB)
                {
                    if (bloodBanks[i].bloodGroup == bloodType)
                    {
                        if (bloodBanks[i].gauge.amountLeft >= amount)
                        {
                            usage += amount;
                        }

                    }
                }
                else
                {
                    if (bloodBanks[i].bloodGroup == BloodType.A || bloodBanks[i].bloodGroup == BloodType.B)
                    {
                        if (bloodBanks[i].gauge.amountLeft >= amount / 2)
                        {
                            usage += amount / 2;
                        }

                    }
                }

            }
            return (usage == amount);
        }

        public void Decrease(BloodType bloodType, int amount)
        {
            for (int i = 0; i < bloodBanks.Length; i++)
            {
                if (bloodType != BloodType.AB)
                {
                    if (bloodBanks[i].bloodGroup == bloodType)
                    {
                        bloodBanks[i].DecrementGauge(amount);
                    }
                }
                else
                {
                    if (bloodBanks[i].bloodGroup == BloodType.A || bloodBanks[i].bloodGroup == BloodType.B)
                    {
                        bloodBanks[i].DecrementGauge(amount / 2);
                    }
                }
            }
        }

        public void Increase(BloodType bloodType, int amount)
        {
            for (int i = 0; i < bloodBanks.Length; i++)
            {
                if (bloodType != BloodType.AB)
                {
                    if (bloodBanks[i].bloodGroup == bloodType)
                    {
                        bloodBanks[i].IncrementGauge(amount);
                    }
                }
                else
                {
                    if (bloodBanks[i].bloodGroup == BloodType.A || bloodBanks[i].bloodGroup == BloodType.B)
                    {
                        bloodBanks[i].IncrementGauge(amount / 2);
                    }
                }
            }
        }

        public void Increase(int amount)
        {
            for (int i = 0; i < bloodBanks.Length; i++)
            {
                bloodBanks[i].IncrementGauge(amount);
            }
        }

        public void Decrease(int amount)
        {
            for (int i = 0; i < bloodBanks.Length; i++)
            {
                bloodBanks[i].DecrementGauge(amount);
            }
        }


        public void SetLevel(int _level)
        {
            level = _level;
        }

        public void UpgradeLevel()
        {
            level += 1;
        }

        public void LoadSave(Save _save)
        {
            SetBloodBanks(_save.bloodBanks);
            SetLevel(_save.level);
        }

    }
}

