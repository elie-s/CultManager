using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    [System.Serializable]
    public struct Token 
    {
        public CultistTraits cultistTraits;

        Token(CultistTraits _cultistTraits)
        {
            cultistTraits = _cultistTraits;
        }

        public bool CompareToken(Token token)
        {
            if (token.cultistTraits.Equals(cultistTraits))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

