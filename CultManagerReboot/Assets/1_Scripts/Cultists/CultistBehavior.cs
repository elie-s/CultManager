using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class CultistBehavior : MonoBehaviour
    {
        public Cultist cultist;
        public BloodType type;

        private void Init()
        {
            cultist = new Cultist(0, "Parmesan", 0);
            type = cultist.blood;
        }

    }
}

