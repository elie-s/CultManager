using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Demons/Sprites Set")]
    public class DemonSpritesSet : ScriptableObject
    {
        public Sprite[] spawns;
        public Sprite demon;
        public Sprite[] spawnsID;
        public Sprite demonID;

        public Sprite SpawnSpriteFromAccuracy(float _accuracy)
        {

            for (int i = 0; i < spawns.Length; i++)
            {
                if (_accuracy <= i / (spawns.Length - 1)) return spawns[i];
            }

            return spawns[spawns.Length];
        }

        public Sprite SpawnIDFromAccuracy(float _accuracy)
        {

            for (int i = 0; i < spawnsID.Length; i++)
            {
                if (_accuracy <= i / (spawnsID.Length - 1)) return spawnsID[i];
            }

            return spawnsID[spawnsID.Length];
        }
    }
}