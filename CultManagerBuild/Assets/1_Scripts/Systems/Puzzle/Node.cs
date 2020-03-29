using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [System.Serializable]
    public struct Node
    {
        public int id;
        public Token token;

        Node(int _id, Token _token)
        {
            id = _id;
            token = _token;
        }

        public bool CompareNode(Node node)
        {
            if (node.id == id && token.CompareToken(node.token))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IncludesNode(Node node)
        {
            if (node.id == id && token.IncludesToken(node.token))
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
