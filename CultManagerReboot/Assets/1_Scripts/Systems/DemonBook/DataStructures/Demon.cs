using CultManager.HexagonalGrid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    [System.Serializable]
    public class Demon
    {
        public string demonName;
        public Pattern pattern;
        public bool isStarred;
        public int spriteIndex;

        public Demon(string _demonName,Pattern _pattern,bool _isStarred,int _spriteIndex)
        {
            demonName = _demonName;
            pattern = _pattern;
            isStarred = _isStarred;
            spriteIndex = _spriteIndex;
        }

        public void ToggleStar()
        {
            isStarred = !isStarred;
        }
    }
}

