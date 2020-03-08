using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Puzzles/PuzzlePattern")]
    public class PuzzlePattern : ScriptableObject
    {
        public Node[] nodeList;

        public bool CompareNodeList(Node[] nodes)
        {
            if (nodes.Length == nodeList.Length)
            {
                int ctr = 0;
                for (int i = 0; i < nodeList.Length; i++)
                {
                    Node curNode = nodeList[i];
                    for (int j = 0; j < nodes.Length; j++)
                    {
                        if (curNode.CompareNode(nodes[j]))
                        {
                            ctr++;
                            break;
                        }
                    }
                }

                return ctr == nodeList.Length;
            }
            else
            {
                return false;
            }
        }

        public bool CompareNodeList(List<PuzzleNode> nodes)
        {
            if (nodes.Count == nodeList.Length)
            {
                int ctr = 0;
                for (int i = 0; i < nodeList.Length; i++)
                {
                    Node curNode = nodeList[i];
                    for (int j = 0; j < nodes.Count; j++)
                    {
                        if (curNode.CompareNode(nodes[j].node))
                        {
                            ctr++;
                            break;
                        }
                    }
                }

                return ctr == nodeList.Length;
            }
            else
            {
                return false;
            }
        }
    }
}

