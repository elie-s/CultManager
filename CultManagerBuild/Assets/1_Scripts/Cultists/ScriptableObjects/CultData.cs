using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Cult/Data")]
    public class CultData : ScriptableObject
    {
        public List<Cultist> cultists { get; private set; }
        private ulong idIndex;

        public void Init()
        {
            idIndex = 0;
        }

        public void AddCultist(Cultist _cultist)
        {
            cultists.Add(_cultist);
        }

        public Cultist GetCultist(ulong _id)
        {
            for (int i = 0; i < cultists.Count; i++)
            {
                if (cultists[i].id == _id) return cultists[i];
            }

            return null;
        }

        public Cultist CreateCultist(string _name, Sprite _sprite)
        {
            Cultist result = new Cultist(idIndex, _name, _sprite);
            idIndex++;

            return result;
        }
    }
}