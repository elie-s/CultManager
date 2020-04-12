using UnityEngine;

namespace CultManager
{
    [System.Serializable]
    public class Cultist
    {
        public ulong id { get; private set; }
        public string cultistName { get; private set; }
        public int age { get; private set; }
        public int spriteIndex { get; private set; }
        public bool occupied { get; set; }
        public CultistTraits traits { get; private set; }

        public Cultist(ulong _id, string _cultistName, int _spriteIndex)
        {
            id = _id;
            cultistName = _cultistName;
            spriteIndex = _spriteIndex;
            RandomAge();
            RandomTraits();
            occupied = false;
        }

        public void ToggleOccupy()
        {
            occupied = !occupied;
        }

        public bool IsOccupied()
        {
            return occupied;
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

        public void RandomTraits()
        {
            traits = (CultistTraits)Random.Range(0, 65);
        }

    }
}

