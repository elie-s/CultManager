using UnityEngine;

namespace CultManager
{
    public class Candidate
    {
        public Cultist cultist { get; private set; }
        public int policeValue { get; private set; }
        public int moneyValue { get; private set; }

        public Candidate(Cultist _cultist, int _policeValue, int _moneyValue)
        {
            cultist = _cultist;
            policeValue = _policeValue;
            moneyValue = _moneyValue;
        }
    }
}

