using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class NodeBehavior : MonoBehaviour
    {
        public Node node;
        public bool active;
        public Node currentNode;

        public bool VerifyNode(PuzzleNode puzzleNode)
        {
            currentNode = puzzleNode.node;
            return (node.token.IncludesToken(currentNode.token));
        }
    }

}
