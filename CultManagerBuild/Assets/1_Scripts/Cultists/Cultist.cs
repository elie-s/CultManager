using UnityEngine;

namespace CultManager
{
    [System.Serializable]
    public class Cultist
    {
        public ulong id { get; private set; }
        public string cultistName { get; private set; }
        public int age { get; private set; }
        public IntGauge faith { get; private set; }
        public IntGauge morale { get; private set; }
        public int spriteIndex { get; private set; }
        public CultistTraits traits { get; private set; }
        public Room room { get; private set; }

        public bool isBusy => room != Room.none;

        public Cultist(ulong _id, string _name, int _sprite)
        {
            id = _id;
            cultistName = _name;
            spriteIndex = _sprite;
            room = Room.none;
            RandomAge();
            faith = new IntGauge(0, 100, false);
            faith.SetValue(50);
            morale = new IntGauge(0, 100, false);
            morale.SetValue(50);

            //traits = CultistTraits.TraitA | CultistTraits.TraitC | CultistTraits.TraitD;

            //traits += CultistTraits.TraitF;
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

        public void SetToRoom(Room _room)
        {
            room = _room;
        }

        public override string ToString()
        {
            return "Cultist #" + id + " (" + cultistName + ")" + (room != Room.none ? " ["+room+"]" : "");
        }
    }
}