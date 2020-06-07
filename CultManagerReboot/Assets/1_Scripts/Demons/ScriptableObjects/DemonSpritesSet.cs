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
                if (_accuracy <= (float)i / (float)(spawns.Length - 1))
                {
                    //Debug.Log(_accuracy + " -> " + i);
                    return spawns[i];
                }
            }

            //Debug.Log(_accuracy + " -> " + spawns.Length);
            return spawns[spawns.Length];
        }

        public Sprite SpawnIDFromAccuracy(float _accuracy)
        {
            for (int i = 0; i < spawnsID.Length; i++)
            {
                if (_accuracy <= (float)i / (float)(spawnsID.Length - 1))
                {
                    //Debug.Log(_accuracy + " -> " + i);
                    return spawnsID[i];
                }
            }

            //Debug.Log(_accuracy + " -> " + spawnsID.Length);
            return spawnsID[spawnsID.Length];
        }
    }
}