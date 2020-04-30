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

        public void Decrease(BloodType bloodType, int amount)
        {
            for (int i = 0; i < bloodBanks.Length; i++)
            {
                if (bloodBanks[i].bloodGroup == bloodType)
                {
                    Debug.Log("Decreased Blood " + bloodType+" "+amount);
                    bloodBanks[i].DecrementGauge(amount);
                }
            }
        }

        public void Increase(BloodType bloodType, int amount)
        {
            for (int i = 0; i < bloodBanks.Length; i++)
            {
                if (bloodBanks[i].bloodGroup == bloodType)
                {
                    Debug.Log("Increased Blood " + bloodType + " " + amount);
                    bloodBanks[i].IncrementGauge(amount);
                }
            }
        }

        public void Increase(int amount)
        {
            for (int i = 0; i < bloodBanks.Length; i++)
            {
                Debug.Log("Increase " + i);
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

