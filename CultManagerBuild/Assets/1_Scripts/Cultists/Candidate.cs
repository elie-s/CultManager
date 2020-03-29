using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class Candidate
    {
        public Cultist cultist { get; private set; }
        public int policeValue { get; private set; }
        public int money { get; private set; }

        public Candidate(Cultist _cultist, int _policeValue, int _money)
        {
            cultist = _cultist;
            policeValue = _policeValue;
            money = _money;
        }
    }
}