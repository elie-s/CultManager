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

        public bool IncludesToken(Token token)
        {
            int a=0, b=0,c=0;
            string[] Traits = System.Enum.GetNames(typeof(CultistTraits));
            for (int i = 0; i < Traits.Length; i++)
            {
                if ((cultistTraits.ToString()).Contains(Traits[i]))
                {
                    a++;
                    if ((token.cultistTraits.ToString()).Contains(Traits[i]))
                    {
                        b++;
                    }
                }
                else if ((token.cultistTraits.ToString()).Contains(Traits[i]))
                {
                    c++;
                }
            }
            
            if (b > a || c > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

