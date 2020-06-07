using System;
using UnityEngine;

namespace CultManager
{
    [Serializable]
    public struct GameDataSet
    {
        public int money;
        public int bloodA;
        public int bloodB;
        public int bloodO;
        public int influence;
        public int police;
        public int candidates;
        public int cultists;
        public int investigators;
        public int puzzlesCount;
        public int level;
        public DateTime time;
        public float timeCode;

        public GameDataSet(MoneyData _money, BloodBankData _blood, InfluenceData _influence, PoliceData _police, CultData _cult, DemonData _demons)
        {
            money = (int) _money.money;
            bloodA = _blood.bloodBanks[0].gauge.value;
            bloodB = _blood.bloodBanks[1].gauge.value;
            bloodO = _blood.bloodBanks[2].gauge.value;
            influence = (int)_influence.value;
            police = _police.value;
            candidates = _cult.candidatesCount;
            cultists = _cult.cultists.Count;
            investigators = 0;
            puzzlesCount = _demons.demons.Count;
            level = 0;
            time = DateTime.Now;
            timeCode = Time.time;
        }

        public static bool ValueChanged(GameDataSet _a, GameDataSet _b)
        {
            return _a.money != _b.money ||
                    _a.bloodA != _b.bloodA ||
                    _a.bloodB != _b.bloodB ||
                    _a.bloodO != _b.bloodO ||
                    _a.influence != _b.influence ||
                    _a.police != _b.police ||
                    _a.candidates != _b.candidates ||
                    _a.cultists != _b.cultists ||
                    _a.investigators != _b.investigators ||
                    _a.puzzlesCount != _b.puzzlesCount ||
                    _a.level != _b.level;
        }

        public override string ToString()
        {
            return money + DataRecorder.Separation +
                    bloodA + DataRecorder.Separation +
                    bloodB + DataRecorder.Separation +
                    bloodO + DataRecorder.Separation +
                    influence + DataRecorder.Separation +
                    police + DataRecorder.Separation +
                    candidates + DataRecorder.Separation +
                    cultists + DataRecorder.Separation +
                    investigators + DataRecorder.Separation +
                    puzzlesCount + DataRecorder.Separation +
                    level + DataRecorder.Separation +
                    time + DataRecorder.Separation +
                    timeCode;
        }
    }
}