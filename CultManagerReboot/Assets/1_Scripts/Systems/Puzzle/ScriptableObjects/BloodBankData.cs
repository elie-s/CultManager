using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Puzzle/BloodBankData")]
    public class BloodBankData : ScriptableObject
    {
        public BloodBank[] bloodBanks=new BloodBank[3];
        public int level;

        public void SetBloodBanks(BloodBank[] _bloodBanks)
        {
            bloodBanks = _bloodBanks;
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

