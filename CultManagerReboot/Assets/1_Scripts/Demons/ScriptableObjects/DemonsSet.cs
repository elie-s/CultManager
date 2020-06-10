using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Demons/Master Set")]
    public class DemonsSet : ScriptableObject
    {
        public DemonSpritesSet[] demonSpritesSets;

        public Sprite GetDemonSprite(DemonName _name)
        {
            return demonSpritesSets[(int)_name].demon;
        }

        public Sprite GetSpawnSprite(DemonName _name, float _accuracy)
        {
            Debug.Log(_name);
            return demonSpritesSets[(int)_name].SpawnSpriteFromAccuracy(_accuracy);
        }

        public Sprite GetDemonID(DemonName _name)
        {
            return demonSpritesSets[(int)_name].demonID;
        }

        public Sprite GetSpawnID(DemonName _name, float _accuracy)
        {
            return demonSpritesSets[(int)_name].SpawnIDFromAccuracy(_accuracy);
        }
    }
}
