using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    [System.Serializable]
    public class PersistentDemon
    {
        public int id;
        public int spriteIndex;

        public Modifier modifier;

        public PersistentDemon(int _id, int _spriteIndex)
        {
            id = _id;
            spriteIndex = _spriteIndex;
        }

    }
}

