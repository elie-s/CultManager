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
            IntGauge intGauge = new IntGauge(90, 100, false);
            for (int i = 0; i < bloodBanks.Length; i++)
            {
                bloodBanks[i] = new BloodBank((BloodType)i,intGauge);
            }
        }

        public void Decrease(BloodType bloodType,int amount)
        {
            Debug.Log("Decrease ");
            for (int i = 0; i < bloodBanks.Length; i++)
            {
                if (bloodBanks[i].bloodGroup == bloodType)
                {
                    Debug.Log("Decrease " + amount);
                    bloodBanks[i].DecrementGauge(amount);
                }
            }
        }

        public void Increase(BloodType bloodType, int amount)
        {
            Debug.Log("Increase ");
            for (int i = 0; i < bloodBanks.Length; i++)
            {
                if (bloodBanks[i].bloodGroup == bloodType)
                {
                    Debug.Log("Increase " + amount);
                    bloodBanks[i].IncrementGauge(amount);
                }
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

