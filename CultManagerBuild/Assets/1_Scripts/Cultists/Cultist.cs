using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [System.Serializable]
    public class Cultist
    {
        public readonly ulong id;
        public string cultistName { get; private set; }
        public int age { get; private set; }
        public IntGauge faith { get; private set; }
        public IntGauge morale { get; private set; }
        public Sprite sprite { get; private set; }
        public CultistTraits traits { get; private set; }

        public Cultist(ulong _id, string _name, Sprite _sprite)
        {
            id = _id;
            cultistName = _name;
            sprite = _sprite;
            RandomAge();
            faith = new IntGauge(0, 100, false);
            faith.SetValue(50);
            morale = new IntGauge(0, 100, false);
            morale.SetValue(50);
        }

        public void RandomAge()
        {
            int value = 11;

            for (int i = 0; i < 7; i++)
            {
                value += Random.Range(1, 7);
            }

            age = value;
        }
    }
}